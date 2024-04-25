namespace WordSearcher
{
    partial class SearchForm
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
            this.listResults = new System.Windows.Forms.ListBox();
            this.rtbFileContent = new System.Windows.Forms.RichTextBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listResults
            // 
            this.listResults.FormattingEnabled = true;
            this.listResults.Location = new System.Drawing.Point(12, 12);
            this.listResults.Name = "listResults";
            this.listResults.Size = new System.Drawing.Size(256, 420);
            this.listResults.TabIndex = 0;
            this.listResults.SelectedIndexChanged += new System.EventHandler(this.listResults_SelectedIndexChanged);
            // 
            // rtbFileContent
            // 
            this.rtbFileContent.Location = new System.Drawing.Point(274, 12);
            this.rtbFileContent.Name = "rtbFileContent";
            this.rtbFileContent.ReadOnly = true;
            this.rtbFileContent.Size = new System.Drawing.Size(512, 420);
            this.rtbFileContent.TabIndex = 1;
            this.rtbFileContent.Text = "";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(12, 445);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(87, 13);
            this.labelTime.TabIndex = 2;
            this.labelTime.Text = "Time to search: ";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(707, 438);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open file";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 467);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.rtbFileContent);
            this.Controls.Add(this.listResults);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "SearchForm";
            this.Text = "Search results";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listResults;
        private System.Windows.Forms.RichTextBox rtbFileContent;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button btnOpen;
    }
}