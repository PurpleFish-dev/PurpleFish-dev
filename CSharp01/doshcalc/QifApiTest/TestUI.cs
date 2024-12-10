using System;
using System.Windows.Forms;
using QifApi;
using System.IO;
using ApiTest;

namespace QifApiTest
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
            //qifDomPropertyGrid.SelectedObject = QifDom.ImportFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\sample.qif");
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            if (ConfirmOverwrite())
            {
                InitializeQifDom();
            }
        }

        private void InitializeQifDom()
        {
            qifDomPropertyGrid.SelectedObject = new QifDom();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                QifDom.ExportFile((QifDom)qifDomPropertyGrid.SelectedObject, saveFileDialog.FileName);
                MessageBox.Show(this, "The export is complete.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            if (ConfirmOverwrite())
            {
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    QifDom dom = QifDom.ImportFile(openFileDialog.FileName);
					if( (dom.DateFormat == QifDom.fileDateFormat.ddmmyyyy) || (dom.DateFormat == QifDom.fileDateFormat.mmddyyyy) )
					{
						qifDomPropertyGrid.SelectedObject = dom;
					}
					else 
					{
						var qIFDateFormatDialog = new QIFDateFormatDialog(dom.DateFormat);
						if (qIFDateFormatDialog.ShowDialog(this) == DialogResult.OK)
						{
							QifDom.fileDateFormat fileDateFormat = qIFDateFormatDialog.DateFormat;
							dom.DateFormat = fileDateFormat;
							qifDomPropertyGrid.SelectedObject = dom;
						}
					}					
                }
            }
        }

        private bool ConfirmOverwrite()
        {
            bool result = false;

            if (qifDomPropertyGrid.SelectedObject != null)
            {
                result = MessageBox.Show(this, "Do you want to overwrite the existing QifDom?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
