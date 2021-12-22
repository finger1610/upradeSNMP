using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace upradeSNMP
{
    public partial class AddOID : Form
    {
       List<string> _oid;
        public AddOID(List<string> oid)
        {
            _oid = oid;
            InitializeComponent();
            if (oid.Count == 3)
            {
                txtboxOIDName.Text = oid[0].ToString();
                txtBoxOIDTFTP.Text = oid[1].ToString();
                txtboxReboot.Text  = oid[2].ToString();
            }
            txtboxOIDName.Select();
        }



      
  

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtBoxOIDTFTP.Text != "" &&
                txtboxReboot.Text != "")
            {
                if (_oid.Count != 0)
                    _oid.Clear();

                _oid.Add(txtboxOIDName.Text);
                _oid.Add(txtBoxOIDTFTP.Text);
                _oid.Add(txtboxReboot.Text);
                MessageBox.Show("OIDы добавлены");
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

       
    }
}
