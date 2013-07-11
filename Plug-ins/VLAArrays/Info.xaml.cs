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
// //	File Name:		Info.xaml.cs
// //	Created:		06-04-2013 Time: 00:08
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Text;
using System.Windows;
using VLAControlLib;

#endregion

namespace VLAArrays
{
    /// <summary>
    ///     Interaction logic for Info.xaml
    /// </summary>
    public partial class Info
    {
        public Info()
        {
            InitializeComponent();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("What is an Array?");
            sb.AppendLine("Array is a single programming variable that can store many " +
                          "values inside it.");
            sb.AppendLine("When is it useful?");
            sb.AppendLine("When you want to have a variable with a fixed amount of related " +
                          "data types.");
            sb.AppendLine("How do you search an Array?");
            InfoRichTextBox.AppendText(sb.ToString());
        }

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }
    }
}