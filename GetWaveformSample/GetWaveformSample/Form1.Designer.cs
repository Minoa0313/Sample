namespace GetWaveformSample
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_send = new System.Windows.Forms.TextBox();
            this.textBox_receive = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.button_get_waveform = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_send
            // 
            this.textBox_send.Location = new System.Drawing.Point(12, 12);
            this.textBox_send.Name = "textBox_send";
            this.textBox_send.Size = new System.Drawing.Size(187, 23);
            this.textBox_send.TabIndex = 0;
            // 
            // textBox_receive
            // 
            this.textBox_receive.Location = new System.Drawing.Point(12, 41);
            this.textBox_receive.Multiline = true;
            this.textBox_receive.Name = "textBox_receive";
            this.textBox_receive.Size = new System.Drawing.Size(419, 180);
            this.textBox_receive.TabIndex = 0;
            // 
            // button_send
            // 
            this.button_send.Location = new System.Drawing.Point(217, 12);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 1;
            this.button_send.Text = "send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // button_get_waveform
            // 
            this.button_get_waveform.Location = new System.Drawing.Point(298, 12);
            this.button_get_waveform.Name = "button_get_waveform";
            this.button_get_waveform.Size = new System.Drawing.Size(133, 23);
            this.button_get_waveform.TabIndex = 1;
            this.button_get_waveform.Text = "get waveform";
            this.button_get_waveform.UseVisualStyleBackColor = true;
            this.button_get_waveform.Click += new System.EventHandler(this.button_get_waveform_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 233);
            this.Controls.Add(this.button_get_waveform);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.textBox_receive);
            this.Controls.Add(this.textBox_send);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox_send;
        private TextBox textBox_receive;
        private Button button_send;
        private Button button_get_waveform;
    }
}