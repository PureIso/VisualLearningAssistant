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
// //	File Name:		PluginService.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VLAPluginLib;

#endregion

namespace VisualLearningAssistant
{
    /// <summary>
    ///     Internal plug-in service class that get's all the information regarding the plug-in
    /// </summary>
    public class PluginService
    {
        private readonly List<VLAPlugin> _pluginIllustrations = new List<VLAPlugin>();
        private readonly List<VLAPlugin> _pluginSystem = new List<VLAPlugin>();

        /// <summary>
        ///     Plug-in service constructor
        /// </summary>
        /// <param name="folderPath"> The plug-in folder path </param>
        public PluginService(string folderPath)
        {
            try
            {
                foreach (string plugin in Directory.GetFiles(folderPath))
                {
                    FileInfo file = new FileInfo(plugin);
                    if (file.Extension.Equals(".dll"))
                    {
                        AddPlugin(plugin);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     Plug-in Data
        /// </summary>
        public List<VLAPlugin> PluginIllustrations
        {
            get { return _pluginIllustrations; }
        }

        /// <summary>
        ///     Plug-in system add-on
        /// </summary>
        public List<VLAPlugin> PluginSystem
        {
            get { return _pluginSystem; }
        }


        private void AddPlugin(string pluginPath)
        {
            try
            {
                Assembly pluginAssembly = Assembly.LoadFrom(pluginPath); //Load assembly given its full name and path

                foreach (Type pluginType in pluginAssembly.GetTypes())
                {
                    if (!pluginType.IsPublic) continue; //break the for each loop to next iteration if any
                    if (pluginType.IsAbstract) continue; //break the for each loop to next iteration if any
                    //search for specified interface while ignoring case sensitivity
                    if (pluginType.BaseType == null || pluginType.BaseType.FullName != "VLAPluginLib.VLAPlugin")
                        continue;
                    //New plug-in information setting
                    VLAPlugin pluginInterfaceInstance =
                        (VLAPlugin) (Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString())));

                    if (pluginInterfaceInstance.PluginType == PluginType.Illustration)
                        _pluginIllustrations.Add(pluginInterfaceInstance);
                    else
                    {
                        _pluginSystem.Add(pluginInterfaceInstance);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}