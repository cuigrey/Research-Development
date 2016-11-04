using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace DirListComparer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnCopyfromClipboard_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace targetWS = Microsoft.CodeAnalysis.MSBuild.MSBuildWorkspace.Create();
            Task<Microsoft.CodeAnalysis.Solution> task = targetWS.OpenSolutionAsync(@"M:\Workspaces\TFS_Cloud_gkgkgray\MS_Fakes_CustomUnitTest\HTRD\IsoCreator\IsoCreator\IsoCreator.sln");
            Microsoft.CodeAnalysis.Solution sln = task.Result;
        }

    }
}
