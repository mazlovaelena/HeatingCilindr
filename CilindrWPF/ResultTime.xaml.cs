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
using MathLib_Cilindr;
using Microsoft.Win32;
using SpreadsheetLight;

namespace CilindrWPF
{
    /// <summary>
    /// Логика взаимодействия для ResultTime.xaml
    /// </summary>
    public partial class ResultTime : UserControl
    {
        public ResultTime()
        {
            InitializeComponent();
        }

        public static Formules Formules;

        public TimeInitial Initial;

        public int Valid(string s)
        {
            string substr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString();
            int count = (s.Length - s.Replace(substr, "").Length) / substr.Length;
            return count;
        }

        public void TextboxValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0) || ((e.Text == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString()) && (Valid(((System.Windows.Controls.TextBox)sender).Text) < 1)));
        }

        public void GetCountValues()
        {
            Bi.Text = Math.Round(Formules.Bi(), 1).ToString();
            A.Text = Math.Round(Formules.A(), 10).ToString("#,##0.000000000");
            T_DS.Text = Math.Round(Formules.Temp_DiffSurface(), 8).ToString();
            TH.Text = Math.Round(Formules.Time_heath(), 0).ToString("#,##0");
        }

        public void CreateFile(string patch)
        {
            if (patch != string.Empty)
            {
                using (SLDocument Doc = new SLDocument())
                {
                    Doc.SetCellValue("A1", "Исходные данные");
                    Doc.SetCellValue("A2", "Радиус цилиндра r, м");
                    Doc.SetCellValue("B2", Initial.R.Text);
                    Doc.SetCellValue("A3", "Материал цилиндра");
                    Doc.SetCellValue("B3", Initial.material.Text);
                    Doc.SetCellValue("A4", "Коэффициент теплопроводности материала цилиндра λм, Вт/(м*К)");
                    Doc.SetCellValue("B4", Initial.lamdaM.Text);
                    Doc.SetCellValue("A5", "Теплоемкость материала цилиндра См, Дж/(кг*К)");
                    Doc.SetCellValue("B5", Initial.cM.Text);
                    Doc.SetCellValue("A6", "Плотность материала цилиндра ρм, кг/м3");
                    Doc.SetCellValue("B6", Initial.roM.Text);
                    Doc.SetCellValue("A7", "Коэффициент теплоотдачи α, Вт/(м2*К)");
                    Doc.SetCellValue("B7", Initial.alfa.Text);
                    Doc.SetCellValue("A8", "Температура печи tпеч, ⁰C");
                    Doc.SetCellValue("B8", Initial.t_pech.Text);
                    Doc.SetCellValue("A9", "Начальная температура цилиндра tнач, ⁰C");
                    Doc.SetCellValue("B9", Initial.t_begin.Text);
                    Doc.SetCellValue("A10", "Конечная температура нагрева цилиндра tкон, ⁰C");
                    Doc.SetCellValue("B10", Initial.t_end.Text);

                    Doc.SetCellValue("D1", "Результаты расчета");
                    Doc.SetCellValue("D2", "Число теплового подобия Bi");
                    Doc.SetCellValue("E2", Bi.Text);
                    Doc.SetCellValue("D3", "Коэффициент температуропроводности материала а, м2/с");
                    Doc.SetCellValue("E3", A.Text);                  
                    Doc.SetCellValue("D4", "Время нагрева цилиндра до заданной конечной температуры τ, с");
                    Doc.SetCellValue("E4", TH.Text);

                    Doc.AutoFitColumn(1, 6);

                    Doc.SaveAs(patch);
                }
            }

        }

        private void ReportTime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Title = "Выберите папку";
                saveFile.DefaultExt = "xlsx";

                if (saveFile.ShowDialog() == true)
                {
                    CreateFile(saveFile.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
