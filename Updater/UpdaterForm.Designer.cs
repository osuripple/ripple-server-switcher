namespace Updater
{
    partial class UpdaterForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterForm));
            Bloom bloom1 = new Bloom();
            Bloom bloom2 = new Bloom();
            Bloom bloom3 = new Bloom();
            Bloom bloom4 = new Bloom();
            Bloom bloom5 = new Bloom();
            Bloom bloom6 = new Bloom();
            Bloom bloom7 = new Bloom();
            Bloom bloom8 = new Bloom();
            this.statusLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.genuineTheme1 = new GenuineTheme();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.genuineTheme1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(42, 42);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(148, 17);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Checking updates...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 71);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(263, 23);
            this.progressBar.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // genuineTheme1
            // 
            this.genuineTheme1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.genuineTheme1.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            bloom1.Name = "Back";
            bloom1.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom2.Name = "Gradient1";
            bloom2.Value = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            bloom3.Name = "Gradient2";
            bloom3.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom4.Name = "Line1";
            bloom4.Value = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            bloom5.Name = "Line2";
            bloom5.Value = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            bloom6.Name = "Text";
            bloom6.Value = System.Drawing.Color.White;
            bloom7.Name = "Border1";
            bloom7.Value = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            bloom8.Name = "Border2";
            bloom8.Value = System.Drawing.Color.Black;
            this.genuineTheme1.Colors = new Bloom[] {
        bloom1,
        bloom2,
        bloom3,
        bloom4,
        bloom5,
        bloom6,
        bloom7,
        bloom8};
            this.genuineTheme1.Controls.Add(this.statusLabel);
            this.genuineTheme1.Controls.Add(this.pictureBox1);
            this.genuineTheme1.Controls.Add(this.progressBar);
            this.genuineTheme1.Customization = "KSkp/xkZGf8pKSn/GRkZ/zo6Ov//////Ojo6/wAAAP8=";
            this.genuineTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genuineTheme1.Font = new System.Drawing.Font("Verdana", 8F);
            this.genuineTheme1.Image = null;
            this.genuineTheme1.Location = new System.Drawing.Point(0, 0);
            this.genuineTheme1.Movable = true;
            this.genuineTheme1.Name = "genuineTheme1";
            this.genuineTheme1.NoRounding = false;
            this.genuineTheme1.Sizable = true;
            this.genuineTheme1.Size = new System.Drawing.Size(285, 105);
            this.genuineTheme1.SmartBounds = true;
            this.genuineTheme1.TabIndex = 3;
            this.genuineTheme1.Text = "Updater";
            this.genuineTheme1.TransparencyKey = System.Drawing.Color.Fuchsia;
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(285, 105);
            this.Controls.Add(this.genuineTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdaterForm";
            this.Text = "Updater";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Shown += new System.EventHandler(this.UpdaterForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.genuineTheme1.ResumeLayout(false);
            this.genuineTheme1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private GenuineTheme genuineTheme1;
    }
}

