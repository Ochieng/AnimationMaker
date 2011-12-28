using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

// グラフリスト
namespace Krustf
{

// DxLibのユーティリティ
static class DxLibUtil
{
    static DxLibUtil()
    {
        // 利用できるフォーマットの検索
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        imageCodecList = new List<ImageCodecInfo>();
        foreach (ImageCodecInfo ici in codecs)
        {
            string s = ici.FormatDescription;
            if (s == "JPEG" || s == "PNG" || s == "BMP")
                imageCodecList.Add(ici);
        }
    }

    // DxLib で利用できる画像フォーマットか調べる
    static public bool IsValidImageFormat(string path)
    {
        Bitmap bmp = null;
        try {
            bmp = new Bitmap(path);
            foreach (ImageCodecInfo ici in imageCodecList)
                if (bmp.RawFormat.Guid == ici.FormatID) return true;
        } catch (Exception) {
            return false;
        } finally {
            if (bmp != null)
                bmp.Dispose();
        }
        return false;
    }

    private static List<ImageCodecInfo> imageCodecList;
}

// グラフィックに対するユーティリティ
static class GraphicsUtil
{
    // 32bitフォーマットでコピー
    static public Bitmap ImageClone(Bitmap img)
    {
        if (img.PixelFormat != PixelFormat.Format32bppArgb) {
            var rect = new Rectangle(new Point(), img.Size);
            var cln  = img.Clone(rect, PixelFormat.Format32bppArgb);
            img.Dispose();
            return cln;
        } else {
            return img;
        }
    }

    // 画像変更
    static public Bitmap drawDivLines(int xdiv, int ydiv, int w, int h, Bitmap image)
    {
        Bitmap cimg = ImageClone(image);
        System.Drawing.Graphics cgraph = System.Drawing.Graphics.FromImage(cimg);
        Pen cpen = new Pen(Color.Black, 5);
        for (int i = 1; i < xdiv; ++i)
            cgraph.DrawLine(cpen, w * i - 1, 0, w * i - 1, cimg.Height);
        for (int i = 1; i < ydiv; ++i)
            cgraph.DrawLine(cpen, 0, h * i - 1, cimg.Width, h * i - 1);
        cgraph.Dispose();
        cpen.Dispose();
        return cimg;
    }

    // 単一グラフィックを生成
    static public SimpleGraphic createGraph(string id, string path)
    {
        return new SimpleGraphic(id, path);
    }

    // 分割グラフィックを生成
    static public DivGraphic createGraph(int x, int y, int all, int w, int h, string id, string path)
    {
        if (x * y < all)
            return null;
        var dat = new DivGraphic.Parameter(x, y, all, new Size(w, h));
        return new DivGraphic(id, path, dat);
    }

    // アニメーションを生成
    static public AnimationGraphic createGraph(int interval, int x, int y, int all, int w, int h, string id, string path)
    {
        if (x * y < all)
            return null;
        var dat = new AnimationGraphic.Parameter(interval, x, y, all, new Size(w, h));
        return new AnimationGraphic(id, path, dat);
    }
}

// グラフリストの要件
public abstract class GraphBase
{
    // XMLへの書き込みを行なう
    public abstract void writeToXML(XmlWriter writer);

    // コンストラクタでIDとファイルパスを受け取る
    protected GraphBase(string id, string entryPath)
    {
        ID = id;
        EntryPath = entryPath;
    }

    // IDを返す
    public override string ToString()
    {
        return ID;
    }

    // ファイルパスの取得
    public string GetEntryPath()
    {
        return EntryPath;
    }

    // XMLへの属性データの書き込み
    public void WriteAttribute(XmlWriter writer, string attrName, string dat)
    {
        writer.WriteStartAttribute(attrName, "");
        writer.WriteString(dat);
        writer.WriteEndAttribute();
    }

