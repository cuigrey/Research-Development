using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageMasterISOCreator
{
    public partial class ISOCreatorMain : Form
    {
        #region Fields

        private Thread m_thread = null;
        private Oscdimg.ISOCreator m_creator = new Oscdimg.ISOCreator();

        #endregion

        public ISOCreatorMain()
        {
            InitializeComponent();

            textBoxVolumeName.Text = "BUNNY-WABBIT";
        }

        private void buttonBrowseIso_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CD Images (*.iso)|*.iso";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxIsoPath.Text = dialog.FileName;
            }
        }

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxFolder.Text = dialog.SelectedPath;
            }
        }
        
        #region Start Creating ISO

        private void buttonStartAbort_Click(object sender, EventArgs e)
        {
            return;
            if (m_thread == null || !m_thread.IsAlive)
            {
                if (textBoxVolumeName.Text.Trim() != "")
                {
                    m_thread = new Thread(new ParameterizedThreadStart((m_creator).Folder2Iso));
                    //m_thread.Start(new ISOCreator.IsoCreatorFolderArgs(textBoxFolder.Text, textBoxIsoPath.Text, textBoxVolumeName.Text));
                    m_thread.Start(new string[] { textBoxFolder.Text, textBoxIsoPath.Text, textBoxVolumeName.Text });

                    buttonStartAbort.Text = "Abort";
                }
                else
                {
                    MessageBox.Show("Please insert a name for the volume", "No volume name", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to abort the process?", "Abort", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    m_thread.Abort();
                }
            }
        }

        private void btnOSCDImg_Click(object sender, EventArgs e)
        {
            if (m_thread == null || !m_thread.IsAlive)
            {
                if (textBoxVolumeName.Text.Trim() != "")
                {
                    this.Hide();
                    m_creator.Folder2Iso(new string[] { textBoxFolder.Text, textBoxIsoPath.Text, textBoxVolumeName.Text });
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Please insert a name for the volume", "No volume name", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        #endregion

        #region Drag&Drop Folder

        /// <summary>
        /// ISO file Source: When mouse up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFolder_DragDrop(object sender, DragEventArgs e)
        {
            string sFileSystemValue = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            if (Directory.Exists(sFileSystemValue))
            {
                textBoxFolder.Text = sFileSystemValue;
            }
        }

        /// <summary>
        /// ISO file Source: Mouse down and drap folder to TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            { 
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Output ISO file: When mouse up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxIsoPath_DragDrop(object sender, DragEventArgs e)
        {
            string sFileSystemValue = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            if (Directory.Exists(sFileSystemValue) && Path.GetDirectoryName(sFileSystemValue) != null)
            {
                textBoxIsoPath.Text = sFileSystemValue + ".iso";
            }
        }

        /// <summary>
        /// Output ISO file: Mouse down and drap folder to TextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxIsoPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion

    }
}
