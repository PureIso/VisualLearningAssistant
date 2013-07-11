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
// //	File Name:		IVLAIllustration.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Collections.Generic;

#endregion

namespace VLAPluginLib
{
    /// <summary>
    ///     Main Plug in to allow users to develop Illustrations
    /// </summary>
    public interface IVLAIllustration
    {
        #region Plugin App

        /// <summary>
        ///     The name of the application
        /// </summary>
        string IllustrationName { get; set; }

        /// <summary>
        ///     The version of the application
        /// </summary>
        string IllustrationVersion { get; set; }

        /// <summary>
        ///     The author of the application
        /// </summary>
        string IllustrationAuthor { get; set; }

        /// <summary>
        ///     The description of the application
        /// </summary>
        string IllustrationDescription { get; set; }

        /// <summary>
        ///     The supported languages of the application
        /// </summary>
        List<string> IllustrationSupportedLanguages { get; set; }

        /// <summary>
        ///     The module the illustration is part of.
        /// </summary>
        List<string> IllustrationModuleName { get; set; }

        /// <summary>
        ///     Display the application
        /// </summary>
        void IllustrationDisplay();

        #endregion
    }
}