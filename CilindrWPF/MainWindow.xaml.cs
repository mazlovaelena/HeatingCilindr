﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CilindrWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TempInitial Initial;

        public MainWindow()
        {
            InitializeComponent();

            Initial = new TempInitial();
            
            TempHeat.TempHeat = TempResult;
            TempResult.Initial = TempHeat;

            TimeHeat.TimeHeat = TimeResult;
            TimeResult.Initial = TimeHeat;
           
        }       

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Inform.Visibility = Visibility.Visible;
            TempHeat.Visibility = Visibility.Hidden;
            TimeHeat.Visibility = Visibility.Hidden;
            TempResult.Visibility = Visibility.Hidden;
            TimeResult.Visibility = Visibility.Hidden;
            Temp.Visibility = Visibility.Visible;
            TempRes.Visibility = Visibility.Hidden;
            Time.Visibility = Visibility.Visible;
            TimeRes.Visibility = Visibility.Hidden;

        }

        private void Temp_Click(object sender, RoutedEventArgs e)
        {
            Inform.Visibility = Visibility.Hidden;
            TempHeat.Visibility = Visibility.Visible;
            TimeHeat.Visibility = Visibility.Hidden;
            TempResult.Visibility = Visibility.Hidden;
            TimeResult.Visibility = Visibility.Hidden;
            Temp.Visibility = Visibility.Hidden;
            TempRes.Visibility = Visibility.Visible;            
            Time.Visibility = Visibility.Visible;
            TimeRes.Visibility = Visibility.Hidden;
        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {
            Inform.Visibility = Visibility.Hidden;
            TempHeat.Visibility = Visibility.Hidden;
            TimeHeat.Visibility = Visibility.Visible;
            TempResult.Visibility = Visibility.Hidden;
            TimeResult.Visibility = Visibility.Hidden;
            Time.Visibility = Visibility.Hidden;
            TimeRes.Visibility = Visibility.Visible;
            Temp.Visibility = Visibility.Visible;
            TempRes.Visibility = Visibility.Hidden;
        }


        private void TempRes_Click(object sender, RoutedEventArgs e)
        {
            Inform.Visibility = Visibility.Hidden;
            TempHeat.Visibility = Visibility.Hidden;
            TimeHeat.Visibility = Visibility.Hidden;
            TempResult.Visibility = Visibility.Visible;
            TimeResult.Visibility = Visibility.Hidden;
            Temp.Visibility = Visibility.Visible;
            TempRes.Visibility = Visibility.Hidden;
        }

        private void TimeRes_Click(object sender, RoutedEventArgs e)
        {
            Inform.Visibility = Visibility.Hidden;
            TempHeat.Visibility = Visibility.Hidden;
            TimeHeat.Visibility = Visibility.Hidden;
            TempResult.Visibility = Visibility.Hidden;
            TimeResult.Visibility = Visibility.Visible;
            Time.Visibility = Visibility.Visible;
            TimeRes.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            TempHeat.GetInitialFile();
            TimeHeat.GetInitialFile();
            Application.Current.Shutdown();
        }
    }
}
