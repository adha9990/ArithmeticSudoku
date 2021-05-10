using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MSScriptControl;

namespace ArithmeticSudoku
{
    public partial class Form1 : Form
    {
        // 地圖
        List<string[]> map = new List<string[]>();
        // 當前焦點Label
        Label nlb = new Label();
        // 未處理問題
        Queue<Point> questions = new Queue<Point>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
        }
        private void init()
        {
            map = new List<string[]>();
            nlb = new Label();
            questions = new Queue<Point>();

            InitFileTable("./test.txt");
        }
        // 讀檔，建地圖
        private void InitFileTable(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            int w = lines[0].Split(',').Length,
                h = lines.Length,
                width = 50,
                height = 50;
            Platter.Size = new Size(width * w, height * h);
            for (int y = 0; y < h; y++)
            {
                string[] objs = lines[y].Split(',');
                for (int x = 0; x < w; x++)
                {
                    string text = objs[x];
                    Platter.Controls.Add(CreateJigsaw(String.Format("S;{0};{1}", x, y), x, y, width, height, text));
                    if (text == "?") questions.Enqueue(new Point(x, y));
                }
                map.Add(objs);
            }
        }
        // 新拼圖
        private Label CreateJigsaw(string name,int x,int y,int width,int height,string text)
        {
            Label lb = new Label();
            lb.Name = name;
            lb.Bounds = new Rectangle(x * width, y * height, width, height);
            lb.BackColor = Color.White;
            lb.BorderStyle = BorderStyle.FixedSingle;
            lb.TextAlign = ContentAlignment.MiddleCenter;
            lb.Font = new Font(DefaultFont.FontFamily, 18);
            lb.Text = text;
            if(text == "x") lb.Visible = false;
            if(text == "?")
            {
                lb.Click += new EventHandler(this.Jigsaw_click);
                lb.Text = "";
            }
            return lb;
        }
        private void Jigsaw_click(Object sender, EventArgs e)
        {
            // 焦點Label變色
            nlb.BackColor = Color.White;
            nlb = (Label)sender;
            nlb.BackColor = Color.LightGray;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)8 && nlb.Text.Length > 0)
            {
                nlb.Text = nlb.Text.Substring(0, nlb.Text.Length - 1);
            }
            else
            {
                if(e.KeyChar >= 48 && e.KeyChar <= 57)
                {
                    nlb.Text += e.KeyChar;
                }
            }
        }
        
        private void Solution_button_Click(object sender, EventArgs e)
        {
            while(questions.Count != 0)
            {
                if (BFS(questions.Peek()))
                {
                    Point p = questions.Dequeue();
                    // test
                    Platter.Controls.Find(String.Format("S;{0};{1}", p.X, p.Y),true)[0].Text = map[p.Y][p.X];
                }
                else
                {
                    questions.Enqueue(questions.Dequeue());
                }
            }
        }

        private bool BFS(Point p)
        {
            bool bol = false;
            if (Horizontal_search(p)) bol = true;
            if (Vertical_search(p)) bol = true;
            return bol;
        }

        private bool Check(Point p)
        {
            if (p.X < 0) return false;
            if (p.X >= map[0].Length) return false;
            if (p.Y < 0) return false;
            if (p.Y >= map.Count) return false;
            if (map[p.Y][p.X] == "x") return false;
            // 判斷此數目前未知，直接略過接下來的流程
            if (map[p.Y][p.X] == "?") return false;
            return true;
        }

        //水平搜尋
        private bool Horizontal_search(Point p)
        {
            List<string> ar = new List<string>();
            ar.Add(map[p.Y][p.X]);
            Point tmp, d;
            tmp = p;
            d = new Point(-1, 0);
            while (Check(new Point(tmp.X+d.X,tmp.Y+d.Y)))
            {
                tmp = new Point(tmp.X + d.X, tmp.Y + d.Y);
                ar.Insert(0, map[tmp.Y][tmp.X]);
            }
            tmp = p;
            d = new Point(1, 0);
            while (Check(new Point(tmp.X + d.X, tmp.Y+d.Y)))
            {
                tmp = new Point(tmp.X + d.X, tmp.Y + d.Y);
                ar.Add(map[tmp.Y][tmp.X]);
            }
            return TryToFindTheAnswer(ar, p);
        }

        // 垂直搜尋
        private bool Vertical_search(Point p)
        {
            List<string> ar = new List<string>();
            ar.Add(map[p.Y][p.X]);
            Point tmp, d;
            tmp = p;
            d = new Point(0,-1);
            while (Check(new Point(tmp.X + d.X, tmp.Y + d.Y)))
            {
                tmp = new Point(tmp.X + d.X, tmp.Y + d.Y);
                ar.Insert(0, map[tmp.Y][tmp.X]);
            }
            tmp = p;
            d = new Point(0, 1);
            while (Check(new Point(tmp.X + d.X, tmp.Y + d.Y)))
            {
                tmp = new Point(tmp.X + d.X, tmp.Y + d.Y);
                ar.Add(map[tmp.Y][tmp.X]);
            }
            return TryToFindTheAnswer(ar,p);
        }

        private bool TryToFindTheAnswer(List<string> ar,Point p)
        {
            if (ar.Count < 5) return false;
            if (ar[3] != "=") ar.Reverse();
            ScriptControl sc = new ScriptControl();
            sc.Language = "javascript";

            string result = "";
            if(ar[0] == "?")
            {
                switch (ar[1])
                {
                    case "+":
                        result = String.Format("{0}-{1}", ar[4],ar[2]);
                        break;
                    case "-":
                        result = String.Format("{0}+{1}", ar[4], ar[2]);
                        break;
                    case "*":
                        result = String.Format("{0}/{1}", ar[4], ar[2]);
                        break;
                    case "/":
                        result = String.Format("{0}*{1}", ar[4], ar[2]);
                        break;
                }
            }
            if(ar[2] == "?")
            {
                switch (ar[1])
                {
                    case "+":
                        result = String.Format("{0}-{1}", ar[4], ar[0]);
                        break;
                    case "-":
                        result = String.Format("({0}-{1})*-1", ar[4], ar[0]);
                        break;
                    case "*":
                        result = String.Format("{0}/{1}", ar[4], ar[0]);
                        break;
                    case "/":
                        result = String.Format("{0}/{1}", ar[0], ar[4]);
                        break;
                }
            }
            if(ar[4] == "?")
            {
                result = String.Format("{0}{1}{2}", ar[0], ar[1], ar[2]);
            }
            result = Convert.ToString(sc.Eval(result));
            map[p.Y][p.X] = result;
            return true;
        }
    }
}
