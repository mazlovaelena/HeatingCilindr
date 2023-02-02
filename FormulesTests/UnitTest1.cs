using NUnit.Framework;
using MathLib_Cilindr;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System;


namespace FormulesTests
{
    public class Tests
    {
        private string fileName = "ForTest.xlsx";
        Microsoft.Office.Interop.Excel.Application objExcel = null;
        Microsoft.Office.Interop.Excel.Workbook WorkBook = null;

        private object oMissing = System.Reflection.Missing.Value;

        [Test]
        public void CalculationTest1()
        {
            double _r = 0.055;
            double _lamdaM = 42;
            double _cM = 712;
            double _roM = 7860;
            double _alfa = 525;
            double _t = 581;
            string _material = "Сталь";
            double _t_pech = 1420;
            double _t_beg = 20;
            double _t_end = 1200;

            Formules Form = new Formules(_r, _lamdaM, _cM, _roM, _alfa, _t, _material, _t_end, _t_beg, _t_pech);

            try
            {
                objExcel = new Microsoft.Office.Interop.Excel.Application();
                WorkBook = objExcel.Workbooks.Open(
                            Directory.GetCurrentDirectory() + "\\" + fileName);
                Microsoft.Office.Interop.Excel.Worksheet WorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)WorkBook.Sheets["Прямая задача"];

                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[2, 7]).Value2 = Form.r;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[3, 7]).Value2 = Form.lamdaM;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[4, 7]).Value2 = Form.cM;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[5, 7]).Value2 = Form.roM;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[6, 7]).Value2 = Form.alfa;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[7, 7]).Value2 = Form.t;


                // отобразить в журнале тестирования
                Console.WriteLine("--- Исходные данные");
                Console.WriteLine("Радиус цилиндра: {0}", Form.r.ToString());
                Console.WriteLine("Коэффициент теплопроводности материала цилиндра: {0}", Form.lamdaM.ToString());
                Console.WriteLine("Теплоемкость материала цилиндра: {0}", Form.cM.ToString());
                Console.WriteLine("Плотность материала цилиндра: {0}", Form.roM.ToString());
                Console.WriteLine("Коэффициент теплоотдачи: {0}", Form.alfa.ToString());
                Console.WriteLine("Время нагрева цилиндра: {0}", Form.t.ToString());

                double Bi = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[2, 17]).Value.ToString());
                double A = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[3, 17]).Value.ToString());
                double Fo = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[4, 17]).Value.ToString());
                double TempDiff_Centr = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[5, 17]).Value.ToString());
                double TempDiff_Mass = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[6, 17]).Value.ToString());
                double TempDiff_Surface = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[7, 17]).Value.ToString());

                Assert.AreEqual(Bi, Math.Round(Form.Bi(), 1), 0.001);
                System.Diagnostics.Debug.WriteLine("Число теплового подобия - BI: expected =" +
                            Bi + "; actual=" + Math.Round(Form.Bi(), 1));

                Assert.AreEqual(A, Math.Round(Form.A(), 7), 0.001);
                System.Diagnostics.Debug.WriteLine("Коэффициент температуропроводности материала - А: expected =" +
                            A + "; actual=" + Math.Round(Form.A(), 7));

                Assert.AreEqual(Fo, Math.Round(Form.Fo(), 3), 0.001);
                System.Diagnostics.Debug.WriteLine("Число подобия Фурье - Fo: expected =" +
                            Fo + "; actual=" + Math.Round(Form.Fo(), 3));

                Assert.AreEqual(TempDiff_Centr, Math.Round(Form.TempDiff_Centr(), 4), 0.001);
                System.Diagnostics.Debug.WriteLine("Разность температур на оси цилиндра: expected =" +
                            TempDiff_Centr + "; actual=" + Math.Round(Form.TempDiff_Centr(), 4));

                Assert.AreEqual(TempDiff_Mass, Math.Round(Form.TempDiff_Mass(), 4), 0.001);
                System.Diagnostics.Debug.WriteLine("Разность температур для массы цилиндра: expected =" +
                            TempDiff_Mass + "; actual=" + Math.Round(Form.TempDiff_Mass(), 4));

                Assert.AreEqual(TempDiff_Surface, Math.Round(Form.TempDiff_Surface(), 4), 0.001);
                System.Diagnostics.Debug.WriteLine("Разность температур на поверхности цилиндра: expected =" +
                            TempDiff_Surface + "; actual=" + Math.Round(Form.TempDiff_Surface(), 4));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Число теплового подобия, метод Bi(): expected = " +
                            Bi + "; actual=" + Math.Round(Form.Bi(), 1));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Коэффициент температуропроводности материала, метод A(): expected = " +
                            A + "; actual=" + Math.Round(Form.A(), 7));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Число подобия Фурье, метод Fo(): expected = " +
                            Fo + "; actual=" + Math.Round(Form.Fo(), 3));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Разность температур на оси цилиндра, метод TempDiff_Centr(): expected = " +
                            TempDiff_Centr + "; actual=" + Math.Round(Form.TempDiff_Centr(), 4));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Разность температур для массы цилиндра, метод TempDiff_Mass(): expected = " +
                            TempDiff_Mass + "; actual=" + Math.Round(Form.TempDiff_Mass(), 4));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Разность температур на поверхности цилиндра, метод TempDiff_Surface(): expected = " +
                            TempDiff_Surface + "; actual=" + Math.Round(Form.TempDiff_Surface(), 4));

                WorkBook.Close(false, null, null);
                objExcel.Quit();

            }
            catch
            {
                if (WorkBook != null) WorkBook.Close(false, null, null);
                if (objExcel != null) objExcel.Quit();
            }
            finally
            {
                //WorkBook.Close(false, null, null);
                //objExcel.Quit();
            }
        }

        [Test]
        public void CalculationTest2()
        {
            double _r = 0.055;
            double _lamdaM = 42;
            double _cM = 712;
            double _roM = 7860;
            double _alfa = 525;
            double _t = 581;
            string _material = "Сталь";
            double _t_pech = 1420;
            double _t_beg = 20;
            double _t_end = 1200;

            Formules Form = new Formules(_r, _lamdaM, _cM, _roM, _alfa, _t, _material, _t_end, _t_beg, _t_pech);

            try
            {
                objExcel = new Microsoft.Office.Interop.Excel.Application();
                WorkBook = objExcel.Workbooks.Open(
                            Directory.GetCurrentDirectory() + "\\" + fileName);
                Microsoft.Office.Interop.Excel.Worksheet WorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)WorkBook.Sheets["Обратная задача"];

                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[2, 7]).Value2 = Form.r;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[4, 7]).Value2 = Form.lamdaM;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[5, 7]).Value2 = Form.cM;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[6, 7]).Value2 = Form.roM;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[7, 7]).Value2 = Form.alfa;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[8, 7]).Value2 = Form.t_p;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[9, 7]).Value2 = Form.t_beg;
                ((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[10, 7]).Value2 = Form.t_end;


                // отобразить в журнале тестирования
                Console.WriteLine("--- Исходные данные");
                Console.WriteLine("Радиус цилиндра: {0}", Form.r.ToString());
                Console.WriteLine("Коэффициент теплопроводности материала цилиндра: {0}", Form.lamdaM.ToString());
                Console.WriteLine("Теплоемкость материала цилиндра: {0}", Form.cM.ToString());
                Console.WriteLine("Плотность материала цилиндра: {0}", Form.roM.ToString());
                Console.WriteLine("Коэффициент теплоотдачи: {0}", Form.alfa.ToString());
                Console.WriteLine("Температура печи: {0}", Form.t_p.ToString());
                Console.WriteLine("Начальная температура цилиндра: {0}", Form.t_beg.ToString());
                Console.WriteLine("Конечная температура цилиндра: {0}", Form.t_end.ToString());

                double Temp_DiffSurface = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[2, 17]).Value.ToString());
                double Bi = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[3, 17]).Value.ToString());
                double A = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[4, 17]).Value.ToString());
                double Time_heath = double.Parse(((Microsoft.Office.Interop.Excel.Range)WorkSheet.Cells[5, 17]).Value.ToString());


                Assert.AreEqual(Temp_DiffSurface, Math.Round(Form.Temp_DiffSurface(), 4), 0.001);
                System.Diagnostics.Debug.WriteLine("Разность температур на поверхности цилиндра: expected =" +
                            Temp_DiffSurface + "; actual=" + Math.Round(Form.Temp_DiffSurface(), 4));


                Assert.AreEqual(Bi, Math.Round(Form.Bi(), 1), 0.001);
                System.Diagnostics.Debug.WriteLine("Число теплового подобия - BI: expected =" +
                            Bi + "; actual=" + Math.Round(Form.Bi(), 1));

                Assert.AreEqual(A, Math.Round(Form.A(), 7), 0.001);
                System.Diagnostics.Debug.WriteLine("Коэффициент температуропроводности материала - А: expected =" +
                            A + "; actual=" + Math.Round(Form.A(), 7));

                Assert.AreEqual(Time_heath, Math.Round(Form.Time_heath(), 1), 0.001);
                System.Diagnostics.Debug.WriteLine("Время нагрева цилиндра - Time_heath: expected =" +
                            Time_heath + "; actual=" + Math.Round(Form.Time_heath(), 1));


                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Разность температур на поверхности цилиндра, метод Temp_DiffSurface(): expected = " +
                            Temp_DiffSurface + "; actual=" + Math.Round(Form.Temp_DiffSurface(), 4));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Число теплового подобия, метод Bi(): expected = " +
                            Bi + "; actual=" + Math.Round(Form.Bi(), 1));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Коэффициент температуропроводности материала, метод A(): expected = " +
                            A + "; actual=" + Math.Round(Form.A(), 7));

                // отобразить в журнале тестирования
                Console.WriteLine("");
                Console.WriteLine("--- Результаты расчета");
                Console.WriteLine("Время нагрева цилиндра, метод Time_heath(): expected = " +
                            Time_heath + "; actual=" + Math.Round(Form.Time_heath(), 1));



                WorkBook.Close(false, null, null);
                objExcel.Quit();

            }
            catch
            {
                if (WorkBook != null) WorkBook.Close(false, null, null);
                if (objExcel != null) objExcel.Quit();
            }
            finally
            {
                //WorkBook.Close(false, null, null);
                //objExcel.Quit();
            }


        }
    }
}