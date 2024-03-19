namespace FTPserver_form {
    partial class Form_setting {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            numeric_maxlogcount = new NumericUpDown();
            label1 = new Label();
            button_cancel = new Button();
            button_apply = new Button();
            ((System.ComponentModel.ISupportInitialize)numeric_maxlogcount).BeginInit();
            SuspendLayout();
            // 
            // numeric_maxlogcount
            // 
            numeric_maxlogcount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numeric_maxlogcount.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numeric_maxlogcount.Location = new Point(12, 25);
            numeric_maxlogcount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numeric_maxlogcount.Name = "numeric_maxlogcount";
            numeric_maxlogcount.Size = new Size(156, 23);
            numeric_maxlogcount.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 7);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 1;
            label1.Text = "max log length:";
            // 
            // button_cancel
            // 
            button_cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button_cancel.Location = new Point(12, 62);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(75, 23);
            button_cancel.TabIndex = 2;
            button_cancel.Text = "cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button1_Click;
            // 
            // button_apply
            // 
            button_apply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_apply.Location = new Point(93, 62);
            button_apply.Name = "button_apply";
            button_apply.Size = new Size(75, 23);
            button_apply.TabIndex = 3;
            button_apply.Text = "apply";
            button_apply.UseVisualStyleBackColor = true;
            button_apply.Click += button2_Click;
            // 
            // Form_setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(180, 97);
            ControlBox = false;
            Controls.Add(button_apply);
            Controls.Add(button_cancel);
            Controls.Add(label1);
            Controls.Add(numeric_maxlogcount);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form_setting";
            Text = "Form_setting";
            Load += Form_setting_Load;
            ((System.ComponentModel.ISupportInitialize)numeric_maxlogcount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown numeric_maxlogcount;
        private Label label1;
        private Button button_cancel;
        private Button button_apply;
    }
}