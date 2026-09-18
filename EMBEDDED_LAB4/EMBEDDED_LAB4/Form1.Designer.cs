namespace EMBEDDED_LAB4
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOn_Click = new System.Windows.Forms.Button();
            this.btnOff_Click = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.PORT = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOn_Click
            // 
            this.btnOn_Click.BackColor = System.Drawing.Color.Lime;
            this.btnOn_Click.Location = new System.Drawing.Point(110, 151);
            this.btnOn_Click.Name = "btnOn_Click";
            this.btnOn_Click.Size = new System.Drawing.Size(170, 62);
            this.btnOn_Click.TabIndex = 0;
            this.btnOn_Click.Text = "ON";
            this.btnOn_Click.UseVisualStyleBackColor = false;
            this.btnOn_Click.Click += new System.EventHandler(this.btnOn_Click_Click);
            // 
            // btnOff_Click
            // 
            this.btnOff_Click.BackColor = System.Drawing.Color.Red;
            this.btnOff_Click.Location = new System.Drawing.Point(449, 151);
            this.btnOff_Click.Name = "btnOff_Click";
            this.btnOff_Click.Size = new System.Drawing.Size(167, 62);
            this.btnOff_Click.TabIndex = 1;
            this.btnOff_Click.Text = "OFF";
            this.btnOff_Click.UseVisualStyleBackColor = false;
            this.btnOff_Click.Click += new System.EventHandler(this.btnOff_Click_Click);
            // 
            // PORT
            // 
            this.PORT.Location = new System.Drawing.Point(30, 43);
            this.PORT.Name = "PORT";
            this.PORT.Size = new System.Drawing.Size(90, 22);
            this.PORT.TabIndex = 3;
            this.PORT.Text = "PORT:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(142, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 250);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.PORT);
            this.Controls.Add(this.btnOff_Click);
            this.Controls.Add(this.btnOn_Click);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOn_Click;
        private System.Windows.Forms.Button btnOff_Click;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox PORT;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

