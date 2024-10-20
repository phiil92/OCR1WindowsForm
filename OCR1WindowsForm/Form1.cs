using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;
using PdfiumViewer;
using System.Drawing;
using System.Threading.Tasks;  

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

        // Start OCR Button Click Event (now async)
        private async void btnStart_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;

            if (Directory.Exists(folderPath))
            {
                // Update the status to "Processing..." and force the UI to refresh
                lblStatus.Text = "Status: Processing...";
                lblStatus.Refresh();  // Forces UI update to happen immediately

                // Perform the OCR processing asynchronously to avoid blocking the UI
                await Task.Run(() => ProcessPDFFiles(folderPath));

                // Show a single message at the end after processing all files
                MessageBox.Show("Processing completed.");

                // Set status back to ready after processing
                lblStatus.Text = "Status: Ready";
            }
            else
            {
                MessageBox.Show("Folder does not exist.");
                lblStatus.Text = "Status: Not Ready (Invalid folder path)";
            }
        }

        // Method to process the PDF files
        private void ProcessPDFFiles(string folderPath)
        {
            // Get all PDF files in the selected folder
            var pdfFiles = Directory.GetFiles(folderPath, "*.pdf");

            foreach (var pdfFile in pdfFiles)
            {
                // Extract text using OCR
                string extractedText = ExtractTextFromPdf(pdfFile);

                // Search for the ITSD number
                string itsdNumber = FindITSDNumber(extractedText);

                if (!string.IsNullOrEmpty(itsdNumber))
                {
                    // Generate a new file name with the ITSD number
                    string newFileName = Path.Combine(folderPath, $"{itsdNumber}.pdf");
                    newFileName = GetUniqueFileName(newFileName);

                    // Rename the PDF file
                    File.Move(pdfFile, newFileName);
                }
            }
        }

        // Extract text from PDF using OCR on each page
        static string ExtractTextFromPdf(string pdfFilePath)
        {
            string extractedText = "";

            using (var pdfDoc = PdfDocument.Load(pdfFilePath))
            {
                // Loop through each page of the PDF
                for (int i = 0; i < pdfDoc.PageCount; i++)
                {
                    // Render each page to an image
                    using (var image = pdfDoc.Render(i, 300, 300, PdfRenderFlags.CorrectFromDpi))
                    {
                        // Perform OCR on the image
                        extractedText += PerformOcrOnImage(image);
                    }
                }
            }

            return extractedText;
        }

        // Perform OCR on image using Tesseract
        static string PerformOcrOnImage(System.Drawing.Image image)
        {
            string ocrResult = "";

            // Initialize Tesseract engine
            using (var ocrEngine = new TesseractEngine(@"C:\Users\ichha\source\repos\OCR1WindowsForm\Tessdata", "eng", EngineMode.Default))
            {
                // Convert image to Pix format for Tesseract
                using (var pixImage = PixConverter.ToPix((Bitmap)image))
                {
                    using (var page = ocrEngine.Process(pixImage))
                    {
                        // Extract text from the page
                        ocrResult = page.GetText();
                    }
                }
            }

            return ocrResult;
        }

        // Find ITSD number in the extracted text using regex
        static string FindITSDNumber(string text)
        {
            var match = Regex.Match(text, @"ITSD-\d{5}");
            return match.Success ? match.Value : null;
        }

        // Ensure unique file name if the file with the same name already exists
        static string GetUniqueFileName(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);
            int count = 1;

            // If file with same name exists, append a counter
            while (File.Exists(filePath))
            {
                filePath = Path.Combine(directory, $"{fileName}({count++}){extension}");
            }

            return filePath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
