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
// //	File Name:		Switcher.cs
// //	Created:		06-04-2013 Time: 00:03
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Windows.Controls;

#endregion

namespace VLAControlLib
{
    /// <summary>
    ///     Static class to switch the "DefaultPage" to a new UserControl
    /// </summary>
    public static class Switcher
    {
        /// <summary>
        ///     Static DefaultPage is the main page that allows for UserControl switching
        /// </summary>
        public static DefaultPage PageSwitcher;

        /// <summary>
        ///     Static Switch is the Method that calls the DefaultPage to switch
        /// </summary>
        /// <param name="newPage">The new UserControl we are switching to</param>
        public static void Switch(UserControl newPage)
        {
            try
            {
                PageSwitcher.Navigate(newPage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     Static Switch is the Method that calls the DefaultPage to switch
        /// </summary>
        /// <param name="newPage">The new UserControl we are switching to</param>
        /// <param name="page">The Default Page for the switch</param>
        public static void Switch(UserControl newPage, DefaultPage page)
        {
            try
            {
                page.Navigate(newPage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}