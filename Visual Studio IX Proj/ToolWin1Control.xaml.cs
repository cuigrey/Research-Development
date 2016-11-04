//------------------------------------------------------------------------------
// <copyright file="ToolWin1Control.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Visual_Studio_IX_Proj
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using EnvDTE;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.OLE.Interop;
    using Microsoft.VisualStudio.TextManager.Interop;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;/// <summary>
                          /// Interaction logic for ToolWin1Control.
                          /// </summary>
    public partial class ToolWin1Control : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWin1Control"/> class.
        /// </summary>
        public ToolWin1Control()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string slnName = string.Empty;
            IVsSolution openedsln = default(IVsSolution);
            DTE dte = (DTE)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
            ServiceProvider sp = new ServiceProvider((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)dte);
            if (sp != null)
            {
                openedsln = sp.GetService(typeof(SVsSolution)) as IVsSolution;
                
                string slnDir = string.Empty;
                string slnFile = string.Empty;
                string userOptsFile = string.Empty;
                openedsln.GetSolutionInfo(out slnDir, out slnFile, out userOptsFile);

                if (slnFile != null)
                {
                    slnName = slnFile;

                    // Pass in cProjects==0 and rgbstrProjectNames==null to obtain the number of BSTRS required in the pcProjectsFetched parameter
                    uint fetchedProjFiles = 0;
                    openedsln.GetProjectFilesInSolution((uint)__VSGETPROJFILESFLAGS.GPFF_SKIPUNLOADEDPROJECTS, 0, null, out fetchedProjFiles);

                    string[] projectNames = new string[fetchedProjFiles];
                    openedsln.GetProjectFilesInSolution((uint)__VSGETPROJFILESFLAGS.GPFF_SKIPUNLOADEDPROJECTS, fetchedProjFiles, projectNames, out fetchedProjFiles);

                    IVsHierarchy projHierarchy;
                    System.Guid projId;
                    System.Guid guidProjectType;
                    string projDir = string.Empty;
                    foreach (string projName in projectNames)
                    {
                        openedsln.GetProjectOfUniqueName(projName, out projHierarchy);
                        //IVsProject proj = (IVsProject)projHierarchy;
                        IVsProject3 proj3 = (IVsProject3)projHierarchy;
                        //sp.GetService(typeof(SVsExternalFilesManager) as IVsExternalFilesManager;
                        //IVsProjectSpecialFiles projSF = (IVsProjectSpecialFiles)projHierarchy;

                        projDir = Path.GetDirectoryName(projName);
                        XElement xeProjFile = XElement.Load(projName);
                        string projXMLNS = xeProjFile.GetDefaultNamespace().NamespaceName;
                        XElement[] allCompileNode = xeProjFile.Descendants(XName.Get("Compile", projXMLNS))
                                                              .Where(cn => cn.Elements(XName.Get("SubType", projXMLNS)).Any(val => val.Value.Equals("Code")))
                                                              .ToArray();
                        foreach (XElement csCode in allCompileNode)
                        {
                            string codeFile = csCode.Attribute("Include").Value;

                            if (!codeFile.StartsWith(@"Properties\"))
                            {
                                uint docItemId;
                                int foundDoc = 0;
                                VSDOCUMENTPRIORITY[] vsFoundDocPriority = new VSDOCUMENTPRIORITY[1];
                                if (VSConstants.S_OK == proj3.IsDocumentInProject(codeFile, out foundDoc, vsFoundDocPriority, out docItemId))
                                {
                                    //IVsWindowFrame vsWinFrame;
                                    //System.Guid codeLogicalView = VSConstants.LOGVIEWID.Code_guid;
                                    //proj.OpenItem(docItemId, ref codeLogicalView, System.IntPtr.Zero, out vsWinFrame);
                                    try
                                    {
                                        NavigateTo(Path.Combine(projDir, codeFile), 1, 1);
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }

                    #region Code Backup (Commented)

                    //System.Guid rguid = System.Guid.Empty;
                    //IEnumHierarchies ppenum;
                    //openedsln.GetProjectEnum((uint)__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION, ref rguid, out ppenum);

                    //IVsHierarchy[] vsHierach = new IVsHierarchy[1];
                    //uint fetchedVsHierCount;
                    //while (VSConstants.S_OK == ppenum.Next(1, vsHierach, out fetchedVsHierCount))
                    //{
                    //    IVsHierarchy projVsHierarchy = vsHierach.First();
                    //}
                    #endregion
                }
                
            }


            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}' and opened solution {1}", this.ToString(), slnName),
                "ToolWin1");
        }

        /// <summary>
        /// Open code file with VS UI Shell.
        /// </summary>
        /// <param name="docPath"></param>
        /// <param name="_line"></param>
        /// <param name="_column"></param>
        /// <returns></returns>
        public int NavigateTo(string docPath, int _line, int _column)
        {
            int hr = VSConstants.S_OK;
            var openDoc = ServiceProvider.GlobalProvider.GetService(typeof(SVsUIShellOpenDocument)) as IVsUIShellOpenDocument;
            if (openDoc == null)
            {
                return VSConstants.E_UNEXPECTED;
            }

            Microsoft.VisualStudio.OLE.Interop.IServiceProvider sp = null;
            IVsUIHierarchy hierarchy = null;
            uint itemID = 0;
            IVsWindowFrame frame = null;
            Guid viewGuid = VSConstants.LOGVIEWID_TextView;

            // Open doc to window frame.
            hr = openDoc.OpenDocumentViaProject(docPath, ref viewGuid, out sp, out hierarchy, out itemID, out frame);

            hr = frame.Show();

            IntPtr viewPtr = IntPtr.Zero;
            Guid textLinesGuid = typeof(IVsTextLines).GUID;

            // Get doc view pointer.
            hr = frame.QueryViewInterface(ref textLinesGuid, out viewPtr);

            IVsTextLines textLines = Marshal.GetUniqueObjectForIUnknown(viewPtr) as IVsTextLines;

            var textMgr = ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager)) as IVsTextManager;
            if (textMgr == null)
            {
                return VSConstants.E_UNEXPECTED;
            }

            IVsTextView textView = null;
            hr = textMgr.GetActiveView(0, textLines, out textView);

            if (textView != null)
            {
                if (_line >= 0)
                {
                    textView.SetCaretPos(_line, Math.Max(_column, 0));
                }
            }

            return VSConstants.S_OK;
        }
    }
}