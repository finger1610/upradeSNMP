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
using System.IO;


namespace upradeSNMP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> _devices;
       List <string> oid;


        public MainWindow()
        {
            InitializeComponent();

             _devices = new List<string>();
          
            oid = new List<string>();
        }

     

        void checkComlete()
        {
            if (_devices.Count!=0&&
                txtBoxIPaddrTFTP.Text != "" &&
              txtBoxCodeReboot.Text != ""&&
              txtBoxCommunity.Text!=""&&
              oid.Count>1)
                btnSend.IsEnabled = true;

            else
                btnSend.IsEnabled = false;
        }

        private void txtBoxIPaddrTFTP_MouseEnter(object sender, MouseEventArgs e)
        {
            txtBoxFile.SelectAll();
            checkComlete();
        }
        private void txtBoxFile_MouseEnter(object sender, MouseEventArgs e)
        {
            txtBoxFile.SelectAll();
            checkComlete();
        }
        private void txtBoxCodeReboot_MouseEnter(object sender, MouseEventArgs e)
        {
            checkComlete();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            AddOID formADD = new AddOID(oid);
            formADD.ShowDialog();
            if (oid.Count>2)
            {
                lblGreenOID.Visibility = Visibility.Visible;
            }
            txtBoxCommunity.SelectAll();
            checkComlete();

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Переданы параметры:\n " +
            //    $"сервер: {oid[1]}|{txtBoxFile.Text}\n" +
            //    $"{oid[0]}|{txtBoxIPaddrTFTP.Text}\n" +
            //    $" {oid[2]}|i {txtBoxCodeReboot.Text}\n " +
            //    $"comm {txtBoxCommunity}\n " +
            //    $"vers {comboBoxVersion.SelectedItem.ToString()}");

            SNMPSet obj = new SNMPSet(txtBoxFile.Text,txtBoxIPaddrTFTP.Text,
                                      txtBoxCodeReboot.Text, oid, txtBoxCommunity.Text,
                                      Convert.ToInt32(comboBoxVersion.Text));

            lblProgress.Visibility = Visibility.Visible;
            prgBar.Visibility = Visibility.Visible;
            prgBar.Maximum = _devices.Count;
            lblProgress.Visibility = Visibility.Visible;

            foreach (string ip in _devices)
            {
                lblProgress.Content = "Отправка: " + ip;
                obj.start(ip);
               
             

                    prgBar.Value++;
               
                lblProgress.Content =prgBar.Value + "/" + _devices.Count;
                this.UpdateLayout();
                
            }
         
        }

        private void txtBoxCommunity_MouseEnter(object sender, MouseEventArgs e)
        {
            checkComlete();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = fileDialog.FileName;
                    txtboxFileDialog.Text = file;
                   txtboxFileDialog.ToolTip = file;
                    readFile();
                    lblFindIP.Visibility = Visibility.Visible;
                    lblFindIP.Content = $"Найдено " + _devices.Count + " строк";
                    btnCheckIPList.IsEnabled = true;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    txtboxFileDialog.Text = "";
                    txtboxFileDialog.ToolTip = null;

                    lblFindIP.Visibility = Visibility.Hidden;
                    btnCheckIPList.IsEnabled = false;
                    break;
            }
            checkComlete();
        }

        private void btnCheckIPList_Click(object sender, RoutedEventArgs e)
        {
            ShowIP showIP = new ShowIP(_devices);
            showIP.ShowDialog();
        }

        private void txtboxFileDialog_MouseEnter(object sender, MouseEventArgs e)
        {
            if (txtboxFileDialog.Text!="")
            {
                readFile();
                lblFindIP.Visibility = Visibility.Visible;
                lblFindIP.Content = $"Найдено " + _devices.Count + " строк";
                btnCheckIPList.IsEnabled = true;
                checkComlete();
            }
            else
            {
                lblFindIP.Visibility = Visibility.Hidden;
                btnCheckIPList.IsEnabled = false;
            }
        }
        void readFile()
        {
            _devices.Clear();
            try
            {
                using (FileStream fs = new FileStream(txtboxFileDialog.Text, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                    {
                        while (!sr.EndOfStream)
                        {
                            _devices.Add(sr.ReadLine());
                        }
                    }
                }
      
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при чтении:\n" + ex.Message.ToString());
            }
        }
    }
}