    protected string ID;
    protected string EntryPath;
}

public class GraphXmlBase
{
    public static readonly string GraphicNamespace = "graphics";
    public static readonly string DirectoryAttr = "directory";
    public static readonly string EntryElement = "entry";
    public static readonly string AnimationElement = "animation";
    public static readonly string DivEntryElement = "diventry";
}

// 通常のグラフィック
public class SimpleGraphic : GraphBase
{
    public SimpleGraphic(string id, string path)
        : base(id,path)
    {
        Name = Path.GetFileName(path);
    }

    public override void writeToXML(XmlWriter writer)
    {
        // ノード
        writer.WriteStartElement(GraphXmlBase.EntryElement, "");
        // 属性ノード
        WriteAttribute(writer, "id", ID);
        // ノードの文字列
        writer.WriteString(Name);
        writer.WriteEndElement();
    }

    // XMLファイルからこのクラスを作成する
    public static SimpleGraphic CreateInstance(XmlTextReader xr, string dir)
    {
        xr.MoveToAttribute("id"); // id 属性
        string id = xr.Value;
        xr.MoveToElement();
        string path = xr.ReadString();
        return new SimpleGraphic(id, dir + path);
    }

    private string Name;        // グラフの画像
}

// アニメーション
public class DivGraphic : GraphBase
{
    public struct Parameter
    {
        public Parameter(int _xnum, int _ynum, int _allnum, Size _size)
        {
            xnum = _xnum;
            ynum = _ynum;
            allnum = _allnum;
            size = _size;
        }
        public int xnum;
        public int ynum;
        public int allnum;
        public Size size;
    }

    public DivGraphic(string id, string path, Parameter dat)
        : base(id, path)
    {
        Name = Path.GetFileName(path);
        Dat = dat;
    }

    public Parameter Parameteres
    {
        get {
            return Dat;
        }
    }

    public override void writeToXML(XmlWriter writer)
    {
        // ノード
        writer.WriteStartElement(GraphXmlBase.DivEntryElement, "");
        // 属性ノード
        WriteAttribute(writer, "id", ID);
        WriteAttribute(writer, "x", Dat.xnum.ToString());
        WriteAttribute(writer, "y", Dat.ynum.ToString());
        WriteAttribute(writer, "all", Dat.allnum.ToString());
        WriteAttribute(writer, "width", Dat.size.Width.ToString());
        WriteAttribute(writer, "height", Dat.size.Height.ToString());
        // ノードの文字列
        writer.WriteString(Name);
        writer.WriteEndElement();
    }

    // XMLファイルからこのクラスを作成する
    public static DivGraphic CreateInstance(XmlTextReader xr, string dir)
    {
        xr.MoveToAttribute("id"); // id 属性
        string id = xr.Value;
        xr.MoveToElement();
        xr.MoveToAttribute("x"); // x 属性
        int x = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("y"); // y 属性
        int y = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("all"); // all 属性
        int all = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("width"); // width 属性
        int w = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("height"); // height 属性
        int h = int.Parse(xr.Value);
        xr.MoveToElement();
        string path = xr.ReadString();
        return new DivGraphic(id, dir + path, new DivGraphic.Parameter(x, y, all, new Size(w, h)));
    }

    private string Name;       // グラフの画像ファイルパス
    private Parameter Dat;        // 分割情報
}

// アニメーション
public class AnimationGraphic : GraphBase
{
    public struct Parameter
    {
        public Parameter(int _interval, int _xnum, int _ynum, int _allnum, Size _size)
        {
            interval = _interval;
            xnum = _xnum;
            ynum = _ynum;
            allnum = _allnum;
            size = _size;
        }
        public int interval;
        public int xnum;
        public int ynum;
        public int allnum;
        public Size size;
    }

    public AnimationGraphic(string id, string path, Parameter dat)
        : base(id,path)
    {
        Name = Path.GetFileName(path);
        Dat = dat;
    }

    public Parameter Parameteres
    {
        get {
            return Dat;
        }
    }

