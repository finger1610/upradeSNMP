using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        async private void  btnSend_Click(object sender, RoutedEventArgs e)
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
                await Task.Run(()=>  obj.start(ip));

                prgBar.Value++;

                System.Threading.Thread.Sleep(50);
                lblProgress.Content =prgBar.Value + "/" + _devices.Count;
             //   UpdateLayout();

                
            }
         
        }

        private void txtBoxCommunity_MouseEnter(object sender, MouseEventArgs e)
        {
            checkComlete();
        }

        private void btnFileDialog_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();

            switch (fileDialog.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    txtboxFileDialog.Text = fileDialog.FileName;
                    txtboxFileDialog.ToolTip = fileDialog.FileName;


                    readFile();

                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    txtboxFileDialog.Text = string.Empty;
                    txtboxFileDialog.ToolTip = null;

                    lblFindIP.Visibility = Visibility.Hidden;
                    btnCheckIPList.IsEnabled = false;
                    break;
            }
        }
     

        private void btnCheckIPList_Click(object sender, RoutedEventArgs e)
        {
            ShowIP obj = new ShowIP("List IP addresses", _devices);
            obj.Show();
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

            //Считывание из файла
            _devices.Clear();
            string tmp = string.Empty;

            try
            {

                //резулярное выражение для поиска ip в строке
                System.Text.RegularExpressions.Regex ip = new System.Text.RegularExpressions.Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                using (FileStream fs = new FileStream(txtboxFileDialog.Text, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8))
                    {

                        while (!sr.EndOfStream)
                        {
                            tmp = sr.ReadLine();
                            if (ip.IsMatch(tmp))
                                _devices.Add(ip.Match(tmp).ToString());
                        }
                    }
                }
                checkComlete();
            }

            catch (ArgumentException ex)
            {
                MessageBox.Show("Argument" + ex.Message.ToString());
            }
            //
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка при чтении:\n" + ex.Message.ToString());
            }


            lblFindIP.Visibility = Visibility.Visible;
            lblFindIP.Content = $"Найдено " + _devices.Count + " адресов";
            btnCheckIPList.IsEnabled = true;
        }
    }
}
