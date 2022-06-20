namespace BelazCP
{
    partial class MainCP
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
            this.HR = new System.Windows.Forms.Button();
            this.Stock = new System.Windows.Forms.Button();
            this.Cash = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HR
            // 
            this.HR.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HR.Location = new System.Drawing.Point(12, 12);
            this.HR.Name = "HR";
            this.HR.Size = new System.Drawing.Size(238, 100);
            this.HR.TabIndex = 0;
            this.HR.Text = "Кадры";
            this.HR.UseVisualStyleBackColor = true;
            this.HR.Click += new System.EventHandler(this.HR_Click);
            // 
            // Stock
            // 
            this.Stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Stock.Location = new System.Drawing.Point(256, 12);
            this.Stock.Name = "Stock";
            this.Stock.Size = new System.Drawing.Size(238, 100);
            this.Stock.TabIndex = 1;
            this.Stock.Text = "Склад";
            this.Stock.UseVisualStyleBackColor = true;
            this.Stock.Click += new System.EventHandler(this.Stock_Click);
            // 
            // Cash
            // 
            this.Cash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Cash.Location = new System.Drawing.Point(501, 12);
            this.Cash.Name = "Cash";
            this.Cash.Size = new System.Drawing.Size(238, 100);
            this.Cash.TabIndex = 3;
            this.Cash.Text = "Касса";
            this.Cash.UseVisualStyleBackColor = true;
            this.Cash.Click += new System.EventHandler(this.Cash_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Exit.Location = new System.Drawing.Point(256, 118);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(238, 100);
            this.Exit.TabIndex = 4;
            this.Exit.Text = "Выход";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Settings
            // 
            this.Settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Settings.Location = new System.Drawing.Point(12, 115);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(238, 100);
            this.Settings.TabIndex = 5;
            this.Settings.Text = "Настройки";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // MainCP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 227);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Cash);
            this.Controls.Add(this.Stock);
            this.Controls.Add(this.HR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainCP";
            this.Text = "Белаз - панель управления [Главное меню]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainCP_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HR;
        private System.Windows.Forms.Button Stock;
        private System.Windows.Forms.Button Cash;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button Settings;
    }
}