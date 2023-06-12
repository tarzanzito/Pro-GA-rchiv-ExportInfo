namespace WinFormsProgArchives
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            buttonProcessArtists = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxLastArtist = new TextBox();
            textBoxLastAlbum = new TextBox();
            textBoxLastCountry = new TextBox();
            buttonRefresh = new Button();
            textBoxUntilAlbum = new TextBox();
            textBoxUntilArtist = new TextBox();
            label5 = new Label();
            label6 = new Label();
            checkBoxOnlyOne = new CheckBox();
            listBox1 = new ListBox();
            buttonProcessAlbums = new Button();
            buttonProcessCountries = new Button();
            buttonAnalyseArtists = new Button();
            button2 = new Button();
            buttonAnalyseAlbums = new Button();
            buttonAnalyseCountries = new Button();
            SuspendLayout();
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            // 
            // buttonProcessArtists
            // 
            buttonProcessArtists.Location = new Point(423, 19);
            buttonProcessArtists.Name = "buttonProcessArtists";
            buttonProcessArtists.Size = new Size(60, 23);
            buttonProcessArtists.TabIndex = 0;
            buttonProcessArtists.Text = "Process";
            buttonProcessArtists.UseVisualStyleBackColor = true;
            buttonProcessArtists.Click += buttonProcessArtists_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 22);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 1;
            label1.Text = "Last Artist:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 55);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 2;
            label2.Text = "Last Album:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 89);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 3;
            label3.Text = "Last Country:";
            // 
            // textBoxLastArtist
            // 
            textBoxLastArtist.Location = new Point(107, 19);
            textBoxLastArtist.Multiline = true;
            textBoxLastArtist.Name = "textBoxLastArtist";
            textBoxLastArtist.ReadOnly = true;
            textBoxLastArtist.Size = new Size(69, 23);
            textBoxLastArtist.TabIndex = 4;
            textBoxLastArtist.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxLastAlbum
            // 
            textBoxLastAlbum.Location = new Point(107, 52);
            textBoxLastAlbum.Multiline = true;
            textBoxLastAlbum.Name = "textBoxLastAlbum";
            textBoxLastAlbum.ReadOnly = true;
            textBoxLastAlbum.Size = new Size(69, 23);
            textBoxLastAlbum.TabIndex = 5;
            textBoxLastAlbum.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxLastCountry
            // 
            textBoxLastCountry.Location = new Point(107, 86);
            textBoxLastCountry.Multiline = true;
            textBoxLastCountry.Name = "textBoxLastCountry";
            textBoxLastCountry.ReadOnly = true;
            textBoxLastCountry.Size = new Size(69, 23);
            textBoxLastCountry.TabIndex = 6;
            textBoxLastCountry.TextAlign = HorizontalAlignment.Right;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Location = new Point(116, 118);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(60, 24);
            buttonRefresh.TabIndex = 7;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // textBoxUntilAlbum
            // 
            textBoxUntilAlbum.Location = new Point(339, 52);
            textBoxUntilAlbum.Multiline = true;
            textBoxUntilAlbum.Name = "textBoxUntilAlbum";
            textBoxUntilAlbum.Size = new Size(69, 23);
            textBoxUntilAlbum.TabIndex = 12;
            textBoxUntilAlbum.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxUntilArtist
            // 
            textBoxUntilArtist.Location = new Point(339, 19);
            textBoxUntilArtist.Multiline = true;
            textBoxUntilArtist.Name = "textBoxUntilArtist";
            textBoxUntilArtist.Size = new Size(69, 23);
            textBoxUntilArtist.TabIndex = 11;
            textBoxUntilArtist.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(271, 55);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 9;
            label5.Text = "Proc Until:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(271, 22);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 8;
            label6.Text = "Proc Until:";
            // 
            // checkBoxOnlyOne
            // 
            checkBoxOnlyOne.AutoSize = true;
            checkBoxOnlyOne.Location = new Point(271, 122);
            checkBoxOnlyOne.Name = "checkBoxOnlyOne";
            checkBoxOnlyOne.Size = new Size(119, 19);
            checkBoxOnlyOne.TabIndex = 14;
            checkBoxOnlyOne.Text = "Process Only One";
            checkBoxOnlyOne.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 148);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(471, 154);
            listBox1.TabIndex = 15;
            // 
            // buttonProcessAlbums
            // 
            buttonProcessAlbums.Location = new Point(423, 51);
            buttonProcessAlbums.Name = "buttonProcessAlbums";
            buttonProcessAlbums.Size = new Size(60, 23);
            buttonProcessAlbums.TabIndex = 16;
            buttonProcessAlbums.Text = "Process";
            buttonProcessAlbums.UseVisualStyleBackColor = true;
            buttonProcessAlbums.Click += buttonProcessAlbums_Click;
            // 
            // buttonProcessCountries
            // 
            buttonProcessCountries.Location = new Point(423, 85);
            buttonProcessCountries.Name = "buttonProcessCountries";
            buttonProcessCountries.Size = new Size(60, 23);
            buttonProcessCountries.TabIndex = 17;
            buttonProcessCountries.Text = "Process";
            buttonProcessCountries.UseVisualStyleBackColor = true;
            buttonProcessCountries.Click += buttonProcessCountries_Click;
            // 
            // buttonAnalyseArtists
            // 
            buttonAnalyseArtists.Location = new Point(192, 19);
            buttonAnalyseArtists.Name = "buttonAnalyseArtists";
            buttonAnalyseArtists.Size = new Size(60, 23);
            buttonAnalyseArtists.TabIndex = 18;
            buttonAnalyseArtists.Text = "Analyse";
            buttonAnalyseArtists.UseVisualStyleBackColor = true;
            buttonAnalyseArtists.Click += buttonAnalyseArtists_Click;
            // 
            // button2
            // 
            button2.Location = new Point(423, 119);
            button2.Name = "button2";
            button2.Size = new Size(60, 23);
            button2.TabIndex = 19;
            button2.Text = "Close";
            button2.UseVisualStyleBackColor = true;
            // 
            // buttonAnalyseAlbums
            // 
            buttonAnalyseAlbums.Location = new Point(192, 52);
            buttonAnalyseAlbums.Name = "buttonAnalyseAlbums";
            buttonAnalyseAlbums.Size = new Size(60, 23);
            buttonAnalyseAlbums.TabIndex = 20;
            buttonAnalyseAlbums.Text = "Analyse";
            buttonAnalyseAlbums.UseVisualStyleBackColor = true;
            buttonAnalyseAlbums.Click += buttonAnalyseAlbums_Click;
            // 
            // buttonAnalyseCountries
            // 
            buttonAnalyseCountries.Location = new Point(192, 86);
            buttonAnalyseCountries.Name = "buttonAnalyseCountries";
            buttonAnalyseCountries.Size = new Size(60, 23);
            buttonAnalyseCountries.TabIndex = 21;
            buttonAnalyseCountries.Text = "Analyse";
            buttonAnalyseCountries.UseVisualStyleBackColor = true;
            buttonAnalyseCountries.Click += buttonAnalyseCountries_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 313);
            Controls.Add(buttonAnalyseCountries);
            Controls.Add(buttonAnalyseAlbums);
            Controls.Add(button2);
            Controls.Add(buttonAnalyseArtists);
            Controls.Add(buttonProcessCountries);
            Controls.Add(buttonProcessAlbums);
            Controls.Add(listBox1);
            Controls.Add(checkBoxOnlyOne);
            Controls.Add(textBoxUntilAlbum);
            Controls.Add(textBoxUntilArtist);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(buttonRefresh);
            Controls.Add(textBoxLastCountry);
            Controls.Add(textBoxLastAlbum);
            Controls.Add(textBoxLastArtist);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonProcessArtists);
            Name = "Form1";
            Text = "Form1";
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            Shown += FormMain_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button buttonProcessArtists;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxLastArtist;
        private TextBox textBoxLastAlbum;
        private TextBox textBoxLastCountry;
        private Button buttonRefresh;
        private TextBox textBoxUntilAlbum;
        private TextBox textBoxUntilArtist;
        private Label label5;
        private Label label6;
        private CheckBox checkBoxOnlyOne;
        private ListBox listBox1;
        private Button buttonProcessAlbums;
        private Button buttonProcessCountries;
        private Button buttonAnalyseArtists;
        private Button button2;
        private Button buttonAnalyseAlbums;
        private Button buttonAnalyseCountries;
    }
}