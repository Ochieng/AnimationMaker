using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace DxLibGraphMaker
{
    using Krustf;
    // リストの型
    using GraphicList = System.Collections.Generic.List<Krustf.GraphBase>;

    public partial class Main : Form
    {
        private GraphicList list;
        // 読み込んだデータが変化したかまたは保存後変化したか
        private bool DataChanged;
        // 読み込んだファイルのパス
        private string LoadFilePath = null;
        // 現在表示中の画像
        private Bitmap images;
        // 現在表示中の画像ファイルへのパス
        private string imagePath = null;
        // 描画を担う
        private ViewWindow window = null;

        private Bitmap img
        {
            set
            {
                if (value != null)
                    images = GraphicsUtil.ImageClone(value);
            }
            get
            {
                return images;
            }
        }

        // コンストラクタ
        public Main()
        {
            InitializeComponent();
            list = new GraphicList();
            Initialize();

            ViewWindow.createRenderDevice(DrawablePanel);
            window = new ViewWindow();
        }

        /// 使用中のリソースをすべてクリーンアップします。
        /// マネージ リソースが破棄される場合 true、破棄されない場合は false です。
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            window.Dispose();
            base.Dispose(disposing);
        }

        // 初期化
        private void Initialize()
        {
            list.Clear();
            GraphicList.Items.Clear();
            DataChanged = false;

            // フィルタを設定
            openFileDialog1.Filter = "pngファイル(*.png)|*.png|jpegファイル(*.jpg)|*.jpg|bmpファイル(*.bmp)|*.bmp|すべてのファイル(*.*)|*.*";
            openFileDialog2.Filter = "xmlファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
            saveFileDialog1.Filter = "xmlファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";

            // 初期ディレクトリはカレントディレクトリに設定
            string curDir = Directory.GetCurrentDirectory();
            openFileDialog1.InitialDirectory = curDir;
            openFileDialog2.InitialDirectory = curDir;
            saveFileDialog1.InitialDirectory = curDir;
        }

        // リストへの追加
        private void AddList(GraphBase g)
        {
            list.Add(g);
            // UIのリストへIDを登録
            GraphicList.Items.Add(g.ToString());
            // データ変更発生
            DataChanged = true;
        }

        // リストからの削除
        private void RemoveList(string id)
        {
            GraphBase g = list.Find(i => i.ToString() == id);
            if (g != null)
            {
                list.Remove(g);
                GraphicList.Items.RemoveAt(GraphicList.SelectedIndex);
                DataChanged = true;
            }
        }

        // XMLへ登録したエントリの画像ファイルを同一ディレクトリへコピー
        private void CopyToXmlListFile(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            foreach (GraphBase g in list)
            {
                string src = g.GetEntryPath();
                string dst = dir + "\\" + Path.GetFileName(src);
                if (!File.Exists(dst))
                    File.Copy(src, dst);
            }
        }

        // XMLへの書き込み
        private void WriteListToXML(String Filename)
        {
            GraphXmlWriter gxw = null;
            try
            {
                gxw = new GraphXmlWriter(Filename, "./" + textBox1.Text);
                gxw.WritingList(list);
                // XMLファイルと同じ位置に画像のディレクトリを出力する
                CopyToXmlListFile(Path.GetDirectoryName(Filename) + "\\" + Path.GetDirectoryName(textBox1.Text));
            }
            catch (XmlException e)
            {
                MessageBox.Show(e.StackTrace, "Exception!");
            }
            finally
            {
                if (gxw != null) gxw.Dispose();
            }
        }

        // リストへのドラッグドロップによるファイル登録
        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                EntryRegistRange((string[])e.Data.GetData(DataFormats.FileDrop));
        }
        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                     ? DragDropEffects.All
                     : DragDropEffects.None;
        }

        // 文字列vectorに対して実行
        private void EntryRegistRange(string[] EntryPaths)
        {
            // 複数画像のドロップは許可しない
            if (EntryPaths.Length == 1)
            {
                var s = EntryPaths[0];
                // 画像フォーマット
                if (DxLibUtil.IsValidImageFormat(s))
                    EntryRegist(s);
                // XMLファイルかも
                else
                    OpenXML(s);
            }
        }

        // 指定されたエントリの登録
        private void EntryRegist(string EntryPath)
        {
            if (DxLibUtil.IsValidImageFormat(EntryPath))
            {
                CanselButton_Click(null, null);
                imagePath = EntryPath;
                img = GraphicsUtil.ImageClone(new Bitmap(imagePath));
                ImgWidth.Text = img.Width.ToString();
                ImgHeight.Text = img.Height.ToString();
                window.LoadTexture(img);
                DataPanel.Visible = true;
                comboBox1_SelectedIndexChanged(null, null);
            }
            else
            {
                MessageBox.Show("画像ファイルではありません！", "Error!");
                imagePath = null;
            }
        }

        // 名前をつけて保存する
        private void SaveToNewName()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string newName = saveFileDialog1.FileName;
                WriteListToXML(newName);
                // 変更データは保存済み
                DataChanged = false;
                // ロードファイルは保存した状態
                LoadFilePath = newName;

                MessageBox.Show(
                    "指定ディレクトリにXMLファイル及び,\n ["
                    + Path.GetDirectoryName(textBox1.Text)
                    + "] ディレクトリを作成しました.\n2つをセットにして利用してください."
                    , "作成完了", MessageBoxButtons.OK
                );
            }
        }

        // 変更があるか及びユーザに対して保存するか問う
        // 戻り値はキャンセルが発生したか
        private bool SaveQuestionToCancel()
        {
            if (DataChanged)
            {
                DialogResult res
                    = MessageBox.Show("テキストは変更されています。保存しますか？", "データの変更", MessageBoxButtons.YesNoCancel);
                switch (res)
                {
                    case DialogResult.Yes:    SaveToNewName(); break;
                    case DialogResult.Cancel: return true;
                }
                // この時点でユーザは変更を保存したかしないことを許可している
                DataChanged = false;
            }
            return false;
        }

        // プログラム終了
        private void ExitTool_Click(object sender, EventArgs e)
        {
            // キャンセルが発生しない or データが変更されていない場合終了
            if (!SaveQuestionToCancel()) Close();
        }

        // 追加処理
        private void AddListsToString(String path)
        {
            EntryRegist(path);
        }

        // 新規作成
        private void NewTool_Click(object sender, EventArgs e)
        {
            if (!SaveQuestionToCancel()) Initialize();
        }

        // 上書き保存
        private void UpdateTool_Click(object sender, EventArgs e)
        {
            // ロードされていれば上書き
            if (LoadFilePath != null)
            {
                WriteListToXML(LoadFilePath);
                LoadFilePath = null;
            }
            // されていなければ名前をつけて保存
            else
            {
                SaveTool_Click(null, null);
            }
        }

        // 名前をつけて保存
        private void SaveTool_Click(object sender, EventArgs e)
        {
            SaveToNewName();
        }

        // エントリの追加
        private void AddTool_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                EntryRegist(openFileDialog1.FileName);
        }

        // XMLを読み込み
        private void OpenXML(string path)
        {
            Initialize();
            GraphXmlReader gxr = null;
            try
            {
                gxr = new GraphXmlReader(path);
                textBox1.Text = gxr.GetDirectory();
                gxr.AddGraphs(AddList);
                Environment.CurrentDirectory = Path.GetDirectoryName(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Exception!");
                return;
            }
            finally
            {
                if (gxr != null) gxr.Dispose();
                LoadFilePath = path;
                // 開くのは既存のもの
                DataChanged = false;
            }
        }

        // 開く
        private void OpenTool_Click(object sender, EventArgs ea)
        {
            // キャンセルが発生していなければ開く
            if (!SaveQuestionToCancel() && openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                OpenXML(openFileDialog2.FileName);
            }
        }

        // データ削除
        private void DeleteMenu_Click(object sender, EventArgs e)
        {
            if (GraphicList.Items.Count > 0)
            {
                RemoveList((string)GraphicList.SelectedItem);
                CanselButton_Click(null, null);
            }
        }

        // エントリの削除
        private void RemoveTool_Click(object sender, EventArgs e)
        {
            DeleteMenu_Click(sender, e);
        }

        // グラフィックの種類を変更
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            object s = comboBox1.SelectedItem;
            if (s == comboBox1.Items[0]) // 単一グラフィック
            {
                enableAnimation.Checked = false;
                enableAnimation.Enabled = false;
                DivPanel.Visible = false;
            }
            else if (s == comboBox1.Items[1]) // 分割グラフィック
            {
                enableAnimation.Checked = false;
                enableAnimation.Enabled = false;
                DivPanel.Visible = true;
            }
            else if (s == comboBox1.Items[2]) // アニメーション
            {
                enableAnimation.Checked = true;
                enableAnimation.Enabled = true;
                DivPanel.Visible = true;
            }
        }

        // 現在選択中のグラフィックデータを取り出してイメージに対するデータを設定する
        private void dataSetsToSelect()
        {
            string id = (string)GraphicList.SelectedItem;
            GraphBase g = list.Find(i => i.ToString() == id);
            if (g != null)
                dataSets(g);
        }

        // イメージに対するデータを設定
        private void dataUISets(int selectIndex, bool animationEnable)
        {
            DataPanel.Visible = true;
            comboBox1.SelectedIndex = selectIndex;
            enableAnimation.Checked = animationEnable;
        }

        private void dataSets(GraphBase g)
        {
            if (img != null) img.Dispose();
            img = new Bitmap(g.GetEntryPath());
            imagePath = g.GetEntryPath();
            if (g is SimpleGraphic)
            {
                // 単一グラフィック時のUIパラメータ設定
                SimpleGraphic sg = (SimpleGraphic)g;
                ID_Name.Text = sg.ToString();
                window.LoadTexture(img);
                dataUISets(0, false);
            }
            else if (g is DivGraphic)
            {
                // 分割グラフィック時のUIパラメータ設定
                DivGraphic dg = (DivGraphic)g;
                var dat = dg.Parameteres;
                ID_Name.Text = dg.ToString();
                X_Div.Text = dat.xnum.ToString();
                Y_Div.Text = dat.ynum.ToString();
                DivCount.Text = dat.allnum.ToString();
                window.LoadTexture(img);
                dataUISets(1, false);
            }
            else if (g is AnimationGraphic)
            {
                // アニメーション時のUIパラメータ設定
                AnimationGraphic ag = (AnimationGraphic)g;
                var dat = ag.Parameteres;
                ID_Name.Text = ag.ToString();
                X_Div.Text = dat.xnum.ToString();
                Y_Div.Text = dat.ynum.ToString();
                DivCount.Text = dat.allnum.ToString();
                animationFrame.Text = dat.interval.ToString();
                dataUISets(2, true);
            }
        }

        // リストの選択が変更されたら
        int graphicListSelectedBefore = -1; // 前回選択されていたインデックス
        private void GraphicList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GraphicList.SelectedIndex > -1)
            {
                if (GraphicList.SelectedIndex != graphicListSelectedBefore)
                {
                    RemoveTool.Enabled = DeleteMenu.Enabled = true;
                    // 画像をセット
                    string id = GraphicList.SelectedItem.ToString();
                    dataSets(list.Find(i => i.ToString() == id));
                    graphicListSelectedBefore = GraphicList.SelectedIndex;
                    // 名前変更
                    SaveButton.Text = "更新";
                }
            }
            else
            {
                graphicListSelectedBefore = -1;
                RemoveTool.Enabled = DeleteMenu.Enabled = false;
            }
        }

        // 既にリスト上に同一IDが存在しないかを確認する
        private bool IsExist(String s)
        {
            return list.Find(i => s.ToString() == i.ToString()) != null;
        }

        // IDの重複をチェックする
        private void IDCheck(String s)
        {
            if (s == "")
                MessageBox.Show("IDを指定してください.", "Error!");
            else if (IsExist(s))
                MessageBox.Show("このIDは既に登録されています！ 上書き保存時には上書きされます.", "Error!");
            else
                MessageBox.Show("このIDは利用可能です.", "Succeeded");
        }

        // IDチェックを行う
        private void button3_Click(object sender, EventArgs e)
        {
            IDCheck(ID_Name.Text);
        }

        // 分割数の合計を挿入
        private void allCountCalc()
        {
            int x, y;
            if (int.TryParse(X_Div.Text, out x) && int.TryParse(Y_Div.Text, out y))
            {
                DivCount.Text = (x * y).ToString();
            }
        }

        // 横方向への分割数が変更
        private void X_Div_TextChanged(object sender, EventArgs e)
        {
            int x;
            if (int.TryParse(X_Div.Text, out x))
            {
                if (x < 1)
                {
                    X_Div.Text = "";
                }
                else
                {
                    ImgWidth.Text = (img.Width / x).ToString();
                    allCountCalc();
                }
            }
            else
            {
                ImgWidth.Text = "###";
            }
        }

        private void Y_Div_TextChanged(object sender, EventArgs e)
        {
            int y;
            if (int.TryParse(Y_Div.Text, out y))
            {
                if (y < 1)
                {
                    Y_Div.Text = "";
                }
                else
                {
                    ImgHeight.Text = (img.Height / y).ToString();
                    allCountCalc();
                }
            }
            else
            {
                ImgHeight.Text = "###";
            }
        }

        // 単一グラフィック生成
        private SimpleGraphic createGraph()
        {
            if (ID_Name.Text == "")
                MessageBox.Show("IDを指定してください。");
            else
                return GraphicsUtil.createGraph(ID_Name.Text, imagePath);
            return null;
        }

        // 分割グラフィック生成
        private DivGraphic createDivGraph()
        {
            int x, y, all;
            if (ID_Name.Text == "")
                MessageBox.Show("IDを指定してください。");
            else if (!int.TryParse(X_Div.Text, out x))
                MessageBox.Show("横の分割数に整数以外が指定されています。");
            else if (!int.TryParse(Y_Div.Text, out y))
                MessageBox.Show("縦の分割数に整数以外が指定されています。");
            else if (!int.TryParse(DivCount.Text, out all))
                MessageBox.Show("総分割数に整数以外が指定されています。");
            else
                return GraphicsUtil.createGraph(
                    x, y, all, int.Parse(ImgWidth.Text), int.Parse(ImgHeight.Text)
                    , ID_Name.Text, imagePath
                );
            return null;
        }

        // アニメーション生成
        private AnimationGraphic createAnimationGraph()
        {
            int interval, x, y, all;
            if (ID_Name.Text == "")
                MessageBox.Show("IDを指定してください。");
            else if (!int.TryParse(X_Div.Text, out x))
                MessageBox.Show("横の分割数に整数以外が指定されています。");
            else if (!int.TryParse(Y_Div.Text, out y))
                MessageBox.Show("縦の分割数に整数以外が指定されています。");
            else if (!int.TryParse(DivCount.Text, out all))
                MessageBox.Show("総分割数に整数以外が指定されています。");
            else if (!int.TryParse(animationFrame.Text, out interval))
                MessageBox.Show("アニメーションのフレーム間隔に整数以外が指定されています。");
            else
                return GraphicsUtil.createGraph(
                    interval, x, y, all, int.Parse(ImgWidth.Text), int.Parse(ImgHeight.Text)
                    , ID_Name.Text, imagePath
                );
            return null;
        }

        // グラフの作成
        private GraphBase makeGraphic()
        {
            // そもそも登録する画像がドラッグドロップされていない
            if (imagePath == null)
                return null;

            // グラフィックデータを生成
            GraphBase g = null;
            object s = comboBox1.SelectedItem;
            if (s == comboBox1.Items[0])        // 単一グラフィック
                g = createGraph();
            else if (s == comboBox1.Items[1])   // 分割グラフィック
                g = createDivGraph();
            else if (s == comboBox1.Items[2])   // アニメーション
                g = createAnimationGraph();

            // この時点でnullなら作成失敗
            return g;
        }

        // IDが書かれていない or 存在する場合エラー
        private bool IDExistOrNone()
        {
            string id = ID_Name.Text;
            if (id == "")
            {
                MessageBox.Show("IDを指定してください.", "Error!!");
                return false;
            }
            else if (list.Find(i => i.ToString() == id) != null)
            {
                MessageBox.Show("IDが重複しています. 別のIDを指定してください.", "Error!!");
                return false;
            }
            return true;
        }

        // 登録データの更新
        // 登録の保存
        private void SaveButton_Click(object sender, EventArgs e)
        {
            GraphBase g = makeGraphic();
            if (g != null)
            {
                // 選択されていれば削除
                if (GraphicList.SelectedIndex != -1)
                    RemoveList(GraphicList.SelectedItem.ToString());
                // 登録されているIDと重複があるかチェック
                if (IDExistOrNone())
                {
                    // 登録
                    AddList(g);
                    // 保存が終わったのでクリア
                    CanselButton_Click(null, null);
                }
            }
        }

        // 登録キャンセル
        private void CanselButton_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            GraphicList.SelectedIndex = -1;
            ID_Name.Text = "";
            X_Div.Text = "";
            Y_Div.Text = "";
            DivCount.Text = "";
            ImgWidth.Text = "";
            ImgHeight.Text = "";
            imagePath = "";
            img = null;
            window.IsRunnable = false;
            SaveButton.Text = "保存"; // 名前変更
            DataPanel.Visible = false;
        }

        // アニメーションを有効にするか
        private void enableAnimation_CheckedChanged(object sender, EventArgs e)
        {
            animationFrame.Enabled = enableAnimation.Checked;
            // 無効にされたときに分割されていれば
            // インデックス0の画像だけ描画するように計算してなくてはならないはず
            if( window.IsAnimation = enableAnimation.Checked )
                checkStartAnimation();
        }

        // 背景描画を有効にするか
        private void enableBackGround_CheckedChanged(object sender, EventArgs e)
        {
            selectBackGround.Enabled = enableBackGround.Checked;
            // 背景画像を無効にしたときには
            // 背景描画できないように設定する必要があるます
            window.IsBackGroundDraw = enableBackGround.Checked;
        }

        // 背景画像の選択
        private void selectBackGround_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                backGroundFile.Text = openFileDialog1.FileName;
            }
        }

        // XやYの位置を変更
        private void X_Move_ValueChanged(object sender, EventArgs e)
        {
            setPosition();
        }

        private void Y_Move_ValueChanged(object sender, EventArgs e)
        {
            setPosition();
        }

        private void setPosition()
        {
            window.SetTranslation((float)X_Move.Value,(float)Y_Move.Value);
        }

        // アニメーションのフレーム間隔が変更された
        private void animationFrame_ValueChanged(object sender, EventArgs e)
        {
            checkStartAnimation();
        }

        // 分割数の総計が弄られた場合もアニメーションを開始できるか試してみる
        private void DivCount_TextChanged(object sender, EventArgs e)
        {
            checkStartAnimation();
        }

        // アニメーションをチェックしてから開始
        private void checkStartAnimation()
        {
            if (this.img != null && comboBox1.SelectedIndex != 0)
            {
                startAnimation();
                if (comboBox1.SelectedIndex == 1)
                {
                    window.IsAnimation = false;
                }
            }
        }

        // アニメーションを開始
        private void startAnimation()
        {
            int x, y, all;
            if (!int.TryParse(X_Div.Text, out x))
                return;
            else if (!int.TryParse(Y_Div.Text, out y))
                return;
            else if (!int.TryParse(DivCount.Text, out all))
                return;
            // 上記のデータを使ってアニメーション開始
            window.LoadAnimation(img, (int)animationFrame.Value
                 , int.Parse(ImgWidth.Text), int.Parse(ImgHeight.Text), all, x, y);
        }

        // 背景画像が変更された
        private void backGroundFile_TextChanged(object sender, EventArgs e)
        {
            string path = backGroundFile.Text;
            if (DxLibUtil.IsValidImageFormat(path))
            {
                var img = new Bitmap(path);
                var cln = GraphicsUtil.ImageClone(img);
                window.LoadBackGround(cln);
                cln.Dispose();
                img.Dispose();
            }
            else
            {
                MessageBox.Show("画像ファイルではありません！", "Error!");
            }
        }
    }
}