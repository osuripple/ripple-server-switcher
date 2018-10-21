using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RippleServerSwitcher
{
    partial class MainForm
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
            Bloom bloom1 = new Bloom();
            Bloom bloom2 = new Bloom();
            Bloom bloom3 = new Bloom();
            Bloom bloom4 = new Bloom();
            Bloom bloom5 = new Bloom();
            Bloom bloom6 = new Bloom();
            Bloom bloom7 = new Bloom();
            Bloom bloom8 = new Bloom();
            Bloom bloom9 = new Bloom();
            Bloom bloom10 = new Bloom();
            Bloom bloom11 = new Bloom();
            Bloom bloom12 = new Bloom();
            Bloom bloom13 = new Bloom();
            Bloom bloom14 = new Bloom();
            Bloom bloom15 = new Bloom();
            Bloom bloom16 = new Bloom();
            Bloom bloom17 = new Bloom();
            Bloom bloom18 = new Bloom();
            Bloom bloom19 = new Bloom();
            Bloom bloom20 = new Bloom();
            Bloom bloom21 = new Bloom();
            Bloom bloom22 = new Bloom();
            Bloom bloom23 = new Bloom();
            Bloom bloom24 = new Bloom();
            Bloom bloom25 = new Bloom();
            Bloom bloom26 = new Bloom();
            Bloom bloom27 = new Bloom();
            Bloom bloom28 = new Bloom();
            Bloom bloom29 = new Bloom();
            Bloom bloom30 = new Bloom();
            Bloom bloom31 = new Bloom();
            Bloom bloom32 = new Bloom();
            Bloom bloom33 = new Bloom();
            Bloom bloom34 = new Bloom();
            Bloom bloom35 = new Bloom();
            Bloom bloom36 = new Bloom();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.genuineTheme1 = new GenuineTheme();
            this.inspectButton = new GenuineButton();
            this.aboutButton = new GenuineButton();
            this.bottomStatusPanel = new System.Windows.Forms.Panel();
            this.bottomStatusPicture = new System.Windows.Forms.PictureBox();
            this.bottomStatusLabel = new System.Windows.Forms.Label();
            this.switchButton = new GenuineButton();
            this.statusLabel = new System.Windows.Forms.Label();
            this.closeButton = new GenuineButton();
            this.genuineTheme1.SuspendLayout();
            this.bottomStatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomStatusPicture)).BeginInit();
            this.SuspendLayout();
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
            this.genuineTheme1.Controls.Add(this.inspectButton);
            this.genuineTheme1.Controls.Add(this.aboutButton);
            this.genuineTheme1.Controls.Add(this.bottomStatusPanel);
            this.genuineTheme1.Controls.Add(this.switchButton);
            this.genuineTheme1.Controls.Add(this.statusLabel);
            this.genuineTheme1.Controls.Add(this.closeButton);
            this.genuineTheme1.Customization = "KSkp/xkZGf8pKSn/GRkZ/zo6Ov//////Ojo6/wAAAP8=";
            this.genuineTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genuineTheme1.Font = new System.Drawing.Font("Verdana", 8F);
            this.genuineTheme1.Image = null;
            this.genuineTheme1.Location = new System.Drawing.Point(0, 0);
            this.genuineTheme1.Margin = new System.Windows.Forms.Padding(4);
            this.genuineTheme1.Movable = true;
            this.genuineTheme1.Name = "genuineTheme1";
            this.genuineTheme1.NoRounding = false;
            this.genuineTheme1.Sizable = false;
            this.genuineTheme1.Size = new System.Drawing.Size(340, 169);
            this.genuineTheme1.SmartBounds = true;
            this.genuineTheme1.TabIndex = 0;
            this.genuineTheme1.Text = "Ripple Server Switcher";
            this.genuineTheme1.TransparencyKey = System.Drawing.Color.Fuchsia;
            // 
            // inspectButton
            // 
            bloom9.Name = "DownGradient1";
            bloom9.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom10.Name = "DownGradient2";
            bloom10.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom11.Name = "NoneGradient1";
            bloom11.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom12.Name = "NoneGradient2";
            bloom12.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom13.Name = "Text";
            bloom13.Value = System.Drawing.Color.White;
            bloom14.Name = "Border1";
            bloom14.Value = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            bloom15.Name = "Border2";
            bloom15.Value = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.inspectButton.Colors = new Bloom[] {
        bloom9,
        bloom10,
        bloom11,
        bloom12,
        bloom13,
        bloom14,
        bloom15};
            this.inspectButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.inspectButton.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Italic);
            this.inspectButton.Image = null;
            this.inspectButton.Location = new System.Drawing.Point(175, 106);
            this.inspectButton.Margin = new System.Windows.Forms.Padding(4);
            this.inspectButton.Name = "inspectButton";
            this.inspectButton.NoRounding = false;
            this.inspectButton.Size = new System.Drawing.Size(150, 31);
            this.inspectButton.TabIndex = 13;
            this.inspectButton.Text = "Inspect";
            this.inspectButton.Transparent = false;
            this.inspectButton.Click += new System.EventHandler(this.inspectButton_click);
            // 
            // aboutButton
            // 
            bloom16.Name = "DownGradient1";
            bloom16.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom17.Name = "DownGradient2";
            bloom17.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom18.Name = "NoneGradient1";
            bloom18.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom19.Name = "NoneGradient2";
            bloom19.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom20.Name = "Text";
            bloom20.Value = System.Drawing.Color.White;
            bloom21.Name = "Border1";
            bloom21.Value = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            bloom22.Name = "Border2";
            bloom22.Value = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.aboutButton.Colors = new Bloom[] {
        bloom16,
        bloom17,
        bloom18,
        bloom19,
        bloom20,
        bloom21,
        bloom22};
            this.aboutButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.aboutButton.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Italic);
            this.aboutButton.Image = null;
            this.aboutButton.Location = new System.Drawing.Point(20, 106);
            this.aboutButton.Margin = new System.Windows.Forms.Padding(4);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.NoRounding = false;
            this.aboutButton.Size = new System.Drawing.Size(150, 31);
            this.aboutButton.TabIndex = 12;
            this.aboutButton.Text = "About";
            this.aboutButton.Transparent = false;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // bottomStatusPanel
            // 
            this.bottomStatusPanel.Controls.Add(this.bottomStatusPicture);
            this.bottomStatusPanel.Controls.Add(this.bottomStatusLabel);
            this.bottomStatusPanel.Location = new System.Drawing.Point(20, 142);
            this.bottomStatusPanel.Name = "bottomStatusPanel";
            this.bottomStatusPanel.Size = new System.Drawing.Size(305, 24);
            this.bottomStatusPanel.TabIndex = 11;
            // 
            // bottomStatusPicture
            // 
            this.bottomStatusPicture.Location = new System.Drawing.Point(4, 2);
            this.bottomStatusPicture.Name = "bottomStatusPicture";
            this.bottomStatusPicture.Size = new System.Drawing.Size(20, 21);
            this.bottomStatusPicture.TabIndex = 8;
            this.bottomStatusPicture.TabStop = false;
            // 
            // bottomStatusLabel
            // 
            this.bottomStatusLabel.AutoSize = true;
            this.bottomStatusLabel.ForeColor = System.Drawing.Color.Silver;
            this.bottomStatusLabel.Location = new System.Drawing.Point(21, 1);
            this.bottomStatusLabel.Name = "bottomStatusLabel";
            this.bottomStatusLabel.Size = new System.Drawing.Size(78, 17);
            this.bottomStatusLabel.TabIndex = 9;
            this.bottomStatusLabel.Text = "Loading...";
            this.bottomStatusLabel.DoubleClick += new System.EventHandler(this.bottomStatusLabel_DoubleClick);
            // 
            // switchButton
            // 
            bloom23.Name = "DownGradient1";
            bloom23.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom24.Name = "DownGradient2";
            bloom24.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom25.Name = "NoneGradient1";
            bloom25.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom26.Name = "NoneGradient2";
            bloom26.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom27.Name = "Text";
            bloom27.Value = System.Drawing.Color.White;
            bloom28.Name = "Border1";
            bloom28.Value = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            bloom29.Name = "Border2";
            bloom29.Value = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.switchButton.Colors = new Bloom[] {
        bloom23,
        bloom24,
        bloom25,
        bloom26,
        bloom27,
        bloom28,
        bloom29};
            this.switchButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.switchButton.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.switchButton.Image = null;
            this.switchButton.Location = new System.Drawing.Point(20, 72);
            this.switchButton.Margin = new System.Windows.Forms.Padding(4);
            this.switchButton.Name = "switchButton";
            this.switchButton.NoRounding = false;
            this.switchButton.Size = new System.Drawing.Size(305, 31);
            this.switchButton.TabIndex = 2;
            this.switchButton.Text = "On/Off";
            this.switchButton.Transparent = false;
            this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(0, 43);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(340, 25);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Loading...";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // closeButton
            // 
            bloom30.Name = "DownGradient1";
            bloom30.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom31.Name = "DownGradient2";
            bloom31.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom32.Name = "NoneGradient1";
            bloom32.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom33.Name = "NoneGradient2";
            bloom33.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom34.Name = "Text";
            bloom34.Value = System.Drawing.Color.White;
            bloom35.Name = "Border1";
            bloom35.Value = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            bloom36.Name = "Border2";
            bloom36.Value = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.closeButton.Colors = new Bloom[] {
        bloom30,
        bloom31,
        bloom32,
        bloom33,
        bloom34,
        bloom35,
        bloom36};
            this.closeButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.closeButton.Font = new System.Drawing.Font("Verdana", 8F);
            this.closeButton.Image = null;
            this.closeButton.Location = new System.Drawing.Point(310, 4);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4);
            this.closeButton.Name = "closeButton";
            this.closeButton.NoRounding = false;
            this.closeButton.Size = new System.Drawing.Size(26, 22);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "x";
            this.closeButton.Transparent = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(340, 169);
            this.Controls.Add(this.genuineTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "DAT SWITCHER";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.genuineTheme1.ResumeLayout(false);
            this.bottomStatusPanel.ResumeLayout(false);
            this.bottomStatusPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomStatusPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GenuineTheme genuineTheme1;
        private GenuineButton closeButton;
        private GenuineButton switchButton;
        private System.Windows.Forms.Label statusLabel;
        private PictureBox bottomStatusPicture;
        private Label bottomStatusLabel;
        private Panel bottomStatusPanel;
        private GenuineButton aboutButton;
        private GenuineButton inspectButton;
    }
}

