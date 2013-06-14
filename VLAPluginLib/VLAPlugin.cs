#region License and File Info

// 
// Copyright (C) 2013  Olawale Egbeyemi
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// 
// //========================================================================//
// //========================================================================//
// //	Author:			Olawale Egbeyemi
// //	Solution Name:	VisualLearningAssistant	
// //	File Name:		VLAPlugin.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections.Generic;
using VLAControlLib;

#endregion

namespace VLAPluginLib
{
    /// <summary>
    ///     The Abstract Plug in class to allow for multiple plug in in one for future updates
    ///     This should be the entry point's base class
    /// </summary>
    public abstract class VLAPlugin : IVLAIllustration, IVLASystem
    {
        #region Properties

        /// <summary>
        ///     Set the plug-in type
        /// </summary>
        public PluginType PluginType { get; protected set; }

        /// <summary>
        ///     How any plug-in to communicate with the main log box
        /// </summary>
        protected LogTextDelegate HLog { get; private set; }

        /// <summary>
        ///     Allow any plug-in to communicate with the progress bar
        /// </summary>
        protected ProgressBarDelegate HProgressBar { get; private set; }

        /// <summary>
        ///     Allow any plug-in to communicate to the Default Page user control
        /// </summary>
        protected DefaultPage HDefaultPage { get; private set; }

        #endregion

        #region IVLAIllustrations

        /// <summary>
        ///     The name of the application
        /// </summary>
        public string IllustrationName { get; set; }

        /// <summary>
        ///     The author of the application
        /// </summary>
        public string IllustrationAuthor { get; set; }

        /// <summary>
        ///     The description of the application
        /// </summary>
        public string IllustrationDescription { get; set; }

        /// <summary>
        ///     The version of the application
        /// </summary>
        public string IllustrationVersion { get; set; }

        /// <summary>
        ///     The supported languages of the application
        /// </summary>
        public List<string> IllustrationSupportedLanguages { get; set; }

        /// <summary>
        ///     The module the illustration is part of.
        /// </summary>
        public List<string> IllustrationModuleName { get; set; }

        /// <summary>
        ///     Display the application
        /// </summary>
        public virtual void IllustrationDisplay()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IVLASystem

        /// <summary>
        ///     The Add-on extension Name which is used in the menu item
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        ///     The Add-on extension load function
        /// </summary>
        public virtual void SystemLoad()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        ///     Display the application
        /// </summary>
        /// <param name="defaultPage">Set the Default Page</param>
        /// <param name="hostLog">Set the Host Log TextBox</param>
        /// <param name="progressbar">The Main progress bar</param>
        public void Display(DefaultPage defaultPage, LogTextDelegate hostLog, ProgressBarDelegate progressbar)
        {
            HProgressBar = progressbar;
            HDefaultPage = defaultPage;
            HLog = hostLog;
            if (PluginType == PluginType.Illustration)
                IllustrationDisplay();
            else
                SystemLoad();
        }
    }
}