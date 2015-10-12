namespace MTmonitor
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label_KV1000MT2 = new System.Windows.Forms.Label();
            this.label_msg = new System.Windows.Forms.Label();
            this.label_SF = new System.Windows.Forms.Label();
            this.label_AFP = new System.Windows.Forms.Label();
            this.label_MT3Fine = new System.Windows.Forms.Label();
            this.label_MT3IDS = new System.Windows.Forms.Label();
            this.label_KV1000SpCam = new System.Windows.Forms.Label();
            this.label_NUV = new System.Windows.Forms.Label();
            this.timer_MT3IDS = new System.Windows.Forms.Timer(this.components);
            this.timer_MT3Fine = new System.Windows.Forms.Timer(this.components);
            this.timer_PictureViewer = new System.Windows.Forms.Timer(this.components);
            this.timer_KV1000SpCam = new System.Windows.Forms.Timer(this.components);
            this.timer_AFP = new System.Windows.Forms.Timer(this.components);
            this.timer_FSI2 = new System.Windows.Forms.Timer(this.components);
            this.timer_KV1000MT2 = new System.Windows.Forms.Timer(this.components);
            this.label_MT3Wide = new System.Windows.Forms.Label();
            this.timer_MT3Wide = new System.Windows.Forms.Timer(this.components);
            this.timer_NIR = new System.Windows.Forms.Timer(this.components);
            this.label_AnalogCamera = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.checkBoxUseDate = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.timerPcCheck = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_NIR = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer_KV1000SpCam2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(441, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "FileMove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(2, 185);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1620, 19);
            this.textBox1.TabIndex = 1;
            // 
            // label_KV1000MT2
            // 
            this.label_KV1000MT2.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_KV1000MT2.Location = new System.Drawing.Point(144, 15);
            this.label_KV1000MT2.Name = "label_KV1000MT2";
            this.label_KV1000MT2.Size = new System.Drawing.Size(140, 140);
            this.label_KV1000MT2.TabIndex = 2;
            this.label_KV1000MT2.Text = "KV1000MT2";
            this.label_KV1000MT2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_msg
            // 
            this.label_msg.AutoSize = true;
            this.label_msg.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label_msg.Location = new System.Drawing.Point(-1, 165);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(41, 14);
            this.label_msg.TabIndex = 3;
            this.label_msg.Text = "label2";
            // 
            // label_SF
            // 
            this.label_SF.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_SF.Location = new System.Drawing.Point(1185, 17);
            this.label_SF.Name = "label_SF";
            this.label_SF.Size = new System.Drawing.Size(140, 140);
            this.label_SF.TabIndex = 4;
            this.label_SF.Text = "MT3SF";
            this.label_SF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_AFP
            // 
            this.label_AFP.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_AFP.Location = new System.Drawing.Point(0, 15);
            this.label_AFP.Name = "label_AFP";
            this.label_AFP.Size = new System.Drawing.Size(140, 140);
            this.label_AFP.TabIndex = 5;
            this.label_AFP.Text = "AFP";
            this.label_AFP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_MT3Fine
            // 
            this.label_MT3Fine.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_MT3Fine.Location = new System.Drawing.Point(152, 9);
            this.label_MT3Fine.Name = "label_MT3Fine";
            this.label_MT3Fine.Size = new System.Drawing.Size(140, 140);
            this.label_MT3Fine.TabIndex = 6;
            this.label_MT3Fine.Text = "MT3Fine";
            this.label_MT3Fine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_MT3IDS
            // 
            this.label_MT3IDS.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_MT3IDS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_MT3IDS.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_MT3IDS.Location = new System.Drawing.Point(893, 15);
            this.label_MT3IDS.Name = "label_MT3IDS";
            this.label_MT3IDS.Size = new System.Drawing.Size(140, 140);
            this.label_MT3IDS.TabIndex = 7;
            this.label_MT3IDS.Text = "MT3LrSpCam";
            this.label_MT3IDS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_MT3IDS.Click += new System.EventHandler(this.label_MT3IDS_Click);
            // 
            // label_KV1000SpCam
            // 
            this.label_KV1000SpCam.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_KV1000SpCam.Location = new System.Drawing.Point(288, 15);
            this.label_KV1000SpCam.Name = "label_KV1000SpCam";
            this.label_KV1000SpCam.Size = new System.Drawing.Size(140, 140);
            this.label_KV1000SpCam.TabIndex = 8;
            this.label_KV1000SpCam.Text = "KV1000SpCam";
            this.label_KV1000SpCam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_NUV
            // 
            this.label_NUV.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_NUV.Location = new System.Drawing.Point(6, 9);
            this.label_NUV.Name = "label_NUV";
            this.label_NUV.Size = new System.Drawing.Size(140, 140);
            this.label_NUV.TabIndex = 9;
            this.label_NUV.Text = "MT3NUV";
            this.label_NUV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_MT3IDS
            // 
            this.timer_MT3IDS.Enabled = true;
            this.timer_MT3IDS.Interval = 6000;
            this.timer_MT3IDS.Tick += new System.EventHandler(this.timer_MT3IDS_Tick);
            // 
            // timer_MT3Fine
            // 
            this.timer_MT3Fine.Enabled = true;
            this.timer_MT3Fine.Interval = 6000;
            this.timer_MT3Fine.Tick += new System.EventHandler(this.timer_MT3Fine_Tick);
            // 
            // timer_PictureViewer
            // 
            this.timer_PictureViewer.Enabled = true;
            this.timer_PictureViewer.Interval = 6000;
            this.timer_PictureViewer.Tick += new System.EventHandler(this.timer_PictureViewer_Tick);
            // 
            // timer_KV1000SpCam
            // 
            this.timer_KV1000SpCam.Enabled = true;
            this.timer_KV1000SpCam.Interval = 6000;
            this.timer_KV1000SpCam.Tick += new System.EventHandler(this.timer_KV1000SpCam_Tick);
            // 
            // timer_AFP
            // 
            this.timer_AFP.Enabled = true;
            this.timer_AFP.Interval = 6000;
            this.timer_AFP.Tick += new System.EventHandler(this.timer_AFP_Tick);
            // 
            // timer_FSI2
            // 
            this.timer_FSI2.Enabled = true;
            this.timer_FSI2.Interval = 6000;
            this.timer_FSI2.Tick += new System.EventHandler(this.timer_FSI2_Tick);
            // 
            // timer_KV1000MT2
            // 
            this.timer_KV1000MT2.Enabled = true;
            this.timer_KV1000MT2.Interval = 6000;
            this.timer_KV1000MT2.Tick += new System.EventHandler(this.timer_KV1000MT2_Tick);
            // 
            // label_MT3Wide
            // 
            this.label_MT3Wide.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_MT3Wide.Location = new System.Drawing.Point(1039, 15);
            this.label_MT3Wide.Name = "label_MT3Wide";
            this.label_MT3Wide.Size = new System.Drawing.Size(140, 140);
            this.label_MT3Wide.TabIndex = 10;
            this.label_MT3Wide.Text = "MT3Wide";
            this.label_MT3Wide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_MT3Wide.Click += new System.EventHandler(this.label_MT3Wide_Click);
            // 
            // timer_MT3Wide
            // 
            this.timer_MT3Wide.Enabled = true;
            this.timer_MT3Wide.Interval = 6000;
            this.timer_MT3Wide.Tick += new System.EventHandler(this.timer_MT3Wide_Tick);
            // 
            // timer_NIR
            // 
            this.timer_NIR.Enabled = true;
            this.timer_NIR.Interval = 6000;
            this.timer_NIR.Tick += new System.EventHandler(this.timer_NIR_Tick);
            // 
            // label_AnalogCamera
            // 
            this.label_AnalogCamera.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_AnalogCamera.Location = new System.Drawing.Point(1480, 15);
            this.label_AnalogCamera.Name = "label_AnalogCamera";
            this.label_AnalogCamera.Size = new System.Drawing.Size(140, 140);
            this.label_AnalogCamera.TabIndex = 11;
            this.label_AnalogCamera.Text = "Analog Camera";
            this.label_AnalogCamera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(566, 158);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(133, 19);
            this.dateTimePicker1.TabIndex = 12;
            // 
            // checkBoxUseDate
            // 
            this.checkBoxUseDate.AutoSize = true;
            this.checkBoxUseDate.Location = new System.Drawing.Point(545, 160);
            this.checkBoxUseDate.Name = "checkBoxUseDate";
            this.checkBoxUseDate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseDate.TabIndex = 13;
            this.checkBoxUseDate.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(702, 158);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(36, 19);
            this.numericUpDown1.TabIndex = 14;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(784, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 24);
            this.button2.TabIndex = 15;
            this.button2.Text = "Ping";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(865, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 19);
            this.panel1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "SC440";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label_NUV);
            this.groupBox1.Controls.Add(this.label_MT3Fine);
            this.groupBox1.Location = new System.Drawing.Point(434, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 155);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "I5-3450";
            // 
            // label8
            // 
            this.label8.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label8.Location = new System.Drawing.Point(298, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 140);
            this.label8.TabIndex = 10;
            this.label8.Text = "KV1000SpCam2";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerPcCheck
            // 
            this.timerPcCheck.Enabled = true;
            this.timerPcCheck.Interval = 5000;
            this.timerPcCheck.Tick += new System.EventHandler(this.timerPcCheck_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label2.Location = new System.Drawing.Point(1330, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 19;
            this.label2.Text = "HP6200SFF";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1170, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "TX100S3-B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(894, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "TX100S3";
            // 
            // label_NIR
            // 
            this.label_NIR.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_NIR.Location = new System.Drawing.Point(1331, 15);
            this.label_NIR.Name = "label_NIR";
            this.label_NIR.Size = new System.Drawing.Size(140, 140);
            this.label_NIR.TabIndex = 22;
            this.label_NIR.Text = "MT3NIR";
            this.label_NIR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_NIR.Click += new System.EventHandler(this.label_NIR_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label6.Location = new System.Drawing.Point(1479, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 23;
            this.label6.Text = "MJ34LL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.label7.Location = new System.Drawing.Point(1540, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 14);
            this.label7.TabIndex = 24;
            this.label7.Text = "KV1000(MT3)";
            // 
            // timer_KV1000SpCam2
            // 
            this.timer_KV1000SpCam2.Enabled = true;
            this.timer_KV1000SpCam2.Interval = 6000;
            this.timer_KV1000SpCam2.Tick += new System.EventHandler(this.timer_KV1000SpCam2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1623, 205);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_NIR);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBoxUseDate);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label_AnalogCamera);
            this.Controls.Add(this.label_MT3Wide);
            this.Controls.Add(this.label_KV1000SpCam);
            this.Controls.Add(this.label_MT3IDS);
            this.Controls.Add(this.label_AFP);
            this.Controls.Add(this.label_SF);
            this.Controls.Add(this.label_msg);
            this.Controls.Add(this.label_KV1000MT2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "動作状況モニター(192.168.1.211:24415)";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label_KV1000MT2;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.Label label_SF;
        private System.Windows.Forms.Label label_AFP;
        private System.Windows.Forms.Label label_MT3Fine;
        private System.Windows.Forms.Label label_MT3IDS;
        private System.Windows.Forms.Label label_KV1000SpCam;
        private System.Windows.Forms.Label label_NUV;
        private System.Windows.Forms.Timer timer_MT3IDS;
        private System.Windows.Forms.Timer timer_MT3Fine;
        private System.Windows.Forms.Timer timer_PictureViewer;
        private System.Windows.Forms.Timer timer_KV1000SpCam;
        private System.Windows.Forms.Timer timer_AFP;
        private System.Windows.Forms.Timer timer_FSI2;
        private System.Windows.Forms.Timer timer_KV1000MT2;
        private System.Windows.Forms.Label label_MT3Wide;
        private System.Windows.Forms.Timer timer_MT3Wide;
        private System.Windows.Forms.Timer timer_NIR;
        private System.Windows.Forms.Label label_AnalogCamera;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.CheckBox checkBoxUseDate;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timerPcCheck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_NIR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer_KV1000SpCam2;
    }
}

