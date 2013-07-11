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
// //	Solution Name:	VLAHuffman	
// //	File Name:		EntryPoint.cs
// //	Created:		14-04-2013 Time: 18:47
// //	Last Edited:	16-04-2013 Time: 20:45
// //=======================================================================//
// //=======================================================================//

#endregion

using System.Collections.Generic;
using VLAControlLib;
using VLAPluginLib;

//===============================================
//Author:      Ola
//Description: VLA Huffman Entry Point
//===============================================

namespace VLAHuffman
{
    public class EntryPoint : VLAPlugin
    {
        public static DefaultPage HostDefaultPage; //Get the Host DefaultPage user Control
        public static LogTextDelegate HostLogger; //Get the Host Log Box

        //Loaded when it's discovered by the plug-in service and all base variables are set
        public EntryPoint()
        {
            base.IllustrationName = "Huffman Coding Illustration";
            base.IllustrationVersion = "1.0.0.0";
            base.IllustrationSupportedLanguages = new List<string>(new[] {"English"});
            base.PluginType = PluginType.Illustration;
            base.IllustrationAuthor = "Ola";
            base.IllustrationDescription = "Huffman Algorithm Illustration";
            base.IllustrationModuleName = new List<string>(
                new[]
                    {
                        "Lossless Data Compression",
                        "Encoding Algorithm",
                        "Compression",
                        "Algorithm"
                    });
            HostDefaultPage = base.HDefaultPage;
        }

        public override void IllustrationDisplay()
        {
            HostDefaultPage = base.HDefaultPage;
            HostLogger = base.HLog;
            HostDefaultPage.Switch = new Main(); //Load the Main.xaml page
        }
    }
}