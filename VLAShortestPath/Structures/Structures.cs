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
// //	File Name:		Structures.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections.Generic;
using System.Windows;
using VLAShortestPath.Core_Logic;

#endregion

namespace VLAShortestPath.Structures
{
    /// <summary>Structure that olds the path to a node and the total weight</summary>
    public struct ShortestPath
    {
        /// <summary>The path</summary>
        public List<Vertex> Paths;

        /// <summary>The total weight/cost</summary>
        public uint TotalWeight;
    }

    /// <summary>Contains structure for the edge of a vertex or connection path details</summary>
    [Serializable]
    public struct Edge
    {
        /// <summary>The destination vertex</summary>
        public Vertex Destination;

        /// <summary>The Weight or cost it takes to get from origin to the destination</summary>
        public uint Weight;
    }

    /// <summary>Contains Details about of data needed to remove a UI element in a panel</summary>
    public struct RemoveUiElement
    {
        /// <summary>
        ///     The UI Element
        /// </summary>
        public UIElement UiElement;

        /// <summary>
        ///     Boolean value to identify if the UI Element is present
        /// </summary>
        public bool UiElementPresent;
    }

    [Serializable]
    public struct SaveConnections
    {
        public Vertex From;
        public Vertex To;
        public uint Weight;
    }
}