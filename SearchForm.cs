using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WordSearcher
{
    public partial class SearchForm : Form
    {
        public string selectedFolder;
        public string searchedText;
        public string selectedFile;

        public SearchForm(string searchText, string selectedFolderPath, List<string> foundFiles, MatchCollection matches, TimeSpan time)
        {
            InitializeComponent();

            selectedFolder = selectedFolderPath;
            searchedText = searchText;

            PopulateListResults(foundFiles);
            labelTime.Text += $"{time.Minutes}:{time.Seconds}.{time.Milliseconds}";
        }

        private HashSet<string> addedFiles = new HashSet<string>();

        private void PopulateListResults(List<string> foundFiles)
        {
            listResults.Items.Clear();
            addedFiles.Clear();
            foreach (var result in foundFiles)
            {
                if (!addedFiles.Contains(result))
                {
                    listResults.Items.Add(result);
                    addedFiles.Add(result);
                }
            }
        }

        private void listResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listResults.SelectedItem != null)
            {
                ReadContent((string)listResults.SelectedItem);
                selectedFile = (string)listResults.SelectedItem;
            }
        }

        private void OpenFile()
        {
            try
            {
                Process.Start(Path.Combine(selectedFolder, selectedFile));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the file: {ex.Message}", "Error");
            }
        }

        private void ReadContent(string filename)
        {
            string filePath = Path.Combine(selectedFolder, filename);
            try
            {
                StringBuilder contentBuilder = new StringBuilder();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        contentBuilder.AppendLine(line);
                    }
                }
                string content = contentBuilder.ToString();
                rtbFileContent.Text = content;

                int startIndex = 0;
                while (true)
                {
                    int position = content.IndexOf(searchedText, startIndex, StringComparison.OrdinalIgnoreCase);
                    if (position == -1) break;

                    rtbFileContent.SelectionStart = position - searchedText.Length;
                    rtbFileContent.SelectionLength = searchedText.Length;
                    rtbFileContent.SelectionBackColor = Color.Yellow;
                    rtbFileContent.SelectionColor = Color.Black;

                    startIndex = position + searchedText.Length;
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"An error occurred while reading the file: {ex.Message}", "Error");
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
    }
}
