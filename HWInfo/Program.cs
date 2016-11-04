using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace HWInfo
{
    class Program
    {
        /// <summary>
        /// http://www.codeproject.com/Articles/17973/How-To-Get-Hardware-Information-CPU-ID-MainBoard-I
        /// http://stackoverflow.com/questions/2333149/how-to-fast-get-hardware-id-in-c
        /// http://jai-on-asp.blogspot.com/2010/03/finding-hardware-id-of-computer.html
        /// </summary>
        /// <returns></returns>
        public static string GetHardwareId()
        {
            //var token = HardwareIdentification.GetPackageSpecificToken(null);
            //var stream = token.Id.AsStream();
            //using (var reader = new BinaryReader(stream))
            //{
            //    var bytes = reader.ReadBytes((int)stream.Length);
            //    return BitConverter.ToString(bytes);
            //}
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_DiskDrive");

            foreach (ManagementObject share in searcher.Get())
            {

            }

            return string.Empty;
        }

        /// <summary>
        /// wmic diskdrive      >e:\diskdrive.txt
        /// wmic partition      >e:\partition.txt
        /// wmic volume         >e:\volume.txt
        /// wmic logicaldisk    >e:\logicaldisk.txt
        /// 
        /// C:\Windows\system32>bcdedit.exe /enum osloader
        /// 
        /// bcdedit /export e:\BCD-backup
        /// (note that there is no ending slash in this command, "BCD" is actually the file name it's being saved to. 
        /// If you end the destination with an \ you will receive an error stating that not enough resources are available)
        ///
        /// To import that store, use;
        /// bcdedit /import X:\BCD-Backup-Path\BCD
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //GetHardwareId();

            string sHWValue = FingerPrint.Value();
        }
    }
}
