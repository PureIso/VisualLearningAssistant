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
// //	File Name:		AVLBinaryTree.cs
// //	Created:		16-04-2013 Time: 20:33
// //	Last Edited:	16-04-2013 Time: 20:38
// //=======================================================================//
// //=======================================================================//

#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using VLAControlLib;

namespace VLABinaryTree.Logic
{
    /// <summary>
    ///     The Binary Tree Class
    ///     data structure in which each node has at most two child nodes,
    ///     usually distinguished as "left" and "right"
    /// </summary>
    public class AVLBinaryTree
    {
        /// <summary>The root node in the tree</summary>
        public Node RootNode { get; set; }

        /// <summary>Binary Tree constructor</summary>
        public AVLBinaryTree()
        {
            RootNode = null;
        }

        public string Preorder([Optional, DefaultParameterValue(null)] Node node)
        {
            string ret = "";
            if (node == null) return ret;

            ret += " " + node.Value;
            ret += Preorder(node.LeftChild);
            ret += Preorder(node.RightChild);
            return ret;
        }

        public string Postorder([Optional, DefaultParameterValue(null)] Node node)
        {
            string ret = "";
            if (node == null) return ret;
            ret += Postorder(node.LeftChild);
            ret += Postorder(node.RightChild);
            ret += " " + node.Value;
            return ret;
        }

        public string Inorder([Optional, DefaultParameterValue(null)] Node node)
        {
            string ret = "";
            if (node == null) return ret;
            ret += Inorder(node.LeftChild);
            ret += " " + node.Value;
            ret += Inorder(node.RightChild);
            return ret;
        }

        public string BreathFirstSearch([Optional, DefaultParameterValue(null)] Node node)
        {
            string ret = "";
            if (node == null)
                return ret;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                if (current == null) continue;
                queue.Enqueue(current.LeftChild);
                queue.Enqueue(current.RightChild);

                ret += " " + current.Value;
            }
            return ret;
        }

        public string DepthFirstSearch([Optional, DefaultParameterValue(null)] Node node)
        {
            string ret = "";
            if (node == null)
                return ret;

            Stack<Node> stack = new Stack<Node>();
            stack.Push(node);

            while (stack.Count > 0)
            {
                Node current = stack.Pop();
                ret += " " + current.Value;

                if (current.LeftChild != null)
                    stack.Push(current.LeftChild);
                if (current.RightChild != null)
                    stack.Push(current.RightChild);
            }
            return ret;
        }

        /// <summary>Add Value into the binary tree.</summary>
        /// <param name="node"></param>
        /// <param name="value">The value to add.</param>
        /// <exception cref="Exception"></exception>
        public Node Insert([Optional, DefaultParameterValue(null)] Node node, int value)
        {
            try
            {
                if (node == null)
                    node = new Node(value, null, null, null); //if current node is empty of same

                else if (value.CompareTo(node.Value) < 0)
                {
                    node.LeftChild = Insert(node.LeftChild, value);
                    node = ReBanlance(node);
                }
                else if (value.CompareTo(node.Value) > 0)
                {
                    node.RightChild = Insert(node.RightChild, value);
                    node = ReBanlance(node);
                }
                RootNode = node;
                return node;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private int GetBalance(Node root)
        {
            return GetHeight(root.LeftChild) - GetHeight(root.RightChild);
        }

        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;
            return 1 + Math.Max(GetHeight(node.LeftChild), GetHeight(node.RightChild));
        }

        private Node ReBanlance(Node node)
        {
            try
            {
                int heightDiff = GetBalance(node);

                if (heightDiff > 1) //left case
                {
                    if (GetBalance(node.LeftChild) > 0) //left left case
                    {
                        node = LeftRotation(node);
                    }
                    else // left right case
                    {
                        node = LeftRightRotation(node);
                    }
                }
                else if (heightDiff < -1) //right case
                {
                    if (GetBalance(node.RightChild) < 0) //left left case
                    {
                        node = RightRotation(node);
                    }
                    else // left right case
                    {
                        node = RightLeftRotation(node);
                    }
                }
                return node;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Left Rotation
        private Node LeftRotation(Node parent)
        {
            try
            {
                Node child = parent.LeftChild;
                parent.LeftChild = child.RightChild;
                child.RightChild = parent;
                return child;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Right Rotation
        private Node RightRotation(Node parent)
        {
            try
            {
                Node child = parent.RightChild;
                parent.RightChild = child.LeftChild;
                child.LeftChild = parent;
                return child;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Node RightLeftRotation(Node parent)
        {
            try
            {
                Node child = parent.RightChild;
                parent.RightChild = LeftRotation(child);
                return RightRotation(parent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Node LeftRightRotation(Node parent)
        {
            try
            {
                Node child = parent.LeftChild;
                parent.LeftChild = RightRotation(child);
                return LeftRotation(parent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DrawTree(Grid canvas, int xAxis, int yAxis, int thickness, TimeSpan duration)
        {
            if (RootNode == null) return; //Don't darw anything
            // Position all of the nodes.
            RootNode.PositionNode(ref xAxis, yAxis);
            // Draw all of the links.
            RootNode.DrawBranches(canvas, duration);
            // Draw all of the nodes.
            RootNode.DrawNode(canvas);
        }
    }
}