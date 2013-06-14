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
// //	File Name:		IsoAppHeader.xaml.cs
// //	Created:		06-04-2013 Time: 00:03
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Windows.Controls;
using System.Windows.Media;

#endregion

namespace VLAControlLib
{
    /// <summary>
    ///     Interaction logic for AppHeader.xaml
    /// </summary>
    public partial class IsoAppHeader : UserControl
    {
        /// <summary>
        /// </summary>
        public IsoAppHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The IsoAppHeader text
        /// </summary>
        public string Header
        {
            get { return FormTitleBlock.Text; }
            set { FormTitleBlock.Text = value; }
        }

        /// <summary>
        ///     The UsoAppHeader Image Source
        /// </summary>
        public ImageSource ImageSource
        {
            get { return ImageIcon.Source; }
            set { ImageIcon.Source = value; }
        }
    }
}