    public override void writeToXML(XmlWriter writer)
    {
        // ノード
        writer.WriteStartElement(GraphXmlBase.AnimationElement, "");
        // 属性ノード
        WriteAttribute(writer, "id", ID);
        WriteAttribute(writer, "interval", Dat.interval.ToString());
        WriteAttribute(writer, "x", Dat.xnum.ToString());
        WriteAttribute(writer, "y", Dat.ynum.ToString());
        WriteAttribute(writer, "all", Dat.allnum.ToString());
        WriteAttribute(writer, "width", Dat.size.Width.ToString());
        WriteAttribute(writer, "height", Dat.size.Height.ToString());
        // ノードの文字列
        writer.WriteString(Name);
        writer.WriteEndElement();
    }

    // XMLファイルからこのクラスを作成する
    public static AnimationGraphic CreateInstance(XmlTextReader xr, string dir)
    {
        xr.MoveToAttribute("id"); // id 属性
        string id = xr.Value;
        xr.MoveToElement();
        xr.MoveToAttribute("interval"); // interval 属性
        int interval = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("x"); // x 属性
        int x = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("y"); // y 属性
        int y = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("all"); // all 属性
        int all = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("width"); // width 属性
        int w = int.Parse(xr.Value);
        xr.MoveToElement();
        xr.MoveToAttribute("height"); // height 属性
        int h = int.Parse(xr.Value);
        xr.MoveToElement();
        string path = xr.ReadString();
        return new AnimationGraphic(id, dir + path, new AnimationGraphic.Parameter(interval, x, y, all, new Size(w, h)));
    }

    private string  Name;       // グラフの画像ファイルパス
    private Parameter Dat;        // 分割情報
}

public class GraphXmlWriter : GraphXmlBase
{
    public GraphXmlWriter(string fileName, string dirAttr)
    {
        xw = new XmlTextWriter(fileName, Encoding.UTF8);
        xw.Formatting  = Formatting.Indented;
        xw.Indentation = 4;
        xw.WriteStartDocument();
        xw.WriteStartElement(GraphicNamespace);
        xw.WriteStartAttribute(DirectoryAttr, "");
        xw.WriteString(dirAttr);
        xw.WriteEndAttribute();
    }

    public void WritingList(List<GraphBase> list)
    {
        foreach (GraphBase g in list)
            g.writeToXML(xw);
    }

    public void Dispose()
    {
        if (xw != null)
        {
            xw.WriteEndElement(); // root element end
            xw.WriteEndDocument();
            xw.Close();
            xw = null;
        }
    }

    private XmlTextWriter xw;
}

public class GraphXmlReader : GraphXmlBase
{
    public GraphXmlReader(string fileName)
    {
        dirAttr = null;
        xr = new XmlTextReader(fileName);
        MoveElement();
    }

    private void MoveElement()
    {
        while (xr.Read() && dirAttr == null) {
            xr.MoveToContent();
            if (IsNode(xr)) {
                if (xr.LocalName.Equals(GraphicNamespace)) {
                    xr.MoveToAttribute(DirectoryAttr);
                    dirAttr = xr.Value.TrimStart("./".ToCharArray());
                    xr.MoveToElement();
                }
            }
        }
    }

    private bool IsNode(XmlTextReader xr)
    {
        return xr.NodeType == XmlNodeType.Element && xr.HasAttributes;
    }

    public string GetDirectory()
    {
        return dirAttr;
    }

    public void AddGraphs(Action<GraphBase> AddList)
    {
        while (xr.Read()) {
            xr.MoveToContent();
            if (IsNode(xr)) {
                if (xr.LocalName.Equals(EntryElement))
                    AddList(SimpleGraphic.CreateInstance(xr, "./" + dirAttr));
                else if (xr.LocalName.Equals(DivEntryElement))
                    AddList(DivGraphic.CreateInstance(xr, "./" + dirAttr));
                else if (xr.LocalName.Equals(AnimationElement))
                    AddList(AnimationGraphic.CreateInstance(xr, "./" + dirAttr));
            }
        }
    }

    public void Dispose()
    {
        if (xr != null) {
            xr.Close();
            xr = null;
        }
    }

    private string dirAttr;
    private XmlTextReader xr;
}

}
