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
// //	File Name:		IVLASystem.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

namespace VLAPluginLib
{
    /// <summary>
    ///     Main Add-on to allow users to develop a non Illustration extensions
    /// </summary>
    public interface IVLASystem
    {
        #region System

        /// <summary>
        ///     The System Add-on Name
        /// </summary>
        string SystemName { get; set; }

        /// <summary>
        ///     Load the Add-on
        /// </summary>
        void SystemLoad();

        #endregion
    }
}