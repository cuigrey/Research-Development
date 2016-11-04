using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BER.CDCat.Export;
using System.IO;

namespace IsoCreator.Forms {
	public partial class MainForm : Form {

		#region Fields 

		private Thread m_thread = null;
		private IsoCreator m_creator = null;

		#endregion

		#region Constructors

		public MainForm() {
			InitializeComponent();
			//
			textBoxFolder.Text = @"C:\";
			textBoxIsoPath.Text = @"C:\MyIso.iso";
			textBoxVolumeName.Text = "BUNNY-WABBIT";
			//
			m_creator = new IsoCreator();
			m_creator.Progress += new ProgressDelegate( creator_Progress );
			m_creator.Finish += new FinishDelegate( creator_Finished );
			m_creator.Abort += new AbortDelegate( creator_Abort );
			//
			this.Icon = Properties.Resources.CDCat;
		}

		#endregion

		#region Set Delegates 

		private delegate void SetLabelDelegate( string text );
		private delegate void SetNumericValueDelegate( int value );

		#endregion

		#region Set Methods

		private void SetLabelStatus( string text ) {
			this.labelStatus.Text = text;
			this.labelStatus.Refresh();
		}

		private void SetProgressValue( int value ) {
			this.progressBar.Value = value;
		}

		private void SetProgressMaximum( int maximum ) {
			this.progressBar.Maximum = maximum;
		}

		#endregion

		#region Event Handlers

		private void buttonStartAbort_Click( object sender, EventArgs e ) {
			if ( m_thread == null || !m_thread.IsAlive ) {
				if ( textBoxVolumeName.Text.Trim() != "" ) {
					m_thread = new Thread( new ParameterizedThreadStart( m_creator.Folder2Iso ) );
					m_thread.Start( new IsoCreator.IsoCreatorFolderArgs( textBoxFolder.Text, textBoxIsoPath.Text, textBoxVolumeName.Text ) );

					buttonStartAbort.Text = "Abort";
				} else {
					MessageBox.Show( "Please insert a name for the volume", "No volume name", MessageBoxButtons.OK, MessageBoxIcon.Hand );
				}
			} else {
				if ( MessageBox.Show( "Are you sure you want to abort the process?", "Abort", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
					m_thread.Abort();
				}
			}
		}

		void creator_Abort( object sender, AbortEventArgs e ) {
			MessageBox.Show( e.Message, "Abort", MessageBoxButtons.OK, MessageBoxIcon.Stop );
			MessageBox.Show( "The ISO creating process has been stopped.", "Abort", MessageBoxButtons.OK, MessageBoxIcon.Stop );
			buttonStartAbort.Enabled = true;
			buttonStartAbort.Text = "Start";
			progressBar.Value = 0;
			progressBar.Maximum = 0;
			labelStatus.Text = "Process not started";
		}

		void creator_Finished( object sender, FinishEventArgs e ) {
			MessageBox.Show( e.Message, "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information );
			buttonStartAbort.Enabled = true;
			buttonStartAbort.Text = "Start";
			progressBar.Value = 0;
			labelStatus.Text = "Process not started";
		}

		void creator_Progress( object sender, ProgressEventArgs e ) {
			if ( e.Action != null ) {
				if ( !this.InvokeRequired ) {
					this.SetLabelStatus( e.Action );
				} else {
					this.Invoke( new SetLabelDelegate( SetLabelStatus ), e.Action );
				}
			}

			if ( e.Maximum != -1 ) {
				if ( !this.InvokeRequired ) {
					this.SetProgressMaximum( e.Maximum );
				} else {
					this.Invoke( new SetNumericValueDelegate( SetProgressMaximum ), e.Maximum );
				}
			}

			if ( !this.InvokeRequired ) {
				progressBar.Value = ( e.Current <= progressBar.Maximum ) ? e.Current : progressBar.Maximum;
			} else {
				int value = ( e.Current <= progressBar.Maximum ) ? e.Current : progressBar.Maximum;
				this.Invoke( new SetNumericValueDelegate( SetProgressValue ), value );
			}
		}

		private void buttonBrowseFolder_Click( object sender, EventArgs e ) {
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			if ( dialog.ShowDialog( this ) == DialogResult.OK ) {
				textBoxFolder.Text = dialog.SelectedPath;
			}
		}

		private void buttonBrowseIso_Click( object sender, EventArgs e ) {
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = "CD Images (*.iso)|*.iso";
			if ( dialog.ShowDialog( this ) == DialogResult.OK ) {
				textBoxIsoPath.Text = dialog.FileName;
			}
		}

		private void MainForm_FormClosing( object sender, FormClosingEventArgs e ) {
			if ( m_creator != null && m_thread != null && m_thread.IsAlive ) {
				m_thread.Abort();
			}
		}

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