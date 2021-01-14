
namespace WindowsFormsApp1
{
    partial class Form2
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
            this.createExPanel = new System.Windows.Forms.Panel();
            this.generateExButton = new System.Windows.Forms.Button();
            this.lenOfWords = new System.Windows.Forms.NumericUpDown();
            this.lenOfWordsLabel = new System.Windows.Forms.Label();
            this.lenOfEx = new System.Windows.Forms.NumericUpDown();
            this.lenOfExLabel = new System.Windows.Forms.Label();
            this.practiceLetters = new System.Windows.Forms.TextBox();
            this.practiceLettersLabel = new System.Windows.Forms.Label();
            this.exNameLabel = new System.Windows.Forms.Label();
            this.exNameTextBox = new System.Windows.Forms.TextBox();
            this.createExPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lenOfWords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lenOfEx)).BeginInit();
            this.SuspendLayout();
            // 
            // createExPanel
            // 
            this.createExPanel.Controls.Add(this.exNameTextBox);
            this.createExPanel.Controls.Add(this.exNameLabel);
            this.createExPanel.Controls.Add(this.generateExButton);
            this.createExPanel.Controls.Add(this.lenOfWords);
            this.createExPanel.Controls.Add(this.lenOfWordsLabel);
            this.createExPanel.Controls.Add(this.lenOfEx);
            this.createExPanel.Controls.Add(this.lenOfExLabel);
            this.createExPanel.Controls.Add(this.practiceLetters);
            this.createExPanel.Controls.Add(this.practiceLettersLabel);
            this.createExPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createExPanel.Location = new System.Drawing.Point(0, 0);
            this.createExPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createExPanel.Name = "createExPanel";
            this.createExPanel.Size = new System.Drawing.Size(260, 239);
            this.createExPanel.TabIndex = 0;
            // 
            // generateExButton
            // 
            this.generateExButton.Location = new System.Drawing.Point(12, 204);
            this.generateExButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.generateExButton.Name = "generateExButton";
            this.generateExButton.Size = new System.Drawing.Size(104, 24);
            this.generateExButton.TabIndex = 6;
            this.generateExButton.Text = "Generiraj vježbu";
            this.generateExButton.UseVisualStyleBackColor = true;
            this.generateExButton.Click += new System.EventHandler(this.generateExButton_Click);
            // 
            // lenOfWords
            // 
            this.lenOfWords.Location = new System.Drawing.Point(118, 96);
            this.lenOfWords.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lenOfWords.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lenOfWords.Name = "lenOfWords";
            this.lenOfWords.Size = new System.Drawing.Size(32, 20);
            this.lenOfWords.TabIndex = 5;
            this.lenOfWords.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lenOfWordsLabel
            // 
            this.lenOfWordsLabel.AutoSize = true;
            this.lenOfWordsLabel.Location = new System.Drawing.Point(9, 98);
            this.lenOfWordsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lenOfWordsLabel.Name = "lenOfWordsLabel";
            this.lenOfWordsLabel.Size = new System.Drawing.Size(104, 13);
            this.lenOfWordsLabel.TabIndex = 4;
            this.lenOfWordsLabel.Text = "Odaberi duljinu riječi:";
            // 
            // lenOfEx
            // 
            this.lenOfEx.Location = new System.Drawing.Point(128, 63);
            this.lenOfEx.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lenOfEx.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lenOfEx.Name = "lenOfEx";
            this.lenOfEx.Size = new System.Drawing.Size(32, 20);
            this.lenOfEx.TabIndex = 3;
            this.lenOfEx.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lenOfExLabel
            // 
            this.lenOfExLabel.AutoSize = true;
            this.lenOfExLabel.Location = new System.Drawing.Point(9, 63);
            this.lenOfExLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lenOfExLabel.Name = "lenOfExLabel";
            this.lenOfExLabel.Size = new System.Drawing.Size(114, 13);
            this.lenOfExLabel.TabIndex = 2;
            this.lenOfExLabel.Text = "Odaberi duljinu vježbe:";
            // 
            // practiceLetters
            // 
            this.practiceLetters.Location = new System.Drawing.Point(11, 31);
            this.practiceLetters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.practiceLetters.Multiline = true;
            this.practiceLetters.Name = "practiceLetters";
            this.practiceLetters.Size = new System.Drawing.Size(76, 21);
            this.practiceLetters.TabIndex = 1;
            // 
            // practiceLettersLabel
            // 
            this.practiceLettersLabel.AutoSize = true;
            this.practiceLettersLabel.Location = new System.Drawing.Point(9, 7);
            this.practiceLettersLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.practiceLettersLabel.Name = "practiceLettersLabel";
            this.practiceLettersLabel.Size = new System.Drawing.Size(242, 13);
            this.practiceLettersLabel.TabIndex = 0;
            this.practiceLettersLabel.Text = "Unesi slova koja želiš vježbati, odvojena zarezom:";
            // 
            // exNameLabel
            // 
            this.exNameLabel.AutoSize = true;
            this.exNameLabel.Location = new System.Drawing.Point(9, 135);
            this.exNameLabel.Name = "exNameLabel";
            this.exNameLabel.Size = new System.Drawing.Size(99, 13);
            this.exNameLabel.TabIndex = 7;
            this.exNameLabel.Text = "Unesi naziv vježbe:";
            // 
            // exNameTextBox
            // 
            this.exNameTextBox.Location = new System.Drawing.Point(12, 163);
            this.exNameTextBox.Name = "exNameTextBox";
            this.exNameTextBox.Size = new System.Drawing.Size(164, 20);
            this.exNameTextBox.TabIndex = 8;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 239);
            this.Controls.Add(this.createExPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kreiraj svoju vježbu";
            this.createExPanel.ResumeLayout(false);
            this.createExPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lenOfWords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lenOfEx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel createExPanel;
        private System.Windows.Forms.Label lenOfExLabel;
        private System.Windows.Forms.TextBox practiceLetters;
        private System.Windows.Forms.Label practiceLettersLabel;
        private System.Windows.Forms.NumericUpDown lenOfEx;
        private System.Windows.Forms.NumericUpDown lenOfWords;
        private System.Windows.Forms.Label lenOfWordsLabel;
        private System.Windows.Forms.Button generateExButton;
        private System.Windows.Forms.TextBox exNameTextBox;
        private System.Windows.Forms.Label exNameLabel;
    }
}