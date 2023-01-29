using MathLib_Cilindr;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SpreadsheetLight;

namespace CilindrWPF
{
    /// <summary>
    /// Логика взаимодействия для ResultTemp.xaml
    /// </summary>
    public partial class ResultTemp : UserControl
    {
        public static Formules Formules;

        public TempInitial Initial;

        public ResultTemp()
        {
            InitializeComponent();

            Serialize();

        }

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

        public void Serialize()
        {
            FieldInfo[] fields = Formules.GetFieldInfo();

            Dictionary<string, object> Data = new();

            foreach (FieldInfo Info in fields)
            {
                Data.Add(Info.Name, Info.GetValue(Formules));
            }

            JsonSerializer formatter = new();

            using StreamWriter fs = new("InputTemp.txt");
            formatter.Serialize(fs, Data);
        }

        public void GetCountValues()
        {
            Bi.Text = Math.Round(Formules.Bi(), 1).ToString();
            A.Text = Math.Round(Formules.A(), 10).ToString("#,##0.000000000");
            Fo.Text = Math.Round(Formules.Fo(), 3).ToString();
            TDS.Text = Math.Round(Formules.TempDiff_Surface(), 8).ToString();
            TDM.Text = Math.Round(Formules.TempDiff_Mass(), 8).ToString();
            TDC.Text = Math.Round(Formules.TempDiff_Centr(), 8).ToString();
        }

        public void CreateFile(string patch)
        {
            if(patch != string.Empty)
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
                    Doc.SetCellValue("A8", "Время нагрева цилиндра τ, с");
                    Doc.SetCellValue("B8", Initial.t.Text);

                    Doc.SetCellValue("D1", "Результаты расчета");
                    Doc.SetCellValue("D2", "Число теплового подобия Bi");
                    Doc.SetCellValue("E2", Bi.Text);
                    Doc.SetCellValue("D3", "Коэффициент температуропроводности материала а, м2/с");
                    Doc.SetCellValue("E3", A.Text);
                    Doc.SetCellValue("D4", "Число подобия Фурье Fo");
                    Doc.SetCellValue("E4", Fo.Text);
                    Doc.SetCellValue("D5", "Относительная разность температур для оси цилиндра Θс");
                    Doc.SetCellValue("E5", TDC.Text);
                    Doc.SetCellValue("D6", "Относительная разность температур для массы цилиндра Θм");
                    Doc.SetCellValue("E6", TDM.Text);
                    Doc.SetCellValue("D7", "Относительная разность температур на поверхности цилиндра Θп");
                    Doc.SetCellValue("E7", TDM.Text);

                    Doc.AutoFitColumn(1, 6);
                   
                    Doc.SaveAs(patch);
                }
            }
            
        }

        private void ReportTemp_Click(object sender, RoutedEventArgs e)
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
