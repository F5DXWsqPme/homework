namespace Solution
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBarSecond = new System.Windows.Forms.ProgressBar();
            this.groupBoxClock = new Solution.MainWindow.DoubleBufferedGroupBox();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressBarSecond
            // 
            this.progressBarSecond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarSecond.Location = new System.Drawing.Point(13, 13);
            this.progressBarSecond.Maximum = 59999;
            this.progressBarSecond.Name = "progressBarSecond";
            this.progressBarSecond.Size = new System.Drawing.Size(392, 59);
            this.progressBarSecond.TabIndex = 0;
            // 
            // groupBoxClock
            // 
            this.groupBoxClock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxClock.Location = new System.Drawing.Point(13, 79);
            this.groupBoxClock.Name = "groupBoxClock";
            this.groupBoxClock.Size = new System.Drawing.Size(392, 424);
            this.groupBoxClock.TabIndex = 1;
            this.groupBoxClock.TabStop = false;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 200;
            this.timerUpdate.Tick += new System.EventHandler(this.UpdateEvent);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 515);
            this.Controls.Add(this.groupBoxClock);
            this.Controls.Add(this.progressBarSecond);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "MainWindow";
            this.Text = "Clock";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarSecond;
        private DoubleBufferedGroupBox groupBoxClock;
        private System.Windows.Forms.Timer timerUpdate;
    }
}

