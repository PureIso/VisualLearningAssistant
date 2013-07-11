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
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Text;
using System.Windows;
using VLAControlLib;

#endregion

namespace VLALinkedList
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
            sb.AppendLine("What is a Linked List?");
            sb.AppendLine("A Linked List is a data structure consisting of a group of nodes which " +
                          "together represent a sequence.");
            sb.AppendLine(
                "The principal benefit of a linked list over a conventional array is that the" +
                " list elements can easily be inserted or removed without reallocation or " +
                "reorganization of the entire structure");

            sb.AppendLine("Linked List Representations:");
            sb.AppendLine("# Singly linked list - Singly linked lists contain nodes " +
                          "which have a data field as well as a next field, which points to " +
                          "the next node in the linked list.");
            sb.AppendLine("# Circular list - Here the last node of the list points to the first node.");
            InfoRichTextBox.AppendText(sb.ToString());
        }

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }
    }
}