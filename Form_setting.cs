using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPserver_form {
    public partial class Form_setting : Form {
        public Form_setting(int max_log_count) {
            InitializeComponent();
            numeric_maxlogcount.Value = max_log_count;
        }

        private void Form_setting_Load(object sender, EventArgs e) {
        }

        private void button2_Click(object sender, EventArgs e) {
            Form1.SetMaxloglength((int)numeric_maxlogcount.Value);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
