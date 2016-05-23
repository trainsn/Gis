namespace MapControlApplication2
{
    partial class DataBoard
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
            this.tbDataName = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.selected = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.miAnalysis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tbDataName
            // 
            this.tbDataName.Location = new System.Drawing.Point(144, 12);
            this.tbDataName.Name = "tbDataName";
            this.tbDataName.Size = new System.Drawing.Size(100, 21);
            this.tbDataName.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(32, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(330, 284);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // selected
            // 
            this.selected.Location = new System.Drawing.Point(95, 353);
            this.selected.Name = "selected";
            this.selected.Size = new System.Drawing.Size(75, 23);
            this.selected.TabIndex = 2;
            this.selected.Text = "selected";
            this.selected.UseVisualStyleBackColor = true;
            this.selected.Click += new System.EventHandler(this.selected_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(32, 50);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(330, 284);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.Visible = false;
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseDoubleClick);
            // 
            // miAnalysis
            // 
            this.miAnalysis.Location = new System.Drawing.Point(236, 352);
            this.miAnalysis.Name = "miAnalysis";
            this.miAnalysis.Size = new System.Drawing.Size(75, 23);
            this.miAnalysis.TabIndex = 4;
            this.miAnalysis.Text = "analysis";
            this.miAnalysis.UseVisualStyleBackColor = true;
            this.miAnalysis.Click += new System.EventHandler(this.miAnalysis_Click);
            // 
            // DataBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 399);
            this.Controls.Add(this.miAnalysis);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.selected);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tbDataName);
            this.Name = "DataBoard";
            this.Text = "数据展示台";
            this.Load += new System.EventHandler(this.DataBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDataName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button selected;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button miAnalysis;
    }
}