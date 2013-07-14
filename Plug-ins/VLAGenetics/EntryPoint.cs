using System;
using System.Collections.Generic;
using System.Text;
using VLAControlLib;
using VLAPluginLib;

namespace VLAGenetics
{
    public class EntryPoint : VLAPlugin
    {
        public static DefaultPage HostDefaultPage; //Get the Host DefaultPage user Control
        public static LogTextDelegate HostLogger; //Get the Host Log Box

        //Loaded when it's discovered by the plug-in service and all base variables are set
        public EntryPoint()
        {
            base.IllustrationName = "Genetic Algorithm Illustration";
            base.IllustrationVersion = "1.0.0.0";
            base.IllustrationSupportedLanguages = new List<string>(new[] {"English"});
            base.PluginType = PluginType.Illustration;
            base.IllustrationAuthor = "Ola";
            base.IllustrationDescription = "In a genetic algorithm, a population of candidate solutions (called individuals, creatures, or phenotypes) to an optimization problem is evolved toward better solutions.";
            base.IllustrationModuleName = new List<string>(
                new[]
                    {
                        "Algorithm",
                    });
            HostDefaultPage = base.HDefaultPage;
        }

        public override void IllustrationDisplay()
        {
            HostDefaultPage = base.HDefaultPage;
            HostLogger = base.HLog;
            HostDefaultPage.Switch = new Main(); //Load the Main.xaml page
        }
    }
}
