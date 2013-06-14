using System.Windows.Controls;
using VLAControlLib;

namespace VLAPluginLib
{
    /// <summary>
    ///     Main Plug in to allow users to develop
    /// </summary>
    public interface IVLAPlugin
    {
        #region Plugin App

        /// <summary>
        ///     The name of the application
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     The version of the application
        /// </summary>
        string Version { get; }

        /// <summary>
        ///     The author of the application
        /// </summary>
        string Author { get; }

        /// <summary>
        ///     The description of the application
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     The supported languages of the application
        /// </summary>
        string SupportedLanguages { get; }

        /// <summary>
        ///     Display the application
        /// </summary>
        void Display();

        /// <summary>
        /// Get the plug-in type
        /// </summary>
        PluginType PluginType { get; }

        #endregion

        #region Host

        /// <summary>
        ///     The Host Log text box
        /// </summary>
        LogTextDisplayer HostLog { get; set; }

        /// <summary>
        ///     Set and Get the Default Page to allow page switching
        /// </summary>
        DefaultPage DefaultPage { get; set; }

        /// <summary>
        /// Set and get MenuItem into the Add-on Menu item
        /// </summary>
        MenuItem MenuItem { get; set; }

        /// <summary>
        /// Set and get the Main Progress Bar
        /// </summary>
        ProgressBarDelegate ProgressBar { get; set; }
        #endregion Host
    }
}