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
// //	File Name:		Functions.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region References

using System;
using System.IO;

#endregion

namespace VLAPluginLib
{
    /// <summary>
    ///     Contains various functions developers can use
    /// </summary>
    public static class Functions
    {
        /// <summary>
        ///     Checks and creates a file if it doesn't exist.
        /// </summary>
        /// <param name="fileName">The file Name.</param>
        /// <returns>True if the file existed before and false if it did not.</returns>
        public static bool CheckAndCreateFile(string fileName)
        {
            bool returnValue = true;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
            if (!File.Exists(filePath))
            {
                returnValue = false;
                using (FileStream fileStream = File.Create(filePath))
                {
                    fileStream.Close();
                }
            }
            return returnValue;
        }

        /// <summary>
        ///     Checks and creates a directory if it doesn't exist inside the base directory.
        /// </summary>
        /// <param name="directoryName">The directory Name.</param>
        /// <returns>True if the file existed before and false if it did not.</returns>
        public static bool CheckAndCreateDirectory(string directoryName)
        {
            bool returnValue = true;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + directoryName;
            if (!Directory.Exists(filePath))
            {
                returnValue = false;
                Directory.CreateDirectory(directoryName);
            }
            return returnValue;
        }
    }
}