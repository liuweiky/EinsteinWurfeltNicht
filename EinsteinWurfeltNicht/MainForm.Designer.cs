namespace EinsteinWurfeltNicht
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.chessPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.diceLabel = new System.Windows.Forms.Label();
            this.playerLabel1 = new System.Windows.Forms.Label();
            this.playerLabel2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chessPanel
            // 
            this.chessPanel.Location = new System.Drawing.Point(12, 12);
            this.chessPanel.Name = "chessPanel";
            this.chessPanel.Size = new System.Drawing.Size(650, 650);
            this.chessPanel.TabIndex = 0;
            // 
            // diceLabel
            // 
            this.diceLabel.AutoSize = true;
            this.diceLabel.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.diceLabel.Location = new System.Drawing.Point(812, 313);
            this.diceLabel.Name = "diceLabel";
            this.diceLabel.Size = new System.Drawing.Size(60, 64);
            this.diceLabel.TabIndex = 1;
            this.diceLabel.Text = "5";
            // 
            // playerLabel1
            // 
            this.playerLabel1.AutoSize = true;
            this.playerLabel1.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.playerLabel1.ForeColor = System.Drawing.Color.Blue;
            this.playerLabel1.Location = new System.Drawing.Point(668, 35);
            this.playerLabel1.Name = "playerLabel1";
            this.playerLabel1.Size = new System.Drawing.Size(378, 97);
            this.playerLabel1.TabIndex = 2;
            this.playerLabel1.Text = "Player1";
            // 
            // playerLabel2
            // 
            this.playerLabel2.AutoSize = true;
            this.playerLabel2.Font = new System.Drawing.Font("宋体", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.playerLabel2.ForeColor = System.Drawing.Color.Red;
            this.playerLabel2.Location = new System.Drawing.Point(668, 529);
            this.playerLabel2.Name = "playerLabel2";
            this.playerLabel2.Size = new System.Drawing.Size(378, 97);
            this.playerLabel2.TabIndex = 3;
            this.playerLabel2.Text = "Player2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 674);
            this.Controls.Add(this.playerLabel2);
            this.Controls.Add(this.playerLabel1);
            this.Controls.Add(this.diceLabel);
            this.Controls.Add(this.chessPanel);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel chessPanel;
        private System.Windows.Forms.Label diceLabel;
        private System.Windows.Forms.Label playerLabel1;
        private System.Windows.Forms.Label playerLabel2;
    }
}

