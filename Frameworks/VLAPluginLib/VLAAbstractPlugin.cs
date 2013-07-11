using System;
using System.Windows.Controls;
using VLAControlLib;

namespace VLAPluginLib
{
    /// <summary>
    ///     The Abstract Plug in class to allow for multiple plug in in one for future updates
    ///     This should be the entry point's base class
    /// </summary>
    public abstract class VLAAbstractPlugin : IVLAPlugin
    {
        /// <summary>
        ///     Display the application
        /// </summary>
        /// <param name="dpage">Set the Default Page</param>
        /// <param name="hostLog">Set the Host Log TextBox</param>
        /// <param name="menuItem">The MenuItem to add System plug-in types to</param>
        /// <param name="progressbar">The Main progress bar</param>
        public void DisplayPlugin(DefaultPage dpage, LogTextDisplayer hostLog, MenuItem menuItem, ProgressBarDelegate progressbar)
        {
            if (PluginType == PluginType.System)
            {
                MenuItem = menuItem;
            }
            MenuItem = menuItem;
            ProgressBar = progressbar;
            DefaultPage = dpage;
            HostLog = hostLog;
            Display();
        }

        #region IVLAPlugin

        /// <summary>
        ///     Display the application
        /// </summary>
        public virtual void Display()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     The name of the application
        /// </summary>
        public virtual string Name
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     The version of the application
        /// </summary>
        public virtual string Version
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     The Author
        /// </summary>
        public virtual string Author
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Get the Plug-in type
        /// </summary>
        public virtual PluginType PluginType
        {
            get { return PluginType.LoadAble; }
        }

        /// <summary>
        ///     The Description
        /// </summary>
        public virtual string Description
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     The supported languages of the plug-in
        /// </summary>
        public virtual string SupportedLanguages
        {
            get { throw new NotImplementedException(); }
        }

        #region Host

        /// <summary>
        ///     Set the Default Page to allow page switching
        /// </summary>
        public virtual DefaultPage DefaultPage
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     The Host Log text box
        /// </summary>
        public virtual LogTextDisplayer HostLog
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Set and get MenuItem into the Add-on Menu item
        /// </summary>
        public virtual MenuItem MenuItem
        {
            get{throw new NotImplementedException();}
            set{throw new NotImplementedException();}
        }

        /// <summary>
        /// Set and get the Main Progress Bar
        /// </summary>
        public virtual ProgressBarDelegate ProgressBar
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #endregion
    }
}