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
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Text;
using System.Windows;
using VLAControlLib;

#endregion

namespace VLAShortestPath
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

            sb.AppendLine("What is the Shortest Path Problem?");
            sb.AppendLine("In graph theory, the shortest path problem is the problem of finding a path " +
                          "between two vertices (or nodes) in a graph such that the sum of the weights" +
                          "of its constituent edges is minimized.");

            sb.AppendLine("#Algorithm");
            sb.AppendLine("Dijkstra's algorithm");
            sb.AppendLine("For a given source vertex (node) in the graph, the algorithm finds the path with " +
                          "lowest cost (i.e. the shortest path) between that vertex and every other vertex. " +
                          "It can also be used for finding costs of shortest paths from a single vertex to a " +
                          "single destination vertex by stopping the algorithm once the shortest path to the " +
                          "destination vertex has been determined. For example, if the vertices of the graph " +
                          "represent cities and edge path costs represent driving distances between pairs of " +
                          "cities connected by a direct road, Dijkstra's algorithm can be used to find the " +
                          "shortest route between one city and all other cities.");

            sb.AppendLine("#Uses");
            sb.AppendLine("In Networking - Routing Protocols");
            sb.AppendLine("# Circular list - Here the last node of the list points to the first node.");

            InfoRichTextBox.AppendText(sb.ToString());
        }

        private void BackToMainMenuClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Main());
        }
    }
}