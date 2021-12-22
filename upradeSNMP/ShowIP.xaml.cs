using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace upradeSNMP
{
    /// <summary>
    /// Логика взаимодействия для ShowIP.xaml
    /// </summary>
    public partial class ShowIP : Window
    {
        readonly List<string> _IP;
        public ShowIP(List<string> ip)
        {
            _IP = ip;
            InitializeComponent();
            lstboxIP.ItemsSource = _IP;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
