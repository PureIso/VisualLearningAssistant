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
// //	File Name:		EnumClass.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

namespace VLAPluginLib
{
    /// <summary>
    ///     Log Types of information to display
    /// </summary>
    public enum LogType
    {
        /// <summary>
        ///     Error Messages
        /// </summary>
        Error,

        /// <summary>
        ///     Information Messages
        /// </summary>
        Info,

        /// <summary>
        ///     A simple message with no icons
        /// </summary>
        None,
    }

    /// <summary>
    ///     Plug-in types to specify how it interacts with the system
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        ///     This plug-in type is for the main application for extended features
        /// </summary>
        System,

        /// <summary>
        ///     This plug-in type are for lessons or Illustrations
        /// </summary>
        Illustration,
    }
}