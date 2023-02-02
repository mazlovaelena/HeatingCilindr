using System;
using System.Collections.Generic;
using System.IO;
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
using MathLib_Cilindr;

namespace CilindrWPF
{
    /// <summary>
    /// Логика взаимодействия для TimeInitial.xaml
    /// </summary>
    public partial class TimeInitial : UserControl
    {
        public TimeInitial()
        {
            InitializeComponent();

            List<double> textBoxesValues = new List<double>();
            try
            {
                ReadInitialFile();

                //Обработка ошибки при отсутствии файла исходных данных
                textBoxesValues.Add(Convert.ToDouble(R.Text));
                textBoxesValues.Add(Double.Parse(material.Text));
                textBoxesValues.Add(Convert.ToDouble(lamdaM.Text));
                textBoxesValues.Add(Convert.ToDouble(cM.Text));
                textBoxesValues.Add(Convert.ToDouble(roM.Text));
                textBoxesValues.Add(Convert.ToDouble(alfa.Text));
                textBoxesValues.Add(Convert.ToDouble(t_pech.Text));
                textBoxesValues.Add(Convert.ToDouble(t_begin.Text));
                textBoxesValues.Add(Convert.ToDouble(t_end.Text));
            }
            catch
            {
                if (textBoxesValues.Count == 0)
                {
                    GetDefaultValues();
                }

            }
        }

        public Formules Formules;
        public ResultTime TimeHeat;

        //Правило валидации строки
        public int Valid(string s)
        {
            string substr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString();
            int count = (s.Length - s.Replace(substr, "").Length) / substr.Length;
            return count;
        }

        //Метод ограничения ввода в поле (запрещает пользователю вводить буквы или иные символы)
        private void R_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0) || ((e.Text == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString()) && (Valid(((TextBox)sender).Text) < 1)));

            double val;
            if (!Double.TryParse(e.Text, out val) && e.Text != ",")
            {
                e.Handled = true;
            }
        }

        //Метод сохранения исходных данных во внешний файл
        public async void GetInitialFile()
        {
            Dictionary<string, object> Init = new Dictionary<string, object>();
            var init = new Dictionary<string, object>()
            {
                {"r", R.Text },
                {"material", material.Text },
                {"lamdaM", lamdaM.Text },
                {"cM", cM.Text },
                {"roM", roM.Text},
                {"alfa", alfa.Text },
                {"t_pech", t_pech.Text },
                {"t_begin", t_begin.Text },
                {"t_end", t_end.Text }

            };

            try
            {
                using (StreamWriter fs = new StreamWriter("InputTime.txt"))
                {
                    string[] strArray = init.Select(x => ("" + x.Key + "=" + x.Value + ";")).ToArray();

                    foreach (string s in strArray)
                    {
                        char[] d = s.ToCharArray();
                        await fs.WriteAsync(d);
                    }

                    fs.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Метод чтения данных из внешнего файла
        public async void ReadInitialFile()
        {
            try
            {
                string path = "InputTime.txt";

                using (StreamReader fstream = new StreamReader(path))
                {
                    string v = await fstream.ReadToEndAsync();
                    string d = v.ToString();
                    var dict = d.Split(';')
                    .Select(part => part.Split('='))
                    .Where(part => part.Length == 2)
                    .ToDictionary(sp => sp[0], sp => sp[1]);


                    R.Text = Convert.ToDouble(dict["r"]).ToString();
                    material.Text = Convert.ToString(dict["material"]);
                    lamdaM.Text = Convert.ToDouble(dict["lamdaM"]).ToString();
                    cM.Text = Convert.ToDouble(dict["cM"]).ToString();
                    roM.Text = Convert.ToDouble(dict["roM"]).ToString();
                    alfa.Text = Convert.ToDouble(dict["alfa"]).ToString();
                    t_pech.Text = Convert.ToDouble(dict["t_pech"]).ToString();
                    t_begin.Text = Convert.ToDouble(dict["t_begin"]).ToString();
                    t_end.Text = Convert.ToDouble(dict["t_end"]).ToString();

                }
            }
            catch
            {

            }
        }


        //Метод подстановки коэффициентов при выборе материала
        private void material_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (material.SelectedItem == Stal)
            {
                lamdaM.Text = Convert.ToString(42);
                cM.Text = Convert.ToString(712);
                roM.Text = Convert.ToString(7860);
            }
            else if (material.SelectedItem == Chygyn)
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
            else if (material.SelectedItem == Svinec)
            {
                lamdaM.Text = Convert.ToString(33.4);
                cM.Text = Convert.ToString(140);
                roM.Text = Convert.ToString(11300);
            }
        }

        //Метод получения воодных значений полей
        public void GetSourceValues()
        {
            double _r = Convert.ToDouble(R.Text);
            string _material = Convert.ToString(material.Text);
            double _lamdaM = Convert.ToDouble(lamdaM.Text);
            double _cM = Convert.ToDouble(cM.Text);
            double _roM = Convert.ToDouble(roM.Text);
            double _alfa = Convert.ToDouble(alfa.Text);
            double _t = 0;
            double _tp = Convert.ToDouble(t_pech.Text);
            double _tbegin = Convert.ToDouble(t_begin.Text);
            double _tend = Convert.ToDouble(t_end.Text);

            Formules = new Formules(_r, _lamdaM, _cM, _roM, _alfa, _t, _material, _tp, _tend, _tbegin);
        }

        //Метод вывода значений по умолчанию (при первом запуске прораммы)
        public void GetDefaultValues()
        {
            R.Text = "0,055";
            material.Text = "Сталь";
            lamdaM.Text = "42";
            cM.Text = "712";
            roM.Text = "7860";
            alfa.Text = "525";
            t_pech.Text = "1420";
            t_begin.Text = "20";
            t_end.Text = "1200";
        }

        //Метод нажатия на кнопку "Расчет"
        private void CalcTime_Click(object sender, RoutedEventArgs e)
        {
            List<string> textBoxesValues = new List<string>();

            textBoxesValues.Add(R.Text);
            textBoxesValues.Add(material.Text);
            textBoxesValues.Add(lamdaM.Text);
            textBoxesValues.Add(cM.Text);
            textBoxesValues.Add(roM.Text);
            textBoxesValues.Add(alfa.Text);
            textBoxesValues.Add(t_pech.Text);
            textBoxesValues.Add(t_begin.Text);
            textBoxesValues.Add(t_end.Text);

            //Проверка наличия пустых полей
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
                    MessageBox.Show("Заполните все поля и/или удалите пробелы!", "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Stop);
                    IsValuesCorrect = false;

                    counter = 0;
                    break;
                }
            }

            if (IsValuesCorrect == true)
            {
                GetSourceValues();

                GetInitialFile();

                Formules.Bi();
                Formules.A();
                Formules.Temp_DiffSurface();
                Formules.Time_heath();

                ResultTime.Formules = Formules;
                TimeHeat.GetCountValues();

            }

        }

       
    }
}
