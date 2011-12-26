using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Threading;

using SlimDX;
using SlimDX.Direct3D9;

namespace DxLibGraphMaker
{
    [StructLayout(LayoutKind.Sequential)]
    struct Vertex
    {
        public static readonly VertexFormat Format = VertexFormat.Position | VertexFormat.Texture1;
        public static readonly int          Size   = Marshal.SizeOf(typeof(Vertex));

        public Vector3 Position;
        public Vector2 TextureCoordnate; 
    }

    static class TextureUtil
    {
        // System.Drawing.BitmapからTextureを生成
        static public Texture TextureFromImage(Device device, Bitmap img)
        {
            // 空のテクスチャ
            Texture tex = new Texture(device, img.Width, img.Height, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);

            var desc = tex.GetLevelDescription(0);
            var drec = tex.LockRectangle(0, new Rectangle(0, 0, img.Width, img.Height), LockFlags.Discard);
            var bmpd = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat);
            var ptr = bmpd.Scan0;

            // コピー
            drec.Data.WriteRange(ptr, bmpd.Width * bmpd.Height * 4);

            img.UnlockBits(bmpd);
            tex.UnlockRectangle(0);
            return tex;
        }
    }

    class DrawObject
    {
        private Texture      texture;
        private VertexBuffer vertexBuf;

        public Texture Texture { get { return texture; } }
        protected VertexBuffer VertexBuf { get { return vertexBuf; } }

        // テクスチャは外部から持ち寄る
        // bounds : 描画可能画面めいっぱいに描画するか
        // img はDisposeされる
        public DrawObject(Device device, Bitmap img, bool bounds = false)
        {
            texture = TextureUtil.TextureFromImage(device, img);

            var desc = texture.GetSurfaceLevel(0).Description;
            float texWidth = desc.Width;
            float texHeight = desc.Height;
            float imgWidth = img.Width;
            float imgHeight = img.Height;

            // 頂点バッファ作成
            int vertexSize = Vertex.Size;
            vertexBuf = new VertexBuffer(device, 4 * vertexSize, Usage.WriteOnly, Vertex.Format, Pool.Managed);

            // 頂点バッファを初期化
            // 座標は中心点を(0,0,0)にした時に中央に描画できるように設定
            float x1 = 0.5f / texWidth, y1 = 0.5f / texHeight;
            float x2 = (imgWidth + 0.5f) / texWidth, y2 = (imgHeight + 0.5f) / texHeight;
            var dataStream = vertexBuf.Lock(0, 4 * vertexSize, LockFlags.None);
            if (!bounds)
            {
                var w = imgWidth / 2.0f;
                var h = imgHeight / 2.0f;
                dataStream.WriteRange(new[] {
                        new Vertex { Position = new Vector3(0 - w, 0 - h, 0.5f), TextureCoordnate = new Vector2(x1, y1) },
                        new Vertex { Position = new Vector3(imgWidth - w, 0 - h, 0.5f), TextureCoordnate = new Vector2(x2, y1) },
                        new Vertex { Position = new Vector3(0 - w, imgHeight - h, 0.5f), TextureCoordnate = new Vector2(x1, y2) },
                        new Vertex { Position = new Vector3(imgWidth - w, imgHeight - h, 0.5f), TextureCoordnate = new Vector2(x2, y2) }
                });
            }
            else
            {
                var w = device.Viewport.Width  / 2.0f;
                var h = device.Viewport.Height / 2.0f;
                dataStream.WriteRange(new[] {
                        new Vertex { Position = new Vector3(-w, -h, 0.5f), TextureCoordnate = new Vector2(x1, y1) },
                        new Vertex { Position = new Vector3(w, -h, 0.5f), TextureCoordnate = new Vector2(x2, y1) },
                        new Vertex { Position = new Vector3(-w, h, 0.5f), TextureCoordnate = new Vector2(x1, y2) },
                        new Vertex { Position = new Vector3(w, h, 0.5f), TextureCoordnate = new Vector2(x2, y2) }
                });
            }
            vertexBuf.Unlock();
        }

        // 描画
        public virtual void draw(Device device)
        {
            device.SetStreamSource(0, vertexBuf, 0, Vertex.Size);
            device.VertexFormat = Vertex.Format;
            device.SetTexture(0, texture);
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }

        // 削除
        public virtual void Dispose()
        {
            if (texture != null) texture.Dispose();
            if (vertexBuf != null) vertexBuf.Dispose();
        }
    }

    class FrameCounter
    {
        public readonly int n;

        // フレーム間隔で初期化
        public FrameCounter(int n)
        {
            this.n = n;
        }

        public bool IsCounterValidate()
        {
            ++Counter;
            return (Counter %= n) == 0;
        }

        private int Counter = 0;
    }

    class Animation : DrawObject
    {
        private FrameCounter Counter = null;
        private int pos = 0;
        private int all;
        private int xdiv;
        private int ydiv;
        private int width;
        private int height;
        private bool isAnimation = true;

        // そもそもアニメーションを行うか
        public bool IsAnimation
        {
            get
            {
                return isAnimation;
            }
            set
            {
                isAnimation = value;
                changeVertex(pos = 0);
            }
        }

        // 第3引数はフレーム間隔
        public Animation(Device device, Bitmap img, int n, int width, int height, int all, int xdiv, int ydiv)
            : base(device, img, false)
        {
            Counter = new FrameCounter(n);
            this.width = width;
            this.height = height;
            this.all = all;
            this.xdiv = xdiv;
            this.ydiv = ydiv;
        }

        // x と y それぞれの位置を計算する
        private static void calcUV(int pos, int xdiv, out int x, out int y)
        {
            x = pos % xdiv;
            y = pos / xdiv;
        }

        // 描画
        public override void draw(Device device)
        {
            if (Counter.IsCounterValidate() && IsAnimation)
            {
                ++pos;
                changeVertex(pos %= all);
            }
            base.draw(device);
        }

        // 頂点位置の変更
        private void changeVertex(int pos)
        {
            // UVの位置計算
            int x, y;
            calcUV(pos, xdiv, out x, out y);

            // 算出したUVを使って頂点位置を再計算する
            float U = 1.0f / xdiv;
            float V = 1.0f / ydiv;
            float x1 = 0.5f / width + U * x;
            float x2 = U * (x + 1);
            float y1 = 0.5f / height + V * y;
            float y2 = V * (y + 1);

            // 頂点バッファを初期化
            // 座標は中心点を(0,0,0)にした時に中央に描画できるように設定
            var dataStream = VertexBuf.Lock(0, 4 * Vertex.Size, LockFlags.None);
            var w = width / 2.0f;
            var h = height / 2.0f;
            dataStream.WriteRange(new[] {
                    new Vertex { Position = new Vector3(0 - w, 0 - h, 0.5f), TextureCoordnate = new Vector2(x1, y1) },
                    new Vertex { Position = new Vector3(width - w, 0 - h, 0.5f), TextureCoordnate = new Vector2(x2, y1) },
                    new Vertex { Position = new Vector3(0 - w, height - h, 0.5f), TextureCoordnate = new Vector2(x1, y2) },
                    new Vertex { Position = new Vector3(width - w, height - h, 0.5f), TextureCoordnate = new Vector2(x2, y2) }
            });
            VertexBuf.Unlock();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    class ViewWindow
    {
        // 描画デバイス
        static private Device device;
        static private PresentParameters param;
        static private Control form;

        // Controlへの描画デバイスを作成
        static public void createRenderDevice(Control renderForm)
        {
            param = new PresentParameters();
            param.BackBufferWidth = renderForm.ClientSize.Width;
            param.BackBufferHeight = renderForm.ClientSize.Height;
            param.BackBufferCount = 1;
            param.SwapEffect = SwapEffect.Flip;
            device = new Device(new Direct3D(), 0, DeviceType.Hardware
                , renderForm.Handle, CreateFlags.HardwareVertexProcessing, param);
            form = renderForm;
        }

        // 描画デバイスを含むDirect3Dが使用したすべてのリソースを削除
        static public void destroyResource()
        {
            foreach (var item in ObjectTable.Objects)
                item.Dispose();
        }
        // ---------------------------

        private DrawObject iconObject = null;
        private DrawObject backGround = null;
        private Thread thr = null;
        private object synchronizedObject = new object();
        private bool isBackGroundDraw = false;
        private float x = 0, y = 0;
        private bool runnable = true;

        public ViewWindow()
        {
        }

        // 削除処理
        public void Dispose()
        {
            if(thr != null) thr.Abort();
            destroyResource();
        }

        // スレッドは描画状態にあるか
        public bool IsRunnable
        {
            set
            {
                lock (synchronizedObject)
                {
                    runnable = value;
                }
            }
        }

        // アニメーションの再生と停止
        public bool IsAnimation
        {
            set
            {
                lock (synchronizedObject)
                {
                    if (iconObject is Animation)
                    {
                        ((Animation)(iconObject)).IsAnimation = value;
                    }
                }
            }
        }

        // 背景描画をするか
        public bool IsBackGroundDraw
        {
            set
            {
                lock (synchronizedObject)
                {
                    isBackGroundDraw = value;
                }
            }
        }

        // テクスチャのロード
        public void LoadTexture(Bitmap img)
        {
            lock (synchronizedObject)
            {
                var cache = iconObject;
                try
                {
                    iconObject = new DrawObject(device, img);
                    if (cache != null) cache.Dispose();
                    runnable = true;
                    StartThread();
                }
                catch (Exception e)
                {
                    iconObject = cache;
                    Console.WriteLine(e.Message);
                }
            }
        }

        // アニメーションとしてロード
        public void LoadAnimation(Bitmap img, int n, int width, int height, int all, int xdiv, int ydiv)
        {
            lock (synchronizedObject)
            {
                var cache = iconObject;
                try
                {
                    iconObject = new Animation(device, img, n, width, height, all, xdiv, ydiv);
                    if (cache != null) cache.Dispose();
                    runnable = true;
                    StartThread();
                }
                catch (Exception e)
                {
                    iconObject = cache;
                    Console.WriteLine(e.Message);
                }
            }
        }

        // 背景画像のロード
        public void LoadBackGround(Bitmap img)
        {
            lock (synchronizedObject)
            {
                var cache = backGround;
                try
                {
                    if (img != null)
                        backGround = new DrawObject(device, img, true);
                    if (cache != null)
                        cache.Dispose();
                    runnable = true;
                    StartThread();
                }
                catch (Exception e)
                {
                    backGround = cache;
                    Console.WriteLine(e.Message);
                }
            }
        }

        // 描画位置の変更
        public void SetTranslation(float x, float y)
        {
            lock (synchronizedObject)
            {
                this.x = x;
                this.y = y;
            }
        }

        // スレッドの作成と開始
        private void StartThread()
        {
            // 初回のスレッド読み込み時にスレッドを起動する
            if (thr == null)
            {
                thr = new Thread(new ThreadStart(threadMain));
                thr.Start();
            }
        }

        // 描画の設定
        private void RenderingConfig(Device dev, bool alphaBlend)
        {
            // カラー
            dev.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
            dev.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
            // アルファ成分
            dev.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.SelectArg1);
            dev.SetTextureStageState(0, TextureStage.AlphaOperation, TextureArgument.Texture);
            // ブレンド
            dev.SetRenderState(RenderState.AlphaBlendEnable, alphaBlend);
            dev.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
            dev.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
        }

        // 描画
        private void draw()
        {
            // 行列指定
            // ビュー行列はY軸が下向きに伸びる、中心は(0,0,0)である
            var view = Matrix.Identity;
            view.M22 = -1;
            device.SetTransform(TransformState.View, view);

            // 射影行列は遠近感を可能な限りなくす
            var proj = Matrix.OrthoLH(form.Width, form.Height, -0.01f, 1.01f);
            device.SetTransform(TransformState.Projection, proj);

            // 描画設定
            RenderingConfig(device, true);

            // 背景では平行移動しない
            device.SetTransform(TransformState.World, Matrix.Identity);

            // 背景描画（あれば）
            if (backGround != null && isBackGroundDraw)
                backGround.draw(device);

            // 平行移動行列で画像を移動させる(ユーザ指定)
            device.SetTransform(TransformState.World, Matrix.Translation(x, y, 0));

            // アイコン描画
            if (iconObject != null)
                iconObject.draw(device);
        }

        // スレッド関数
        private void threadMain()
        {
            while (true)
            {
                // リソースを占有して実行
                lock (synchronizedObject)
                {
                    try
                    {
                        device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Black, 1.0f, 0);
                        device.BeginScene();
                        if (runnable)
                        {
                            draw();
                        }
                        device.EndScene();
                        device.Present();
                    }
                    catch (Direct3D9Exception e)
                    {
                        while (device.TestCooperativeLevel() != ResultCode.DeviceNotReset)
                            Thread.Sleep(2);
                        Console.WriteLine(e.Message);
                        // 作成元スレッドでリセットしてもらう
                        form.Invoke((MethodInvoker)delegate() { device.Reset(param); });
                    }
                }
                // 適当に16秒程度停止
                Thread.Sleep(16);
            }
        }
    }
}
