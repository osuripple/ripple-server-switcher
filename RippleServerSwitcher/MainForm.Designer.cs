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
            Bloom bloom37 = new Bloom();
            Bloom bloom38 = new Bloom();
            Bloom bloom39 = new Bloom();
            Bloom bloom40 = new Bloom();
            Bloom bloom41 = new Bloom();
            Bloom bloom42 = new Bloom();
            Bloom bloom43 = new Bloom();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.genuineTheme1 = new GenuineTheme();
            this.localButton = new GenuineButton();
            this.installCertificateButton = new GenuineButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MirrorIPTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.updateIPButton = new GenuineButton();
            this.switchButton = new GenuineButton();
            this.statusLabel = new System.Windows.Forms.Label();
            this.genuineButton1 = new GenuineButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.genuineTheme1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.genuineTheme1.Controls.Add(this.localButton);
            this.genuineTheme1.Controls.Add(this.installCertificateButton);
            this.genuineTheme1.Controls.Add(this.groupBox1);
            this.genuineTheme1.Controls.Add(this.updateIPButton);
            this.genuineTheme1.Controls.Add(this.switchButton);
            this.genuineTheme1.Controls.Add(this.statusLabel);
            this.genuineTheme1.Controls.Add(this.genuineButton1);
            this.genuineTheme1.Controls.Add(this.groupBox2);
            this.genuineTheme1.Customization = "KSkp/xkZGf8pKSn/GRkZ/zo6Ov//////Ojo6/wAAAP8=";
            this.genuineTheme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genuineTheme1.Font = new System.Drawing.Font("Verdana", 8F);
            this.genuineTheme1.Image = null;
            this.genuineTheme1.Location = new System.Drawing.Point(0, 0);
            this.genuineTheme1.Movable = true;
            this.genuineTheme1.Name = "genuineTheme1";
            this.genuineTheme1.NoRounding = false;
            this.genuineTheme1.Sizable = false;
            this.genuineTheme1.Size = new System.Drawing.Size(255, 202);
            this.genuineTheme1.SmartBounds = true;
            this.genuineTheme1.TabIndex = 0;
            this.genuineTheme1.Text = "Ripple Server Switcher";
            this.genuineTheme1.TransparencyKey = System.Drawing.Color.Fuchsia;
            // 
            // localButton
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
            this.localButton.Colors = new Bloom[] {
        bloom9,
        bloom10,
        bloom11,
        bloom12,
        bloom13,
        bloom14,
        bloom15};
            this.localButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.localButton.Font = new System.Drawing.Font("Verdana", 8F);
            this.localButton.Image = null;
            this.localButton.Location = new System.Drawing.Point(128, 97);
            this.localButton.Name = "localButton";
            this.localButton.NoRounding = false;
            this.localButton.Size = new System.Drawing.Size(116, 25);
            this.localButton.TabIndex = 9;
            this.localButton.Text = "Local/Remote";
            this.localButton.Transparent = false;
            this.localButton.Click += new System.EventHandler(this.localButton_Click);
            // 
            // installCertificateButton
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
            this.installCertificateButton.Colors = new Bloom[] {
        bloom16,
        bloom17,
        bloom18,
        bloom19,
        bloom20,
        bloom21,
        bloom22};
            this.installCertificateButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.installCertificateButton.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.installCertificateButton.Image = null;
            this.installCertificateButton.Location = new System.Drawing.Point(128, 67);
            this.installCertificateButton.Name = "installCertificateButton";
            this.installCertificateButton.NoRounding = false;
            this.installCertificateButton.Size = new System.Drawing.Size(116, 25);
            this.installCertificateButton.TabIndex = 8;
            this.installCertificateButton.Text = "Install certificate";
            this.installCertificateButton.Transparent = false;
            this.installCertificateButton.Click += new System.EventHandler(this.installCertificateButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MirrorIPTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.IPTextBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(15, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 65);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // MirrorIPTextBox
            // 
            this.MirrorIPTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.MirrorIPTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MirrorIPTextBox.ForeColor = System.Drawing.Color.White;
            this.MirrorIPTextBox.Location = new System.Drawing.Point(116, 35);
            this.MirrorIPTextBox.Name = "MirrorIPTextBox";
            this.MirrorIPTextBox.Size = new System.Drawing.Size(107, 20);
            this.MirrorIPTextBox.TabIndex = 7;
            this.MirrorIPTextBox.TextChanged += new System.EventHandler(this.MirrorIPTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(113, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mirror IP:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ripple IP:";
            // 
            // IPTextBox
            // 
            this.IPTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.IPTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IPTextBox.ForeColor = System.Drawing.Color.White;
            this.IPTextBox.Location = new System.Drawing.Point(5, 35);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(107, 20);
            this.IPTextBox.TabIndex = 5;
            this.IPTextBox.TextChanged += new System.EventHandler(this.IPTextBox_TextChanged);
            // 
            // updateIPButton
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
            this.updateIPButton.Colors = new Bloom[] {
        bloom23,
        bloom24,
        bloom25,
        bloom26,
        bloom27,
        bloom28,
        bloom29};
            this.updateIPButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.updateIPButton.Font = new System.Drawing.Font("Verdana", 8F);
            this.updateIPButton.Image = null;
            this.updateIPButton.Location = new System.Drawing.Point(15, 97);
            this.updateIPButton.Name = "updateIPButton";
            this.updateIPButton.NoRounding = false;
            this.updateIPButton.Size = new System.Drawing.Size(110, 25);
            this.updateIPButton.TabIndex = 3;
            this.updateIPButton.Text = "Update IP";
            this.updateIPButton.Transparent = false;
            this.updateIPButton.Click += new System.EventHandler(this.updateIPButton_Click);
            // 
            // switchButton
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
            this.switchButton.Colors = new Bloom[] {
        bloom30,
        bloom31,
        bloom32,
        bloom33,
        bloom34,
        bloom35,
        bloom36};
            this.switchButton.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.switchButton.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.switchButton.Image = null;
            this.switchButton.Location = new System.Drawing.Point(15, 67);
            this.switchButton.Name = "switchButton";
            this.switchButton.NoRounding = false;
            this.switchButton.Size = new System.Drawing.Size(110, 25);
            this.switchButton.TabIndex = 2;
            this.switchButton.Text = "On/Off";
            this.switchButton.Transparent = false;
            this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(0, 35);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(255, 35);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Loading...";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // genuineButton1
            // 
            bloom37.Name = "DownGradient1";
            bloom37.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom38.Name = "DownGradient2";
            bloom38.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom39.Name = "NoneGradient1";
            bloom39.Value = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            bloom40.Name = "NoneGradient2";
            bloom40.Value = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            bloom41.Name = "Text";
            bloom41.Value = System.Drawing.Color.White;
            bloom42.Name = "Border1";
            bloom42.Value = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            bloom43.Name = "Border2";
            bloom43.Value = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.genuineButton1.Colors = new Bloom[] {
        bloom37,
        bloom38,
        bloom39,
        bloom40,
        bloom41,
        bloom42,
        bloom43};
            this.genuineButton1.Customization = "KSkp/zMzM/8zMzP/KSkp//////////8MGRkZ/w==";
            this.genuineButton1.Font = new System.Drawing.Font("Verdana", 8F);
            this.genuineButton1.Image = null;
            this.genuineButton1.Location = new System.Drawing.Point(230, 3);
            this.genuineButton1.Name = "genuineButton1";
            this.genuineButton1.NoRounding = false;
            this.genuineButton1.Size = new System.Drawing.Size(22, 23);
            this.genuineButton1.TabIndex = 0;
            this.genuineButton1.Text = "x";
            this.genuineButton1.Transparent = false;
            this.genuineButton1.Click += new System.EventHandler(this.genuineButton1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(15, 202);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(229, 109);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Warnings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8F);
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(15, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 78);
            this.label4.TabIndex = 2;
            this.label4.Text = "You are playing on a local server!\r\nThat\'s only for Ripple developers!\r\nYou won\'t" +
    " be able to connect\r\nif you leave the switcher in the\r\ncurrent state. Please pre" +
    "ss the\r\nLocal/Remote button.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 202);
            this.Controls.Add(this.genuineTheme1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "DAT SWITCHER";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.genuineTheme1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GenuineTheme genuineTheme1;
        private GenuineButton genuineButton1;
        private GenuineButton switchButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IPTextBox;
        private GenuineButton updateIPButton;
        private GenuineButton installCertificateButton;
        private TextBox MirrorIPTextBox;
        private Label label2;
        private GenuineButton localButton;
        private Label label4;
        private GroupBox groupBox2;
    }
}

