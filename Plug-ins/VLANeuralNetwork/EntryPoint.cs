using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLAControlLib;
using VLAPluginLib;

namespace VLANeuralNetwork
{
    public class EntryPoint : VLAPlugin
    {
        public static DefaultPage HostDefaultPage; //Get the Host DefaultPage user Control
        public static LogTextDelegate HostLogger; //Get the Host Log Box
        public static ProgressBarDelegate HostProgressBar; //Get the Host ProgressBar

        //Loaded when it's discovered by the plug-in service and all base variables are set
        public EntryPoint()
        {
            base.IllustrationName = "Neural Network Illustration";
            base.IllustrationVersion = "1.0.0.0";
            base.IllustrationSupportedLanguages = new List<string>(new[] { "English" });
            base.PluginType = PluginType.Illustration;
            base.IllustrationAuthor = "Ola";
            base.IllustrationDescription = "Neural Network Illustration";
            base.IllustrationModuleName = new List<string>(
                new[]
                    {
                        "Data Structures",
                    });
        }

        public override void IllustrationDisplay()
        {
            HostDefaultPage = base.HDefaultPage;
            HostLogger = base.HLog;
            HostProgressBar = base.HProgressBar;
            HostDefaultPage.Switch = new Main();
        }
    }
}
