using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;
using PdfiumViewer;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OCR1WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblStatus.Text = "Status: Ready";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderBrowser.SelectedPath;

                    if (Directory.Exists(txtFolderPath.Text))
                    {
                        lblStatus.Text = "Status: Ready";
                    }
                    else
                    {
                        lblStatus.Text = "Status: Not Ready (Invalid folder path)";
                    }
                }
            }
        }

        // hawk
        private async void btnStart_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;

            if (Directory.Exists(folderPath))
            {
                // tuah
                lblStatus.Text = "Status: Processing...";
                lblStatus.Refresh();  

                await Task.Run(() => ProcessPDFFiles(folderPath));

                MessageBox.Show("Processing completed.");

                lblStatus.Text = "Status: Ready";
            }
            else
            {
                MessageBox.Show("Please select a valid folder.");
            }
        }

        // Netto
        private Dictionary<string, string> lieferscheinToITSD = new Dictionary<string, string>();

        private void ProcessPDFFiles(string folderPath)
        {
            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

            foreach (string pdfFile in pdfFiles)
            {
                string text = ExtractTextFromPDF(pdfFile);

                string itsdNumber = ExtractITSDNumber(text);
                string lieferscheinNr = ExtractLieferscheinNr(text);

                if (string.IsNullOrEmpty(itsdNumber))
                {
                    if (lieferscheinToITSD.ContainsKey(lieferscheinNr))
                    {
                        itsdNumber = lieferscheinToITSD[lieferscheinNr]; 
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lieferscheinNr) && !lieferscheinToITSD.ContainsKey(lieferscheinNr))
                    {
                        lieferscheinToITSD[lieferscheinNr] = itsdNumber;
                    }
                }

                RenamePDFFile(pdfFile, folderPath, itsdNumber);
            }
        }

        private string ExtractTextFromPDF(string pdfFilePath)
        {
            using (var pdfDocument = PdfDocument.Load(pdfFilePath))
            using (var bitmap = new Bitmap(pdfDocument.Render(0, 300, 300, true)))
            {
                using (var ocrEngine = new TesseractEngine(@"C:\Users\ichha\source\repos\OCR1WindowsForm\Tessdata", "eng", EngineMode.Default))
                {
                    var page = ocrEngine.Process(bitmap);
                    return page.GetText();
                }
            }
        }
        private string ExtractITSDNumber(string text)
        {
            Regex regex = new Regex(@"\bITSD[^\s]*\b", RegexOptions.IgnoreCase);
            Match match = regex.Match(text);

            if (match.Success)
            {
                return match.Value.Trim();
            }
            return null;
        }

        // Steal
        private string ExtractLieferscheinNr(string text)
        {
            Regex regex = new Regex(@"\bLieferschein\s*Nr\.\s*[^\s]+\b", RegexOptions.IgnoreCase);
            Match match = regex.Match(text);

            if (match.Success)
            {
                return match.Value.Trim();
            }
            return null;
        }

        private void RenamePDFFile(string originalFilePath, string folderPath, string itsdNumber)
        {
            string newFileName = $"{itsdNumber}.pdf";
            string newFilePath = Path.Combine(folderPath, newFileName);

            int counter = 1;
            while (File.Exists(newFilePath))
            {
                newFileName = $"{itsdNumber}_{counter}.pdf";
                newFilePath = Path.Combine(folderPath, newFileName);
                counter++;
            }

            File.Move(originalFilePath, newFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
