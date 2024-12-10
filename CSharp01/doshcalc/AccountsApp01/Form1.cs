using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WindowsFormsApplication6
{
    public partial class PdfFrm : Form
    {
        private AccountsCore.Accounts _accounts;
        public PdfFrm(AccountsCore.Accounts accounts)            
        {
            _accounts = accounts;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            System.IO.FileStream fs = new FileStream(@"F:\Working\demo.pdf", FileMode.Create);

            // Create an instance of the document class which represents the PDF document itself.  
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Create an instance to the PDF file by creating an instance of the PDF   
            // Writer class using the document and the filestrem in the constructor.  

            using (PdfWriter writer = PdfWriter.GetInstance(document, fs))
            {
                // Open the document to enable you to write to the document  
                document.Open();
                // Add a simple and wellknown phrase to the document in a flow layout manner  
                foreach (string line in this._accounts.ActionMaster.getActionReport())
                {
                    document.Add(new Paragraph(line));
                }
                
                // Close the document  
                document.Close();
                // Close the writer instance  
                writer.Close();
            }
            // Always close open filehandles explicity  
            fs.Close();



            //this.webBrowser1.Navigate(@"file://F:\Working\IMG_20200823_123645.jpg");



            //using (MemoryStream ms = new MemoryStream())
            //{
            //    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            //    PdfWriter writer = PdfWriter.GetInstance(document, ms);
            //    document.Open();
            //    document.Add(new Paragraph("Hello World"));
            //    document.Close();
            //    writer.Close();
            //    Response.ContentType = "pdf/application";
            //    Response.AddHeader("content-disposition",
            //    "attachment;filename=First PDF document.pdf");
            //    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            //}


            this.axAcroPDF1.LoadFile(@"F:\Working\demo.pdf");
            axAcroPDF1.setView("fitH");
            axAcroPDF1.setLayoutMode("SinglePage");
            this.axAcroPDF1.setShowToolbar(false); // Visible = true;
            axAcroPDF1.setShowScrollbars(true);



        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void PdfFrm_Activated(object sender, EventArgs e)
        {
            
        }

        private void webBrowser1_SizeChanged(object sender, EventArgs e)
        {
            //this.webBrowser1.Navigate(@"file://F:\Working\demo.pdf");
            
        }

        private void axAcroPDF1_Enter(object sender, EventArgs e)
        {

        }

        private void axAcroPDF1_SizeChanged(object sender, EventArgs e)
        {
            //this.axAcroPDF1.LoadFile(@"F:\Working\demo.pdf"); 
           // axAcroPDF1.Refresh();
        }
    }
}
//using System.Net;

//string pdfPath = Server.MapPath("~/SomePDFFile.pdf");
//WebClient client = new WebClient();
//Byte[] buffer = client.DownloadData(pdfPath);
//Response.ContentType = "application/pdf";
//Response.AddHeader("content-length", buffer.Length.ToString());
//Response.BinaryWrite(buffer);

//AddedwebBrowserControl.Navigate(@"c:\IntroductiontoSqlServer.pdf");

//AxAcroPDFLib.AxAcroPDF pdf = new AxAcroPDFLib.AxAcroPDF();
//pdf.Dock = System.Windows.Forms.DockStyle.Fill;
//pdf.Enabled = true;
//pdf.Location = new System.Drawing.Point(0, 0);
//pdf.Name = "pdfReader";
//pdf.OcxState = ((System.Windows.Forms.AxHost.State)(new System.ComponentModel.ComponentResourceManager(typeof(ViewerWindow)).GetObject("pdfReader.OcxState")));
//pdf.TabIndex = 1;

//// Add pdf viewer to current form        
//this.Controls.Add(pdf);

//pdf.LoadFile(@"C:\MyPDF.pdf");
//pdf.setView("Fit");
//pdf.Visible = true;