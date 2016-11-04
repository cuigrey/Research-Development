using System;
using System.Management;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NetstatHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                this.RegexAttr = "AMZYXFM";
                PropertyInfo propertyInfo = this.GetType().GetProperty("RegexAttr");
                propertyInfo.GetCustomAttribute<RegularExpressionAttribute>().Match(this.RegexAttr);
            }
            catch (Exception ex)
            {

            }

            this.ExecuteNetstatCMD();
        }

        [RegularExpression(@"(\w(?!AM|FM|WM))*(\w(?=AM|FM|WM))?(AM|FM|WM)(\w(?!\3))+$", ErrorMessage = "Characters are not allowed.")]
        public string RegexAttr
        {
            get;
            set;
        }

        private void ExecuteNetstatCMD()
        {
            string[] sNetStat;
            Match rightNetConn;
            int iFirstNet = 0;
            int iProcId = 0;
            Process procOfNetConn = null;
            try
            {
                Process netStat = new Process();

                netStat.StartInfo.FileName = @"netstat.exe";
                netStat.StartInfo.Arguments = "/anvo";
                netStat.StartInfo.UseShellExecute = false;
                netStat.StartInfo.RedirectStandardOutput = true;

                netStat.Start();

                /*
                [0]	    ""	
                [1]	    "Active Connections"
                [2]	    ""	
                [3]	    "  Proto  Local Address          Foreign Address        State           PID"
                [4]	    "  TCP    0.0.0.0:80             0.0.0.0:0              LISTENING       4"
                [5]	    "  TCP    0.0.0.0:135            0.0.0.0:0              LISTENING       800"
                [6]	    "  TCP    0.0.0.0:443            0.0.0.0:0              LISTENING       5680"
                [7]	    "  TCP    0.0.0.0:445            0.0.0.0:0              LISTENING       4"
                [8]	    "  TCP    0.0.0.0:3389           0.0.0.0:0              LISTENING       1164"
                [9]	    "  TCP    0.0.0.0:8888           0.0.0.0:0              LISTENING       4"
                [10]	"  TCP    0.0.0.0:26143          0.0.0.0:0              LISTENING       4"
                [11]	"  TCP    0.0.0.0:32111          0.0.0.0:0              LISTENING       2400"
                [12]	"  TCP    0.0.0.0:49152          0.0.0.0:0              LISTENING       492"
                [13]	"  TCP    0.0.0.0:49153          0.0.0.0:0              LISTENING       904"
                [14]	"  TCP    0.0.0.0:49154          0.0.0.0:0              LISTENING       608"
                */

                string[] sNetstatResult = netStat.StandardOutput.ReadToEnd()
                    .Split(new string[] { "\r\n" }, StringSplitOptions.None);

                List<string[]> netConns = new List<string[]>();

                for (iFirstNet = 4; iFirstNet < sNetstatResult.Length; iFirstNet++)
                {
                    rightNetConn = Regex.Match(sNetstatResult[iFirstNet], @"(\w+)\s+(\d+\.\d+\.\d+\.\d+:\d+)\s+(\d+\.\d+\.\d+\.\d+:\d+)\s+(\w+)\s+(\d+)", RegexOptions.IgnoreCase);

                    if (rightNetConn.Success)
                    {
                        sNetStat = new string[6];
                        sNetStat[0] = rightNetConn.Groups[1].Value;
                        sNetStat[1] = rightNetConn.Groups[2].Value;
                        sNetStat[2] = rightNetConn.Groups[3].Value;
                        sNetStat[3] = rightNetConn.Groups[4].Value;
                        sNetStat[4] = rightNetConn.Groups[5].Value;

                        if (int.TryParse(sNetStat[4], out iProcId))
                        {
                            try
                            {
                                sNetStat[5] = Netstat.GetExecutablePathViaWMI(iProcId);
                            }
                            catch (Exception ex)
                            {
                                sNetStat[5] = ex.Message;
                            }
                        }

                        netConns.Add(sNetStat);
                    }
                }

                this.NetstatCMDResult.Items.Add(netConns[0]);
            }
            catch (Exception)
            {

            }
        }
    }
}
