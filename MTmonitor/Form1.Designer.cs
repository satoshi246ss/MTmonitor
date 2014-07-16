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
            this.label_PictureViewer = new System.Windows.Forms.Label();
            this.label_AFP = new System.Windows.Forms.Label();
            this.label_MT3Fine = new System.Windows.Forms.Label();
            this.label_MT3IDS = new System.Windows.Forms.Label();
            this.label_KV1000SpCam = new System.Windows.Forms.Label();
            this.label_FSI2 = new System.Windows.Forms.Label();
            this.timer_MT3IDS = new System.Windows.Forms.Timer(this.components);
            this.timer_MT3Fine = new System.Windows.Forms.Timer(this.components);
            this.timer_PictureViewer = new System.Windows.Forms.Timer(this.components);
            this.timer_KV1000SpCam = new System.Windows.Forms.Timer(this.components);
            this.timer_AFP = new System.Windows.Forms.Timer(this.components);
            this.timer_FSI2 = new System.Windows.Forms.Timer(this.components);
            this.timer_KV1000MT2 = new System.Windows.Forms.Timer(this.components);
            this.label_MT3Wide = new System.Windows.Forms.Label();
            this.timer_MT3Wide = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(441, 137);
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
            this.textBox1.Location = new System.Drawing.Point(2, 167);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1116, 19);
            this.textBox1.TabIndex = 1;
            // 
            // label_KV1000MT2
            // 
            this.label_KV1000MT2.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_KV1000MT2.Location = new System.Drawing.Point(139, -3);
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
            this.label_msg.Location = new System.Drawing.Point(0, 149);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(41, 14);
            this.label_msg.TabIndex = 3;
            this.label_msg.Text = "label2";
            // 
            // label_PictureViewer
            // 
            this.label_PictureViewer.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_PictureViewer.Location = new System.Drawing.Point(839, -3);
            this.label_PictureViewer.Name = "label_PictureViewer";
            this.label_PictureViewer.Size = new System.Drawing.Size(140, 140);
            this.label_PictureViewer.TabIndex = 4;
            this.label_PictureViewer.Text = "PictureViewer";
            this.label_PictureViewer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_AFP
            // 
            this.label_AFP.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_AFP.Location = new System.Drawing.Point(-1, -3);
            this.label_AFP.Name = "label_AFP";
            this.label_AFP.Size = new System.Drawing.Size(140, 140);
            this.label_AFP.TabIndex = 5;
            this.label_AFP.Text = "AFP";
            this.label_AFP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_MT3Fine
            // 
            this.label_MT3Fine.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_MT3Fine.Location = new System.Drawing.Point(559, -3);
            this.label_MT3Fine.Name = "label_MT3Fine";
            this.label_MT3Fine.Size = new System.Drawing.Size(140, 140);
            this.label_MT3Fine.TabIndex = 6;
            this.label_MT3Fine.Text = "MT3Fine";
            this.label_MT3Fine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_MT3IDS
            // 
            this.label_MT3IDS.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold);
            this.label_MT3IDS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_MT3IDS.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_MT3IDS.Location = new System.Drawing.Point(699, -3);
            this.label_MT3IDS.Name = "label_MT3IDS";
            this.label_MT3IDS.Size = new System.Drawing.Size(140, 140);
            this.label_MT3IDS.TabIndex = 7;
            this.label_MT3IDS.Text = "MT3IDS";
            this.label_MT3IDS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_KV1000SpCam
            // 
            this.label_KV1000SpCam.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_KV1000SpCam.Location = new System.Drawing.Point(279, -3);
            this.label_KV1000SpCam.Name = "label_KV1000SpCam";
            this.label_KV1000SpCam.Size = new System.Drawing.Size(140, 140);
            this.label_KV1000SpCam.TabIndex = 8;
            this.label_KV1000SpCam.Text = "KV1000SpCam";
            this.label_KV1000SpCam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_FSI2
            // 
            this.label_FSI2.Image = global::MTmonitor.Properties.Resources.Green_button;
            this.label_FSI2.Location = new System.Drawing.Point(419, -3);
            this.label_FSI2.Name = "label_FSI2";
            this.label_FSI2.Size = new System.Drawing.Size(140, 140);
            this.label_FSI2.TabIndex = 9;
            this.label_FSI2.Text = "MT3SF";
            this.label_FSI2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.label_MT3Wide.Location = new System.Drawing.Point(978, -1);
            this.label_MT3Wide.Name = "label_MT3Wide";
            this.label_MT3Wide.Size = new System.Drawing.Size(140, 140);
            this.label_MT3Wide.TabIndex = 10;
            this.label_MT3Wide.Text = "MT3Wide";
            this.label_MT3Wide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_MT3Wide
            // 
            this.timer_MT3Wide.Enabled = true;
            this.timer_MT3Wide.Interval = 6000;
            this.timer_MT3Wide.Tick += new System.EventHandler(this.timer_MT3Wide_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 187);
            this.Controls.Add(this.label_MT3Wide);
            this.Controls.Add(this.label_FSI2);
            this.Controls.Add(this.label_KV1000SpCam);
            this.Controls.Add(this.label_MT3IDS);
            this.Controls.Add(this.label_MT3Fine);
            this.Controls.Add(this.label_AFP);
            this.Controls.Add(this.label_PictureViewer);
            this.Controls.Add(this.label_msg);
            this.Controls.Add(this.label_KV1000MT2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "動作状況モニター";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label_KV1000MT2;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.Label label_PictureViewer;
        private System.Windows.Forms.Label label_AFP;
        private System.Windows.Forms.Label label_MT3Fine;
        private System.Windows.Forms.Label label_MT3IDS;
        private System.Windows.Forms.Label label_KV1000SpCam;
        private System.Windows.Forms.Label label_FSI2;
        private System.Windows.Forms.Timer timer_MT3IDS;
        private System.Windows.Forms.Timer timer_MT3Fine;
        private System.Windows.Forms.Timer timer_PictureViewer;
        private System.Windows.Forms.Timer timer_KV1000SpCam;
        private System.Windows.Forms.Timer timer_AFP;
        private System.Windows.Forms.Timer timer_FSI2;
        private System.Windows.Forms.Timer timer_KV1000MT2;
        private System.Windows.Forms.Label label_MT3Wide;
        private System.Windows.Forms.Timer timer_MT3Wide;
    }
}

