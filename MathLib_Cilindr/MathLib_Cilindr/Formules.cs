using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MathLib_Cilindr
{
    [Serializable]
    public class Formules
    {
        public Formules(double _r, double _lamdaM, double _cM, double _roM, double _alfa, double _t, string _material, double _tp, double _tend, double _tbegin)
        {
            r = _r;
            lamdaM = _lamdaM;
            cM = _cM;
            roM = _roM;
            alfa = _alfa;
            t = _t;
            material = _material;
            t_p = _tp;
            t_end = _tend;
            t_beg = _tbegin;
        }
       
        #region ИСХОДНЫЕ ДАННЫЕ (ПРЯМАЯ ЗАДАЧА)
        //ИСХОДНЫЕ ДАННЫЕ (РАСЧЕТ ТЕМПЕРАТУР НАГРЕВА ЦИЛИНДРА)

        private double _r;
        /// <summary>
        /// РАДИУС ЦИЛИНДРА, м
        /// </summary>
        public double r
        {
            get { return _r; }
            set { _r = value; }
        }

        private double _lamdaM;
        /// <summary>
        /// КОЭФФИЦИЕНТ ТЕПЛОПРОВОДНОСТИ МАТЕРИАЛА, Вт/(м*К)
        /// </summary>
        public double lamdaM
        {
            get { return _lamdaM; }
            set { _lamdaM = value; }
        }

        private double _cM;
        /// <summary>
        /// КОЭФФИЦИЕНТ ТЕПЛОЁМКОСТИ МАТЕРИАЛА, Дж/(кг*К)
        /// </summary>
        public double cM
        {
            get { return _cM; }
            set { _cM = value; }
        }

        private double _roM;
        /// <summary>
        /// ПЛОТНОСТЬ МАТЕРИАЛА, кг/м3
        /// </summary>
        public double roM
        {
            get { return _roM; }
            set { _roM = value; }
        }

        private double _alfa;
        /// <summary>
        /// КОЭФФИЦИЕНТ ТЕПЛООТДАЧИ, Вт/(м2*К)
        /// </summary>
        public double alfa
        {
            get { return _alfa; }
            set { _alfa = value; }
        }

        private double _t;
        /// <summary>
        /// ВРЕМЯ НАГРЕВА, с
        /// </summary>
        public double t
        {
            get { return _t; }
            set { _t = value; }
        }


        private string _material;
        /// <summary>
        /// МАТЕРИАЛ ЦИЛИНДРА
        /// </summary>
        public string material
        {
            get { return _material; }
            set { _material = value; }
        }


        //ПОСТОЯННЫЕ ЗНАЧЕНИЯ 

        private double _Rc;
        /// <summary>
        /// ФУНКЦИЯ ЧИСЛА БИО Рц
        /// </summary>
        public double Rc
        {
            get { return _Rc; }
            set { _Rc = value; }
        }

        private double _Mc;
        /// <summary>
        /// ФУНКЦИЯ ЧИСЛА БИО Мц
        /// </summary>
        public double Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }

        private double _Nc;
        /// <summary>
        /// ФУНКЦИЯ ЧИСЛА БИО Nц
        /// </summary>
        public double Nc
        {
            get { return _Nc; }
            set { _Nc = value; }
        }

        private double _mu;
        /// <summary>
        /// ФУНКЦИЯ ЧИСЛА БИО МЮц
        /// </summary>
        public double mu
        {
            get { return _mu; }
            set { _mu = value; }
        }
        #endregion ИСХОДНЫЕ ДАННЫЕ (ПРЯМАЯ ЗАДАЧА) 

        #region ИСХОДНЫЕ ДАННЫЕ (ОБРАТНАЯ ЗАДАЧА)
        //ИСХОДНЫЕ ДАННЫЕ(РАСЧЁТ ВРЕМЕНИ НАГРЕВА ЦИЛИНДРА)

        private double _tp;
        ///<summary>
        ///Температура печи, град.С
        ///</summary>
        public double t_p
        {
            get { return _tp; }
            set { _tp = value; }
        }

        private double _t_end;
        ///<summary>
        ///Конечная температура нагрева цилиндра, град.С
        ///</summary>
        public double t_end
        {
            get { return _t_end; }
            set { _t_end = value; }
        }

        private double _t_beg;
        ///<summary>
        ///Начальная температура цилиндра, град.С
        ///</summary>
        public double t_beg
        {
            get { return _t_beg; }
            set { _t_beg = value; }
        }
        #endregion ИСХОДНЫЕ ДАННЫЕ (ОБРАТНАЯ ЗАДАЧА)

        //public static Formules GetData()
        //{
        //    return new Formules
        //    {
        //        _r = 0.055,
        //        _lamdaM = 42,
        //        _cM = 712,
        //        _roM = 7860,
        //        _alfa = 525,
        //        _t = 581,
        //        _t_beg = 20,
        //        _t_end = 1200,
        //        _tp = 1420,
        //        _material = "Сталь"

        //    };
        //}
        
        #region ВЫБОР ПАРАМЕТРОВ ЧИСЛА БИО
        public void Search()
        {
            switch (_Bi)
            {
                case 0.00:
                    mu = 0.000;
                    Rc = 1.000;
                    Mc = 1.000;
                    Nc = 1.000;
                    break;
                case 0.01:
                    mu = 0.020;
                    Rc = 0.997;
                    Mc = 1.000;
                    Nc = 1.002;
                    break;
                case 0.10:
                    mu = 0.195;
                    Rc = 0.975;
                    Mc = 1.000;
                    Nc = 1.025;
                    break;
                case 0.20:
                    mu = 0.381;
                    Rc = 0.951;
                    Mc = 0.999;
                    Nc = 1.048;
                    break;
                case 0.30:
                    mu = 0.557;
                    Rc = 0.927;
                    Mc = 0.998;
                    Nc = 1.071;
                    break;
                case 0.40:
                    mu = 0.725;
                    Rc = 0.904;
                    Mc = 0.997;
                    Nc = 1.093;
                    break;
                case 0.50:
                    mu = 0.885;
                    Rc = 0.881;
                    Mc = 0.995;
                    Nc = 1.114;
                    break;
                case 0.60:
                    mu = 1.037;
                    Rc = 0.859;
                    Mc = 0.993;
                    Nc = 1.134;
                    break;
                case 0.70:
                    mu = 1.182;
                    Rc = 0.837;
                    Mc = 0.992;
                    Nc = 1.154;
                    break;
                case 0.80:
                    mu = 1.320;
                    Rc = 0.816;
                    Mc = 0.989;
                    Nc = 1.172;
                    break;
                case 0.90:
                    mu = 1.452;
                    Rc = 0.796;
                    Mc = 0.987;
                    Nc = 1.190;
                    break;
                case 1.00:
                    mu = 1.577;
                    Rc = 0.776;
                    Mc = 0.984;
                    Nc = 1.207;
                    break;
                case 1.10:
                    mu = 1.577;
                    Rc = 0.776;
                    Mc = 0.984;
                    Nc = 1.207;
                    break;
                case 1.20:
                    mu = 1.577;
                    Rc = 0.776;
                    Mc = 0.984;
                    Nc = 1.207;
                    break;
                case 1.30:
                    mu = 1.577;
                    Rc = 0.776;
                    Mc = 0.984;
                    Nc = 1.207;
                    break;
                case 1.40:
                    mu = 2.123;
                    Rc = 0.686;
                    Mc = 0.970;
                    Nc = 1.281;
                    break;
                case 1.50:
                    mu = 2.123;
                    Rc = 0.686;
                    Mc = 0.970;
                    Nc = 1.281;
                    break;
                case 1.60:
                    mu = 2.123;
                    Rc = 0.686;
                    Mc = 0.970;
                    Nc = 1.281;
                    break;
                case 1.70:
                    mu = 2.123;
                    Rc = 0.686;
                    Mc = 0.970;
                    Nc = 1.281;
                    break;
                case 1.80:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 1.90:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 2.00:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 2.10:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 2.20:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 2.30:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 2.40:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 2.50:
                    mu = 2.558;
                    Rc = 0.610;
                    Mc = 0.953;
                    Nc = 1.338;
                    break;
                case 2.60:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 2.70:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 2.80:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 2.90:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 3.00:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 3.10:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 3.20:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 3.30:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 3.40:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 3.50:
                    mu = 3.199;
                    Rc = 0.492;
                    Mc = 0.923;
                    Nc = 1.419;
                    break;
                case 3.60:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 3.70:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 3.80:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 3.90:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 4.00:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 4.10:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 4.20:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 4.30:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 4.40:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 4.50:
                    mu = 3.641;
                    Rc = 0.407;
                    Mc = 0.895;
                    Nc = 1.470;
                    break;
                case 4.60:
                    mu = 3.959;
                    Rc = 0.345;
                    Mc = 0.872;
                    Nc = 1.503;
                    break;
                case 4.70:
                    mu = 3.959;
                    Rc = 0.345;
                    Mc = 0.872;
                    Nc = 1.503;
                    break;
                case 4.80:
                    mu = 3.959;
                    Rc = 0.345;
                    Mc = 0.872;
                    Nc = 1.503;
                    break;
                case 4.90:
                    mu = 3.959;
                    Rc = 0.345;
                    Mc = 0.872;
                    Nc = 1.503;
                    break;
                case 5.00:
                    mu = 3.959;
                    Rc = 0.345;
                    Mc = 0.872;
                    Nc = 1.503;
                    break;
                case 5.10:
                    mu = 3.959;
                    Rc = 0.345;
                    Mc = 0.872;
                    Nc = 1.503;
                    break;
                case 5.20:
                case 5.30:
                case 5.40:
                case 5.50:
                case 5.60:
                case 5.70:
                case 5.80:
                case 5.90:
                case 6.00:
                case 6.10:
                case 6.20:
                case 6.30:
                case 6.40:
                case 6.50:
                case 6.60:
                case 6.70:
                case 6.80:
                case 6.90:
                case 7.00:
                case 7.10:
                case 7.20:
                case 7.30:
                case 7.40:
                case 7.50:
                case 7.60:
                case 7.70:
                case 7.80:
                case 7.90:
                    mu = 3.959;
                    Rc = 0.345;
                    Mc = 0.872;
                    Nc = 1.503;
                    break;
                case 8.00:
                case 8.10:
                case 8.20:
                case 8.30:
                case 8.40:
                case 8.50:
                case 8.60:
                case 8.70:
                case 8.80:
                case 8.90:
                case 9.00:
                case 9.10:
                case 9.20:
                case 9.30:
                case 9.40:
                case 9.50:
                case 9.60:
                case 9.70:
                case 9.80:
                case 9.90:
                case 10.00:
                case 10.10:
                case 10.20:
                case 10.30:
                case 10.40:
                case 10.50:
                case 10.60:
                case 10.70:
                case 10.80:
                case 10.90:
                case 11.00:
                case 11.10:
                case 11.20:
                case 11.30:
                case 11.40:
                case 11.50:
                case 11.60:
                case 11.70:
                case 11.80:
                case 11.90:
                    mu = 4.750;
                    Rc = 0.191;
                    Mc = 0.804;
                    Nc = 1.568;
                    break;
                case 12.00:
                case 12.10:
                case 12.20:
                case 12.30:
                case 12.40:
                case 12.50:
                case 12.60:
                case 12.70:
                case 12.80:
                case 12.90:
                case 13.00:
                case 13.10:
                case 13.20:
                case 13.30:
                case 13.40:
                case 13.50:
                case 13.60:
                case 13.70:
                case 13.80:
                case 13.90:
                case 14.00:
                case 14.10:
                case 14.20:
                case 14.30:
                case 14.40:
                case 14.50:
                case 14.60:
                case 14.70:
                case 14.80:
                case 14.90:
                case 15.00:
                    mu = 5.066;
                    Rc = 0.130;
                    Mc = 0.772;
                    Nc = 1.585;
                    break;
                case 20.00:
                    mu = 5.235;
                    Rc = 0.099;
                    Mc = 0.754;
                    Nc = 1.592;
                    break;
                case 30.00:
                    mu = 5.411;
                    Rc = 0.066;
                    Mc = 0.736;
                    Nc = 1.596;
                    break;
                case 40.00:
                    mu = 5.501;
                    Rc = 0.050;
                    Mc = 0.724;
                    Nc = 1.599;
                    break;
                case 50.00:
                    mu = 5.557;
                    Rc = 0.040;
                    Mc = 0.718;
                    Nc = 1.600;
                    break;

            }

        }

        #endregion

        #region ПРОМЕЖУТОЧНЫЙ РАСЧЕТ

        //ПРОМЕЖУТОЧНЫЙ РАСЧЕТ

        private double _Bi;
        /// <summary>
        /// ЧИСЛО ТЕПЛОВОГО ПОДОБИЯ БИО
        /// </summary>
        public double Bi()
        {
            _Bi = Math.Round(((_alfa * _r) / _lamdaM), 1);
            return _Bi;
        }

        private double _A;
        /// <summary>
        /// КОЭФФИЦИЕНТ ТЕМПЕРАТУРОПРОВОДНОСТИ МАТЕРИАЛА, м2/с
        /// </summary>
        public double A()
        {
            _A = _lamdaM / (_cM * _roM);
            return _A;
        }

        private double _Fo;
        /// <summary>
        /// ЧИСЛО ПОДОБИЯ ФУРЬЕ
        /// </summary>
        public double Fo()
        {
            _Fo = (_A * _t) / Math.Pow(_r, 2);
            return _Fo;
        }
        #endregion ПРОМЕЖУТОЧНЫЙ РАСЧЕТ

        #region РАСЧЕТ НАГРЕВА ЦИЛИНДРА

        //РАСЧЕТ НАГРЕВА ЦИЛИНДРА



        private double _TDS;
        /// <summary>
        /// ОТНОСТИТЕЛЬНАЯ РАЗНОСТЬ ТЕМПЕРАТУР НА ПОВЕРХНОСТИ ЦИЛИНДРА
        /// </summary>

        public double TempDiff_Surface()
        {
            Search();
            _TDS = _Rc * Math.Exp(-_mu * _Fo);
            return _TDS;
        }

        private double _TDM;
        /// <summary>
        /// ОТНОСИТЕЛЬНАЯ РАЗНОСТЬ ТЕПЕРАТУР ДЛЯ МАССЫ ЦИЛИНДРА
        /// </summary>
        public double TempDiff_Mass()
        {
            Search();
            _TDM = _Mc * Math.Exp(-_mu * _Fo);
            return _TDM;
        }

        private double _TDC;
        /// <summary>
        /// ОТНОСИТЕЛЬНАЯ РАЗНОСТЬ ТЕМПЕРАТУР НА ОСИ ЦИЛИНДРА
        /// </summary>
        public double TempDiff_Centr()
        {
            Search();
            _TDC = _Nc * Math.Exp(-_mu * _Fo);
            return _TDC;
        }
        #endregion РАСЧЕТ НАГРЕВА ЦИЛИНДРА

        #region РАСЧЕТ ВРЕМЕНИ НАГРЕА ЦИЛИНДРА
        //РАСЧЕТ ВРЕМЕНИ НАГРЕВА

        private double _T_DS;
        ///<summary>
        ///РАЗНОСТЬ ТЕМПЕРАТУР НА ПОВЕРХНОСТИ ЦИЛИНДРА
        /// </summary>
        public double Temp_DiffSurface()
        {

            _T_DS = (_tp - _t_end) / (_tp - _t_beg);
            return _T_DS;
        }

        private double _TH;
        ///<summary>
        ///ВРЕМЯ НАГРЕВА ЦИЛИНДРА, с
        /// </summary>
        public double Time_heath()
        {
            Search();
            _TH = (Math.Pow(_r, 2) / (_A * _mu)) * Math.Log(_Rc / _T_DS);
            return _TH;
        }

        #endregion РАСЧЕТ ВРЕМЕНИ НАГРЕВА ЦИЛИНДРА

        public static FieldInfo[] GetFieldInfo()
        {
            FieldInfo[] fields = typeof(Formules).GetFields();

            return fields;
        }
    }
}
