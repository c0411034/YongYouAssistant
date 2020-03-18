namespace YongYouAssistant
{
    partial class AlertToDoTask
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
            this.labelUrgency = new System.Windows.Forms.Label();
            this.labelContent = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelUrgency
            // 
            this.labelUrgency.AutoSize = true;
            this.labelUrgency.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelUrgency.ForeColor = System.Drawing.Color.Red;
            this.labelUrgency.Location = new System.Drawing.Point(3, 16);
            this.labelUrgency.Name = "labelUrgency";
            this.labelUrgency.Size = new System.Drawing.Size(50, 26);
            this.labelUrgency.TabIndex = 0;
            this.labelUrgency.Text = "急件";
            // 
            // labelContent
            // 
            this.labelContent.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelContent.Location = new System.Drawing.Point(48, 16);
            this.labelContent.Name = "labelContent";
            this.labelContent.Size = new System.Drawing.Size(489, 25);
            this.labelContent.TabIndex = 0;
            this.labelContent.Text = "这是一条特别长的通知这是一条特别长的通知这是一条特别长的通知这是一条特别长的通知";
            // 
            // labelTime
            // 
            this.labelTime.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.labelTime.Location = new System.Drawing.Point(430, 52);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(86, 24);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "2020/02/23";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlertToDoTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 85);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelContent);
            this.Controls.Add(this.labelUrgency);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlertToDoTask";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "您有一条新的待办";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AlertToDoTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUrgency;
        private System.Windows.Forms.Label labelContent;
        private System.Windows.Forms.Label labelTime;
    }
}