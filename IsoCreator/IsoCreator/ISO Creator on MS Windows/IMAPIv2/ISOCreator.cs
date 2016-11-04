using IMAPI2;
using IMAPI2FS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageMasterISOCreator.IMAPIv2
{    
    public class ISOCreator
    {
        public void Folder2Iso(object data)
        {
            Folder2Iso();
        }

        //[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false, EntryPoint = "SHCreateStreamOnFileEx")]
        private void Folder2Iso()
        {
            MsftFileSystemImage iso = new MsftFileSystemImage();
            IFileSystemImageResult resultImage = null;
            try
            {
                iso.ChooseImageDefaultsForMediaType(IMAPI2FS.IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DISK);
                iso.FileSystemsToCreate = FsiFileSystems.FsiFileSystemUDF;//FsiFileSystems.FsiFileSystemISO9660 | FsiFileSystems.FsiFileSystemJoliet;
                iso.Root.AddTree(@"G:\Localhost\cn_windows_8_1_enterprise_x86_dvd", false);

                resultImage = iso.CreateResultImage();
                //dynamic imageStream = resultImage.ImageStream;
                IMAPI2FS.IStream newStream = null; 

                MemoryStream oMemStream = new MemoryStream();

                if (resultImage.ImageStream != null)
                {
                    //MsftDiscFormat2Data DataWriter;
                    //DataWriter.ClientName = "IMAPIv2 TEST";
                    //DataWriter.Write(imageStream);
                    resultImage.ImageStream.Commit(0);
                    ////iso.BootImageOptions.PlatformId = PlatformId.PlatformX86
                    IMAPI2FS.tagSTATSTG tagSTAT;
                    System.Runtime.InteropServices.ComTypes.STATSTG stat = default(System.Runtime.InteropServices.ComTypes.STATSTG);
                    resultImage.ImageStream.Stat(out tagSTAT, 0x1);
                    System.Runtime.InteropServices.ComTypes.IStream sss = resultImage.ImageStream as System.Runtime.InteropServices.ComTypes.IStream;
                    
                    Stream stream = new FileStream(@"G:\Localhost\cn_windows_8_1_enterprise_x86_dvd.iso", FileMode.CreateNew);

                    int tbs = resultImage.TotalBlocks;

                    List<byte[]> ds = new List<byte[]>();

                    for (int ifb = 0; ifb < tbs; ifb++)
                    {
                        int bs = resultImage.BlockSize;
                        byte data = 0;
                        uint pcbRead = 0;

                        for (int ibp = 0; ibp < bs; ibp++)
                        {
                            resultImage.ImageStream.RemoteRead(out data, (uint)1, out pcbRead);
                        }

                        //ds.Add(data);

                        string sp = "";
                    }

                    //sss.Write(,,)

                    string ss = sss.ToString();
                    

                    //int res = SHCreateStreamOnFileEx(@"G:\Localhost\cn_windows_8_1_enterprise_x86_dvd\your.iso", 0x1001, newStream);

                    //if (res == 0 && newStream != null)
                    //{
                    //    IntPtr inBytes = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(long)));
                    //    IntPtr outBytes = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(long)));
                    //    try
                    //    {
                    //        imageStream.CopyTo(newStream, stat.cbSize, inBytes, outBytes);
                    //    }
                    //    finally
                    //    {
                    //        Marshal.FinalReleaseComObject(imageStream);
                    //        newStream.Commit(0);
                    //        Marshal.FinalReleaseComObject(newStream);
                    //        Marshal.FreeHGlobal(inBytes);
                    //        Marshal.FreeHGlobal(outBytes);
                    //        Marshal.FinalReleaseComObject(resultImage);
                    //        Marshal.FinalReleaseComObject(iso);
                    //    }
                    //}
                    //else
                    //{
                    //    Marshal.FinalReleaseComObject(imageStream);
                    //    Marshal.FinalReleaseComObject(resultImage);
                    //    Marshal.FinalReleaseComObject(iso);
                    //    //TODO: Throw exception or do whatever to signal failure here
                    //}
                    Marshal.FinalReleaseComObject(resultImage);
                    Marshal.FinalReleaseComObject(iso);
                }
                else
                {
                    Marshal.FinalReleaseComObject(resultImage);
                    Marshal.FinalReleaseComObject(iso);
                    //TODO: Throw exception or do whatever to signal failure here
                }
            }
            catch
            {
                if (resultImage != null)
                {
                    Marshal.FinalReleaseComObject(resultImage);
                }
                Marshal.FinalReleaseComObject(iso);
            }
        }

    }
}
