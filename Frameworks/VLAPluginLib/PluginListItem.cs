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
// //	File Name:		PluginListItem.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.ComponentModel;

#endregion

namespace VLAPluginLib
{
    /// <summary>
    ///     Data Type for the Plug-in List View
    /// </summary>
    public class PluginListItem : INotifyPropertyChanged
    {
        private bool _selected;

        /// <summary>
        ///     The constructor
        /// </summary>
        /// <param name="name">The name of the plug-in</param>
        /// <param name="version">The version of the plug-in</param>
        /// <param name="ischecked">If the plug-in is checked or selected</param>
        public PluginListItem(string name, string version, bool ischecked)
        {
            Name = name;
            Verson = version;
            Selected = ischecked;
        }

        /// <summary>
        ///     The name of the application
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The version of the application
        /// </summary>
        public string Verson { get; set; }

        /// <summary>
        ///     Check the box of the checkbox
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected == value) return;
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        /// <summary>
        ///     The Event Change for the checkbox
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}