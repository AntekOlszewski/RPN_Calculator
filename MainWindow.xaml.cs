using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        ArrayList dzialanie = new ArrayList();
        public void Ins(string a)
        {
            if (Wynik.Text == "0")
                Wynik.Text = ""+a;
            else
                Wynik.Text += a;
        }
        public void Insdz(string a)
        {
            char t= Wynik.Text[Wynik.Text.Length - 1];
            if (t!='!' & t!='^' & t!= '√' & t!='/' & t!='*' & t!='-' & t!='+')
                Wynik.Text += a;
            else
                Wynik.Text = Wynik.Text.Substring(0, Wynik.Text.Length - 1) + a;

        }
        public void ConvertToArray()
        {
            string a = Wynik.Text;
            string poj="";
            int i = 0;
            while(i<a.Length)
            {
                if(int.TryParse(a.Substring(i,1),out int x) || a[i]==',')
                {
                    poj += a[i];
                }
                else if (a[i]== 'π')
                {
                    dzialanie.Add(Math.PI.ToString());
                }               
                else
                {
                    dzialanie.Add(poj);
                    poj = "";
                    dzialanie.Add(a[i]);
                }
                if (i == (a.Length - 1) && int.TryParse(a.Substring(i, 1), out int p))
                {
                    dzialanie.Add(poj);
                    poj = "";
                }
                i++;
            }
        }

        public int Silnia(int a)
        {
            int ret = a;
            if (a == 0) ret = 1;
            else
            {
                for (int i=a-1; i>0; i--)
                {
                    ret *= i;
                }
            }
            return ret;
        }
        public void Calulate()
        {
            ConvertToArray();
            Dictionary<string, int> kolejnosc = new Dictionary<string, int>();
            kolejnosc.Add("!", 4);
            kolejnosc.Add("^", 3);
            kolejnosc.Add("√", 3);
            kolejnosc.Add("/", 2);
            kolejnosc.Add("*", 2);
            kolejnosc.Add("-", 1);
            kolejnosc.Add("+", 1);
            kolejnosc.Add("(", 0);
            Queue kolejka = new Queue();
            Stack wyjscie = new Stack();
            string str; 
            for(int i = 0; i<dzialanie.Count; i++)
            {
                str = dzialanie[i].ToString();
                if (double.TryParse(str, out double n))
                {
                    kolejka.Enqueue(n);
                }
                else if (str == "!" | str == "√")
                {
                    wyjscie.Push(str);
                }
                else if (str == "/" | str == "*" | str == "+" | str == "-")
                {
                    if (wyjscie.Count > 0)
                    {
                        while (kolejnosc[str] <= kolejnosc[wyjscie.Peek().ToString()])
                        {                        
                            kolejka.Enqueue(wyjscie.Pop());
                            if (wyjscie.Count == 0) break;
                        }
                    }
                    wyjscie.Push(str);

                }
                else if (str == "^") 
                {
                    if (wyjscie.Count > 0)
                    {
                        while (kolejnosc[str] < kolejnosc[wyjscie.Peek().ToString()])
                        {
                            kolejka.Enqueue(wyjscie.Pop());
                            if (wyjscie.Count == 0) break;
                        }
                    }
                    wyjscie.Push(str);
                }
                else if (str == "(")
                {
                    wyjscie.Push("(");
                }
                else if (str == ")")
                {
                    while (wyjscie.Peek().ToString() != "(")
                    {
                        kolejka.Enqueue(wyjscie.Pop());
                    }
                    wyjscie.Pop();
                    if (wyjscie.Peek().ToString() == "√" || wyjscie.Peek().ToString() == "!")
                    {
                        kolejka.Enqueue(wyjscie.Pop());
                    }
                }
                
            }
            while (wyjscie.Count>0)
            {
                kolejka.Enqueue(wyjscie.Pop());
            }
            OPN(kolejka);
            dzialanie.Clear();
        }
        public void OPN(Queue a)
        {
            Stack wynik = new Stack();
            string tem;
            while (a.Count>0)
            {
                tem = a.Dequeue().ToString();
                if (double.TryParse(tem, out double n))
                {
                    wynik.Push(n);
                }
                else if (tem =="^" || tem == "/" || tem == "*" || tem == "-"|| tem == "+")
                {
                    double x = double.Parse(wynik.Pop().ToString());
                    double y = double.Parse(wynik.Pop().ToString());
                    if (tem == "^") wynik.Push(Math.Pow(y, x));
                    else if (tem == "*") wynik.Push(y * x);
                    else if (tem == "/") wynik.Push(y / x);
                    else if (tem == "-") wynik.Push(y - x);
                    else if (tem == "+") wynik.Push(y + x);
                }
                else
                {
                    if (tem == "!") wynik.Push(Silnia(int.Parse(wynik.Pop().ToString())));
                    else if (tem == "√") wynik.Push(Math.Sqrt(double.Parse(wynik.Pop().ToString())));
                }
            }
            Wynik.Text = wynik.Pop().ToString();
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Ins("0");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ins("1");
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Ins("2");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Ins("3");
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Ins("4");
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Ins("5");
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Ins("6");
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Ins("7");
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Ins("8");
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Ins("9");
        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (Wynik.Text == "") Wynik.Text = "0,";
            else Wynik.Text += ",";             
        }
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Wynik.Text = "0";
        }
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (Wynik.Text.Length > 1) Wynik.Text = Wynik.Text.Substring(0, Wynik.Text.Length - 1);
            else Wynik.Text = "0";
        }
        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            Ins("(");
        }
        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            Ins(")");
        }
        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            Ins("π");
        }
        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            Insdz("+");
        }
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            if (Wynik.Text != "0") Insdz("^(0-1)");
        }
        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            Insdz("-");
        }
        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            if(Wynik.Text == "0") Wynik.Text += "!";
            else Ins("!");
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            Insdz("*");
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            Insdz("^");
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            Ins("√");
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            Insdz("/");
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            Calulate();           
        }
    }
}
