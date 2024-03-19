using System.Globalization;
using System.Net;
using System.Text;

namespace FTPserver_form {
    public partial class Form1 : Form {
        ToastManager toastmanager = null;
        FTPserver ftpserver = null;

        static Form1 instance = null;
        static int max_logLength = 1000;
        static bool started = false;

        public static void SetMaxloglength(int len) {
            max_logLength = len;
        }

        public Form1() {
            InitializeComponent();
            ftpserver = new FTPserver();
            toastmanager = new ToastManager(footer_statuslabel, this);
            instance = this;
        }

        private void Form1_Load(object sender, EventArgs e) {
            toastmanager.ShowToast("welcome!");
            LoadSetting();
        }


        private void button_run_Click(object sender, EventArgs e) {
            if (started) {
                toastmanager.ShowToast("server already started!");
                return;
            }
            if (!Path.Exists(textbox_targetPath.Text)) {
                MessageBox.Show("not exists path");
                return;
            }

            do {
                if (!IPAddress.TryParse(textbox_ipaddress.Text, out IPAddress temp_ad)) {
                    MessageBox.Show("unexpected ipaddress");
                    return;
                }
            } while (false);

            Task.Run(() => ftpserver.start(textbox_targetPath.Text, textbox_ipaddress.Text));
            started = true;
            button_run.Enabled = false;

            toastmanager.ShowToast("ftpserver started");

            SaveSetting();
        }

        public static void WriteLog(string log) {
            StringBuilder logbuilder = new StringBuilder();
            logbuilder.Append(instance.text_logbox.Text);

            logbuilder.Append($"{log}\r\n");

            if (logbuilder.Length > max_logLength) {
                logbuilder.Remove(0, logbuilder.Length - max_logLength);
            }

            instance.Invoke(() => {
                instance.text_logbox.Text = logbuilder.ToString();
            });
        }

        private void button_selectpath_Click(object sender, EventArgs e) {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) {
                textbox_targetPath.Text = dialog.SelectedPath;
            }
        }


        private void SaveSetting() { //[0]: targetpath [1]: ipaddress
            string save_path = Path.Combine(Environment.CurrentDirectory, "savefile");
            if (!Directory.Exists(save_path)) {
                Directory.CreateDirectory(save_path);
            }
            string savefile_path = Path.Combine(save_path, "savefile.txt");

            string save_data = $"{textbox_targetPath.Text}||{textbox_ipaddress.Text}||{max_logLength}";
            byte[] save_byte = Encoding.UTF8.GetBytes(save_data);
            using (FileStream file = File.Open(savefile_path, FileMode.Create)) {
                file.Write(save_byte, 0, save_byte.Length);
            }

            toastmanager.ShowToast("setting has saved!");
        }

        private void LoadSetting() {
            string save_path = Path.Combine(Environment.CurrentDirectory, "savefile");
            if (!Directory.Exists(save_path)) {
                return;
            }
            string savefile_path = Path.Combine(save_path, "savefile.txt");

            string saved_text = string.Empty;
            using (FileStream file = File.Open(savefile_path, FileMode.Open))
            using (StreamReader reader = new StreamReader(file)) {
                saved_text = reader.ReadToEnd();
            }

            string[] fruits = saved_text.Split("||");
            if (fruits.Length != 3) {
                toastmanager.ShowToast("failed to load savefile");
                return;
            }

            textbox_targetPath.Text = fruits[0];
            textbox_ipaddress.Text = fruits[1];
            try {
                max_logLength = int.Parse(fruits[2]);
            }
            catch (FormatException e) {
                toastmanager.ShowToast("failed to load max log length setting");
            }


            toastmanager.ShowToast("setting loaded");
        }

        private void button_setting_Click(object sender, EventArgs e) {

            Form_setting setting = new Form_setting(max_logLength);
            DialogResult result= setting.ShowDialog();
            if(result== DialogResult.OK) {
                SaveSetting();
                toastmanager.ShowToast("setting has saved");
            }
            //ShowDialog(new Form_setting(max_logLength));
        }
    }
}
