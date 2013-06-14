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
// //	Created:		06-04-2013 Time: 00:08
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using VLAPluginLib;

#endregion

namespace VLASaveLog
{
    public class EntryPoint : VLAPlugin
    {
        private static LogTextDelegate _hostLogger; //Get the Host Log Box

        //Loaded when it's discovered by the plug-in service and all base variables are set
        public EntryPoint()
        {
            base.SystemName = "Save Log";
            base.PluginType = PluginType.System;
        }

        public override void SystemLoad()
        {
            _hostLogger = base.HLog;
            SaveLog();
        }

        private static void SaveLog()
        {
            try
            {
                _hostLogger("Saving log information....", LogType.Info);
                RichTextBox richTextBox = _hostLogger(null, LogType.None);
                TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                MemoryStream ms = new MemoryStream();
                textRange.Save(ms, DataFormats.Text);
                string path = "Log-" + DateTime.Today.ToString().Replace("/", "-").Remove(11) + ".txt";
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                file.Write(ms.ToArray(), 0, ms.ToArray().Length);
                file.Close();
                ms.Close();
                _hostLogger("Log information saved Successful.", LogType.Info);
                MessageBox.Show("Log information saved successfully!", "Visual Learning Assistant", MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                _hostLogger("Save Error: " + e.Message, LogType.Error);
                MessageBox.Show(e.Message, "Visual Learning Assistant", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}