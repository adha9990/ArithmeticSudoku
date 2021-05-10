namespace ArithmeticSudoku
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Platter = new System.Windows.Forms.Panel();
            this.Solution_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Platter
            // 
            this.Platter.Location = new System.Drawing.Point(12, 12);
            this.Platter.Name = "Platter";
            this.Platter.Size = new System.Drawing.Size(448, 550);
            this.Platter.TabIndex = 1;
            // 
            // Solution_button
            // 
            this.Solution_button.Font = new System.Drawing.Font("新細明體", 18F);
            this.Solution_button.Location = new System.Drawing.Point(360, 568);
            this.Solution_button.Name = "Solution_button";
            this.Solution_button.Size = new System.Drawing.Size(100, 33);
            this.Solution_button.TabIndex = 2;
            this.Solution_button.Text = "solution";
            this.Solution_button.UseVisualStyleBackColor = true;
            this.Solution_button.Click += new System.EventHandler(this.Solution_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 613);
            this.Controls.Add(this.Solution_button);
            this.Controls.Add(this.Platter);
            this.Name = "Form1";
            this.Text = "四則運算數獨遊戲";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Platter;
        private System.Windows.Forms.Button Solution_button;
    }
}

