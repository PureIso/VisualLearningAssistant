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
// //	File Name:		EntryPoint.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Collections.Generic;
using VLAControlLib;
using VLAPluginLib;

#endregion

namespace VLALinkedList
{
    public class EntryPoint : VLAPlugin
    {
        public static DefaultPage HostDefaultPage; //Get the Host DefaultPage user Control
        public static LogTextDelegate HostLogger; //Get the Host Log Box
        public static ProgressBarDelegate HostProgressBar; //Get the Host ProgressBar

        //Loaded when it's discovered by the plug-in service and all base variables are set
        public EntryPoint()
        {
            base.IllustrationName = "Linked List Illustration";
            base.IllustrationVersion = "1.0.0.0";
            base.IllustrationSupportedLanguages = new List<string>(new[] {"English"});
            base.PluginType = PluginType.Illustration;
            base.IllustrationAuthor = "Ola";
            base.IllustrationDescription = "Linked List Data Structure Illustration";
            base.IllustrationModuleName = new List<string>(
                new[]
                    {
                        "Data Structures",
                    });
        }

        public override void IllustrationDisplay()
        {
            HostDefaultPage = base.HDefaultPage;
            HostLogger = base.HLog;
            HostProgressBar = base.HProgressBar;
            HostDefaultPage.Switch = new Main();
        }
    }
}