namespace FTPserver_form {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textbox_targetPath = new TextBox();
            button_selectpath = new Button();
            textbox_ipaddress = new TextBox();
            statusStrip1 = new StatusStrip();
            footer_statuslabel = new ToolStripStatusLabel();
            button_run = new Button();
            text_logbox = new TextBox();
            button_setting = new Button();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            closeToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textbox_targetPath
            // 
            textbox_targetPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_targetPath.Location = new Point(44, 12);
            textbox_targetPath.Name = "textbox_targetPath";
            textbox_targetPath.PlaceholderText = "path to share..";
            textbox_targetPath.Size = new Size(264, 23);
            textbox_targetPath.TabIndex = 1;
            // 
            // button_selectpath
            // 
            button_selectpath.Location = new Point(12, 12);
            button_selectpath.Name = "button_selectpath";
            button_selectpath.Size = new Size(26, 23);
            button_selectpath.TabIndex = 0;
            button_selectpath.Text = "🔍";
            button_selectpath.UseVisualStyleBackColor = true;
            button_selectpath.Click += button_selectpath_Click;
            // 
            // textbox_ipaddress
            // 
            textbox_ipaddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_ipaddress.Location = new Point(12, 42);
            textbox_ipaddress.Name = "textbox_ipaddress";
            textbox_ipaddress.PlaceholderText = "IP address (0.0.0.0)";
            textbox_ipaddress.Size = new Size(202, 23);
            textbox_ipaddress.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.ScrollBar;
            statusStrip1.Items.AddRange(new ToolStripItem[] { footer_statuslabel });
            statusStrip1.Location = new Point(0, 115);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(320, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // footer_statuslabel
            // 
            footer_statuslabel.Name = "footer_statuslabel";
            footer_statuslabel.Size = new Size(38, 17);
            footer_statuslabel.Text = "status";
            // 
            // button_run
            // 
            button_run.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_run.Location = new Point(252, 41);
            button_run.Name = "button_run";
            button_run.Size = new Size(56, 23);
            button_run.TabIndex = 3;
            button_run.Text = "run";
            button_run.UseVisualStyleBackColor = true;
            button_run.Click += button_run_Click;
            // 
            // text_logbox
            // 
            text_logbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            text_logbox.BackColor = SystemColors.ActiveCaptionText;
            text_logbox.ForeColor = SystemColors.InactiveBorder;
            text_logbox.Location = new Point(12, 70);
            text_logbox.Multiline = true;
            text_logbox.Name = "text_logbox";
            text_logbox.PlaceholderText = "log box";
            text_logbox.ReadOnly = true;
            text_logbox.ScrollBars = ScrollBars.Vertical;
            text_logbox.Size = new Size(296, 35);
            text_logbox.TabIndex = 4;
            // 
            // button_setting
            // 
            button_setting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_setting.Location = new Point(220, 42);
            button_setting.Name = "button_setting";
            button_setting.Size = new Size(26, 23);
            button_setting.TabIndex = 5;
            button_setting.Text = "⚙";
            button_setting.UseVisualStyleBackColor = true;
            button_setting.Click += button_setting_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "FTPserver";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick_1;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { closeToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(102, 26);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(101, 22);
            closeToolStripMenuItem.Text = "close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(320, 137);
            Controls.Add(button_setting);
            Controls.Add(text_logbox);
            Controls.Add(button_run);
            Controls.Add(statusStrip1);
            Controls.Add(textbox_ipaddress);
            Controls.Add(button_selectpath);
            Controls.Add(textbox_targetPath);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "FTPserver";
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textbox_targetPath;
        private Button button_selectpath;
        private TextBox textbox_ipaddress;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel footer_statuslabel;
        private Button button_run;
        private TextBox text_logbox;
        private Button button_setting;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem closeToolStripMenuItem;
    }
}
