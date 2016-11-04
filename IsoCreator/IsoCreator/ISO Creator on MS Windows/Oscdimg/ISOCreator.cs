using IMAPI2;
using IMAPI2FS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageMasterISOCreator.Oscdimg
{    
    /// <summary>
    /// Oscdimg is extracted from Windows PE 8.1.
    /// Only used to create windows bootable ISO file for CD/DVD.
    /// </summary>
    public class ISOCreator
    {
        public void Folder2Iso(object data)
        {
            try
            {
                string[] sParms = data as string[];
                Folder2Iso(sParms[0], sParms[1], sParms[2]);
            }
            finally
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sWindowsProgRoot"></param>
        /// <param name="sOutput"></param>
        /// <param name="sVolumeName"></param>
        private void Folder2Iso(string sWindowsProgRoot, string sOutput, string sVolumeName)
        {
            if (File.Exists(sOutput))
            {
                File.Delete(sOutput);
            }

            // 0, 0x00
            string sBootOnBIOS = "etfsboot.com";
            // EF, 0xEF
            string sBootOnEFI = "efisys.bin";

            sBootOnBIOS = Directory.GetFiles(sWindowsProgRoot, sBootOnBIOS, SearchOption.AllDirectories).FirstOrDefault();
            sBootOnEFI = Directory.GetFiles(sWindowsProgRoot, sBootOnEFI, SearchOption.AllDirectories).FirstOrDefault();

            string sMSOSCDIMGCreator = "oscdimg.exe";
            sMSOSCDIMGCreator = Directory.GetFiles(@".\", sMSOSCDIMGCreator, SearchOption.AllDirectories).FirstOrDefault();

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            //startInfo.RedirectStandardOutput = true;

            // Setting this property to false enables you to redirect input, output, and error streams.
            startInfo.UseShellExecute = false; 

            startInfo.FileName = sMSOSCDIMGCreator;
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = string.Format(" -n -m -p0 -b\"{0}\" \"{1}\" \"{2}\" ", sBootOnBIOS, sWindowsProgRoot, sOutput);

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    // Set our event handler to asynchronously read the sort output.
                    //exeProcess.OutputDataReceived += new DataReceivedEventHandler((object sendingProcess, DataReceivedEventArgs outLine) =>
                    //{
                    //    if (!string.IsNullOrWhiteSpace(outLine.Data))
                    //    {
                    //        string sPrecent = Regex.Match(outLine.Data, "(\\d{1,3})\\%").Groups[1].Value;
                    //    }
                    //});

                    //exeProcess.BeginOutputReadLine();
                    //exeProcess.StandardOutput.ReadAsync
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }

    }
}
