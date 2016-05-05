namespace MapControlApplication2
{
    partial class AdmitBookmarkName
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbBookmarkName = new System.Windows.Forms.TextBox();
            this.btnAdmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbBookmarkName
            // 
            this.tbBookmarkName.Location = new System.Drawing.Point(41, 12);
            this.tbBookmarkName.Name = "tbBookmarkName";
            this.tbBookmarkName.Size = new System.Drawing.Size(100, 21);
            this.tbBookmarkName.TabIndex = 0;
            // 
            // btnAdmit
            // 
            this.btnAdmit.Location = new System.Drawing.Point(51, 39);
            this.btnAdmit.Name = "btnAdmit";
            this.btnAdmit.Size = new System.Drawing.Size(75, 23);
            this.btnAdmit.TabIndex = 1;
            this.btnAdmit.Text = "确认";
            this.btnAdmit.UseVisualStyleBackColor = true;
            this.btnAdmit.Click += new System.EventHandler(this.btnAdmit_Click);
            // 
            // AdmitBookmarkName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 62);
            this.Controls.Add(this.btnAdmit);
            this.Controls.Add(this.tbBookmarkName);
            this.Name = "AdmitBookmarkName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "书签名称设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBookmarkName;
        private System.Windows.Forms.Button btnAdmit;
    }
}