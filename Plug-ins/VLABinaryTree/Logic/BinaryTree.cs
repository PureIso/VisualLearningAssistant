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
// //	Solution Name:	VLABinaryTree	
// //	File Name:		BinaryTree.cs
// //	Created:		16-04-2013 Time: 20:33
// //	Last Edited:	16-04-2013 Time: 20:38
// //=======================================================================//
// //=======================================================================//

#endregion

using System;
using System.Windows.Controls;
using VLAControlLib;

namespace VLABinaryTree.Logic
{
    /// <summary>
    ///     The Binary Tree Class
    ///     data structure in which each node has at most two child nodes,
    ///     usually distinguished as "left" and "right"
    /// </summary>
    public class BinaryTree
    {
        /// <summary>The last node that was added to the tree</summary>
        internal Node CurrentNode { get; set; }

        /// <summary>The root node in the tree</summary>
        internal Node RootNode { get; set; }

        /// <summary>Binary Tree constructor</summary>
        public BinaryTree()
        {
            CurrentNode = new Node(0, null, null, null);
            RootNode = null;
        }

        /// <summary>Add Value into the binary tree.</summary>
        /// <param name="value">The value to add.</param>
        /// <exception cref="Exception"></exception>
        public void Insert(int value)
        {
            try
            {
                if (CurrentNode.Value == 0 || CurrentNode.Value == value)
                    CurrentNode.Value = value; //if current node is empty of same
                else if (CurrentNode.Value > value) //if value is less than current node value
                {
                    if (CurrentNode.LeftChild == null) //if current node left child is empty
                        CurrentNode.LeftChild = new Node(value, CurrentNode, null, null);
                    else //current node left child is not empty
                    {
                        CurrentNode = CurrentNode.LeftChild; //set the current node to the left child
                        Insert(value); //call the Add function again - Recursion
                    }
                }
                else if (CurrentNode.Value < value) //if value is greater than current node value
                {
                    if (CurrentNode.RightChild == null) //if current node right child is empty
                        CurrentNode.RightChild = new Node(value, CurrentNode, null, null);
                    else //current node right child is not empty
                    {
                        CurrentNode = CurrentNode.RightChild; //set the current node to the right child
                        Insert(value); //call the Add function again - Recursion
                    }
                }
                if (RootNode == null) RootNode = CurrentNode; //Keep track of the root node
                CurrentNode = RootNode;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DrawTree(Grid canvas, int xAxis, int yAxis, TimeSpan duration)
        {
            if (RootNode == null) return; //Don't draw anything
            // Position all of the nodes.
            RootNode.PositionNode(ref xAxis, yAxis);
            // Draw all of the links.
            RootNode.DrawBranches(canvas, duration);
            // Draw all of the nodes.
            RootNode.DrawNode(canvas);
        }
    }
}