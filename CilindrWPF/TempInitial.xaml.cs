using MathLib_Cilindr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static MathLib_Cilindr.Formules;


namespace CilindrWPF
{
    /// <summary>
    /// Логика взаимодействия для TempInitial.xaml
    /// </summary>
    public partial class TempInitial : UserControl
    {
        public TempInitial()
        {
            InitializeComponent();

            GetDefaultValues();
        }

        public Formules Formules;
        public ResultTemp TempHeat;
       

        public int Valid(string s)
        {
            string substr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString();
            int count = (s.Length - s.Replace(substr, "").Length) / substr.Length;
            return count;
        }

        public void TextboxValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0) || ((e.Text == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString()) && (Valid(((TextBox)sender).Text) < 1)));
        }

        public void Deserialize()
        {
            try             
            {
                JsonSerializer formatter = new JsonSerializer();
                using (StreamReader fs = new StreamReader("InputTemp.txt"))
                {
                    Dictionary<string, object> Data = (Dictionary<string, object>)formatter.Deserialize(fs, typeof(Dictionary<string, object>));

                    R.Text = Convert.ToDouble(Data["r"]).ToString();
                    material.Text = Convert.ToString(Data["material"]);
                    lamdaM.Text = Convert.ToDouble(Data["lamdaM"]).ToString();
                    cM.Text = Convert.ToDouble(Data["cM"]).ToString();
                    roM.Text = Convert.ToDouble(Data["roM"]).ToString();
                    alfa.Text = Convert.ToDouble(Data["alfa"]).ToString();
                    t.Text = Convert.ToDouble(Data["t"]).ToString();
                }
            }
            catch 
            { 

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<double> textBoxesValues = new List<double>();

            try
            {
                textBoxesValues.Add(Convert.ToDouble(R.Text));
                textBoxesValues.Add(Double.Parse(material.Text));
                textBoxesValues.Add(Convert.ToDouble(lamdaM.Text));
                textBoxesValues.Add(Convert.ToDouble(cM.Text));
                textBoxesValues.Add(Convert.ToDouble(roM.Text));
                textBoxesValues.Add(Convert.ToDouble(alfa.Text));
                textBoxesValues.Add(Convert.ToDouble(t.Text));
             
            }
            catch
            {
                if (textBoxesValues.Count == 0)
                {
                    GetDefaultValues(); 
                }
            }

        }      

        public void Serialize()
        {
            FieldInfo[] fields = Formules.GetFieldInfo();

            Dictionary<string, object> Data = new Dictionary<string, object>();

            foreach (FieldInfo Info in fields)
            {
                Data.Add(Info.Name, Info.GetValue(Formules));
            }

            JsonSerializer formatter = new JsonSerializer();
            using (StreamWriter fs = new StreamWriter("InputTemp.txt"))
            {
                formatter.Serialize(fs, Data);
            }
        }

        private void material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(material.SelectedItem == Stal)
            {
                lamdaM.Text = Convert.ToString(42);
                cM.Text = Convert.ToString(712);
                roM.Text = Convert.ToString(7860);
            }
            else if(material.SelectedItem == Chugun)
            {
                lamdaM.Text = Convert.ToString(62.8);
                cM.Text = Convert.ToString(541);
                roM.Text = Convert.ToString(7000);
            }
            else if (material.SelectedItem == Olovo)
            {
                lamdaM.Text = Convert.ToString(66.11);
                cM.Text = Convert.ToString(234);
                roM.Text = Convert.ToString(7300);
            }
            else if(material.SelectedItem == Svinec)
            {
                lamdaM.Text = Convert.ToString(33.4);
                cM.Text = Convert.ToString(140);
                roM.Text = Convert.ToString(11300);
            }
            
        }
        public void GetSourceValues()
        {
            double _r = Convert.ToDouble(R.Text);
            string _material = Convert.ToString(material.Text);
            double _lamdaM = Convert.ToDouble(lamdaM.Text);
            double _cM = Convert.ToDouble(cM.Text);
            double _roM = Convert.ToDouble(roM.Text);
            double _alfa = Convert.ToDouble(alfa.Text);
            double _t = Convert.ToDouble(t.Text);
            double _tp = 0;
            double _tbegin = 0;
            double _tend = 0;

            Formules = new Formules(_r, _lamdaM, _cM, _roM, _alfa, _t, _material, _tp, _tend, _tbegin);
         
        }

        public void GetDefaultValues()
        {
            R.Text = "0,055";
            material.Text = "Сталь";
            lamdaM.Text = "42";
            cM.Text = "712";
            roM.Text = "7860";
            alfa.Text = "525";
            t.Text = "581";
        }

        private void CalcTemp_Click(object sender, RoutedEventArgs e)
        {
            List<string> textBoxesValues = new List<string>();

            textBoxesValues.Add(R.Text);
            textBoxesValues.Add(material.Text);
            textBoxesValues.Add(lamdaM.Text);
            textBoxesValues.Add(cM.Text);
            textBoxesValues.Add(roM.Text);
            textBoxesValues.Add(alfa.Text);
            textBoxesValues.Add(t.Text);
           
            bool IsValuesCorrect = true;

            foreach (string s in textBoxesValues)
            {
                string[] filterEmpty = s.Split(new char[] { ' ' });
                int counter = 0;
                foreach (string n in filterEmpty)
                {
                    if (n == "")
                    {
                        counter++;
                    }
                }

                if (counter >= 1 || s == String.Empty || s == ",")
                {
                    MessageBox.Show("Заполните все поля!", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Stop);
                    IsValuesCorrect = false;

                    counter = 0;
                    break;
                }
            }

            if (IsValuesCorrect == true)
            {
                GetSourceValues();

                Serialize();

                Formules.Bi();
                Formules.A();
                Formules.Fo();
                Formules.TempDiff_Surface();
                Formules.TempDiff_Mass();
                Formules.TempDiff_Centr();

                ResultTemp.Formules = Formules;
                TempHeat.GetCountValues();

            }
           
        }

        
    }
}
