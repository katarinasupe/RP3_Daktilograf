
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
            this.lenOfWords = new System.Windows.Forms.NumericUpDown();
            this.lenOfWordsLabel = new System.Windows.Forms.Label();
            this.lenOfEx = new System.Windows.Forms.NumericUpDown();
            this.lenOfExLabel = new System.Windows.Forms.Label();
            this.practiceLetters = new System.Windows.Forms.TextBox();
            this.practiceLettersLabel = new System.Windows.Forms.Label();
            this.generateExButton = new System.Windows.Forms.Button();
            this.createExPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lenOfWords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lenOfEx)).BeginInit();
            this.SuspendLayout();
            // 
            // createExPanel
            // 
            this.createExPanel.Controls.Add(this.generateExButton);
            this.createExPanel.Controls.Add(this.lenOfWords);
            this.createExPanel.Controls.Add(this.lenOfWordsLabel);
            this.createExPanel.Controls.Add(this.lenOfEx);
            this.createExPanel.Controls.Add(this.lenOfExLabel);
            this.createExPanel.Controls.Add(this.practiceLetters);
            this.createExPanel.Controls.Add(this.practiceLettersLabel);
            this.createExPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createExPanel.Location = new System.Drawing.Point(0, 0);
            this.createExPanel.Name = "createExPanel";
            this.createExPanel.Size = new System.Drawing.Size(346, 243);
            this.createExPanel.TabIndex = 0;
            // 
            // lenOfWords
            // 
            this.lenOfWords.Location = new System.Drawing.Point(157, 118);
            this.lenOfWords.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lenOfWords.Name = "lenOfWords";
            this.lenOfWords.Size = new System.Drawing.Size(42, 22);
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
            this.lenOfWordsLabel.Location = new System.Drawing.Point(12, 120);
            this.lenOfWordsLabel.Name = "lenOfWordsLabel";
            this.lenOfWordsLabel.Size = new System.Drawing.Size(141, 17);
            this.lenOfWordsLabel.TabIndex = 4;
            this.lenOfWordsLabel.Text = "Odaberi duljinu riječi:";
            // 
            // lenOfEx
            // 
            this.lenOfEx.Location = new System.Drawing.Point(171, 77);
            this.lenOfEx.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lenOfEx.Name = "lenOfEx";
            this.lenOfEx.Size = new System.Drawing.Size(42, 22);
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
            this.lenOfExLabel.Location = new System.Drawing.Point(12, 77);
            this.lenOfExLabel.Name = "lenOfExLabel";
            this.lenOfExLabel.Size = new System.Drawing.Size(153, 17);
            this.lenOfExLabel.TabIndex = 2;
            this.lenOfExLabel.Text = "Odaberi duljinu vježbe:";
            // 
            // practiceLetters
            // 
            this.practiceLetters.Location = new System.Drawing.Point(15, 38);
            this.practiceLetters.Multiline = true;
            this.practiceLetters.Name = "practiceLetters";
            this.practiceLetters.Size = new System.Drawing.Size(100, 25);
            this.practiceLetters.TabIndex = 1;
            // 
            // practiceLettersLabel
            // 
            this.practiceLettersLabel.AutoSize = true;
            this.practiceLettersLabel.Location = new System.Drawing.Point(12, 9);
            this.practiceLettersLabel.Name = "practiceLettersLabel";
            this.practiceLettersLabel.Size = new System.Drawing.Size(323, 17);
            this.practiceLettersLabel.TabIndex = 0;
            this.practiceLettersLabel.Text = "Unesi slova koja želiš vježbati, odvojena zarezom:";
            // 
            // generateExButton
            // 
            this.generateExButton.Location = new System.Drawing.Point(12, 161);
            this.generateExButton.Name = "generateExButton";
            this.generateExButton.Size = new System.Drawing.Size(138, 30);
            this.generateExButton.TabIndex = 6;
            this.generateExButton.Text = "Generiraj vježbu";
            this.generateExButton.UseVisualStyleBackColor = true;
            this.generateExButton.Click += new System.EventHandler(this.generateExButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 243);
            this.Controls.Add(this.createExPanel);
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
    }
}