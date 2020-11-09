using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace DayZCleaner {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    /*
     * Copyright (c) 2020 Ryan Broman
     */
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Process[] collection = Process.GetProcesses();
            foreach(var p in from Process p in collection
                             where p.ProcessName.Contains("DayZ")
                                   && p.ProcessName != Process.GetCurrentProcess().ProcessName
                             select p) {
                try {
                    p.Kill();
                } catch(Exception exc) {
                    MessageBox.Show(exc.ToString(), "Error, please report this to Whitename!");
                }
            }

            Int64 amountRemoved = 0;
            string sPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = new DirectoryInfo(sPath + "\\DayZ");


            foreach(var file in dir.EnumerateFiles("*.log")) {
                amountRemoved += file.Length;
                file.Delete();
            }

            foreach(var file in dir.EnumerateFiles("*.adm")) {
                amountRemoved += file.Length;
                file.Delete();
            }

            foreach(var file in dir.EnumerateFiles("*.rpt")) {
                amountRemoved += file.Length;
                file.Delete();
            }

            foreach(var file in dir.EnumerateFiles("*.mdmp")) {
                amountRemoved += file.Length;
                file.Delete();
            }

            MessageBox.Show($"DayZ cleanup complete! Removed {Math.Round(Convert.ToDouble(amountRemoved)/1000000000, 2)}GB of log data.", "Operation Complete");
        }
    }
}
