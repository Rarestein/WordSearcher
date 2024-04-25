using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Linq;

namespace WordSearcher
{
    public partial class MainForm : Form
    {
        [Serializable]
        public class FileInfo
        {
            public string Filename { get; set; }
            public string Extension { get; set; }
        }

        public TimeSpan timeToExecute;
        private ConcurrentBag<FileInfo> files = new ConcurrentBag<FileInfo>();
        public string selectedFolderPath;

        public MainForm()
        {
            InitializeComponent();
        }

        public async Task LoadFiles(string folderPath)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                TimeSpan time;

                files = new ConcurrentBag<FileInfo>();
                listFiles.Items.Clear();

                string[] fileEntries = Directory.GetFiles(folderPath);

                await Task.Run(() =>
                {
                    Parallel.ForEach(fileEntries, filePath =>
                    {
                        var fileInfo = new FileInfo
                        {
                            Filename = Path.GetFileNameWithoutExtension(filePath),
                            Extension = Path.GetExtension(filePath)
                        };

                        files.Add(fileInfo);
                    });
                });

                var fileNames = files.Select(fileInfo => $"{fileInfo.Filename}{fileInfo.Extension}").ToList();

                listFiles.Items.AddRange(fileNames.ToArray());

                stopwatch.Stop();
                time = stopwatch.Elapsed;

                labelTime.Text = $"Time to load: {time.Minutes}:{time.Seconds}.{time.Milliseconds}";
            }
            catch
            {
                MessageBox.Show("Try again", "No folder selected");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (!string.IsNullOrEmpty(dialog.FileName))
                    {
                        selectedFolderPath = dialog.FileName;
                        LoadFiles(selectedFolderPath);
                    }
                    else
                    {
                        MessageBox.Show("Please select a folder.", "No Folder Selected");
                    }
                }
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtWord.Text != "" && files.Count != 0)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                string searchText = txtWord.Text;
                ConcurrentDictionary<string, bool> foundFiles = new ConcurrentDictionary<string, bool>();

                Regex searchPattern = new Regex(searchText, RegexOptions.IgnoreCase);

                try
                {
                    await Task.Run(() =>
                    {
                        Parallel.ForEach(files, file =>
                        {
                            string filePath = Path.Combine(selectedFolderPath, file.Filename + file.Extension);
                            if (File.Exists(filePath))
                            {
                                using (StreamReader reader = new StreamReader(filePath))
                                {
                                    string line;
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        if (searchPattern.IsMatch(line))
                                        {
                                            foundFiles.TryAdd(file.Filename + file.Extension, true);
                                            break;
                                        }
                                    }
                                }
                            }
                        });
                    });
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"An error occurred while searching: {ex.Message}", "Error");
                }

                stopwatch.Stop();
                timeToExecute = stopwatch.Elapsed;

                listFiles.Items.Clear();
                foreach (var file in foundFiles.Keys)
                {
                    listFiles.Items.Add(file);
                }

                if (foundFiles.Count == 0)
                {
                    MessageBox.Show("Text not found in any file.", "Search Results");
                }
                else
                {
                    SearchForm searchForm = new SearchForm(searchText, selectedFolderPath, foundFiles.Keys.ToList(), null, timeToExecute);
                    searchForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No word given in the search box");
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
