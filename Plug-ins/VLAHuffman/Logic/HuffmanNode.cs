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
// //	File Name:		HuffmanNode.cs
// //	Created:		14-04-2013 Time: 18:47
// //	Last Edited:	16-04-2013 Time: 20:45
// //=======================================================================//
// //=======================================================================//

#endregion

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VLAHuffman.Design;

namespace VLAHuffman.Logic
{
    public class HuffmanNode
    {
        internal char Symbol { get; set; }
        internal int Frequency { get; set; }
        internal HuffmanNode RightChild { get; set; }
        internal HuffmanNode LeftChild { get; set; }

        internal List<bool> Traverse(char symbol, List<bool> data)
        {
            // Leaf
            if (RightChild == null && LeftChild == null)
            {
                return symbol.Equals(Symbol) ? data : null;
            }
            List<bool> left = null;
            List<bool> right = null;

            if (LeftChild != null)
            {
                List<bool> leftPath = new List<bool>();
                leftPath.AddRange(data);
                leftPath.Add(false);

                left = LeftChild.Traverse(symbol, leftPath);
            }

            if (RightChild != null)
            {
                List<bool> rightPath = new List<bool>();
                rightPath.AddRange(data);
                rightPath.Add(true);
                right = RightChild.Traverse(symbol, rightPath);
            }

            return left ?? right;
        }

        #region Drawing Routines

        private const int Radius = 30;
        private const int DistanceBetweenEachChildNode = 20;
        private const int HightBetweenParentAndChildNode = 20;
        private int _currentNodeXaxis;
        private int _currentNodeYaxis;

        // Find the position where we should draw the centre of this node.
        internal void PositionNode(ref int xAxis, int yAxis)
        {
            _currentNodeYaxis = yAxis + Radius/2;

            if (LeftChild != null) // Position our left sub tree.
                LeftChild.PositionNode(ref xAxis, yAxis + Radius + HightBetweenParentAndChildNode);

            if (LeftChild != null && RightChild != null)
                xAxis += DistanceBetweenEachChildNode; //if we have both children set space between them

            if (RightChild != null) // Position our right sub tree.
                RightChild.PositionNode(ref xAxis, yAxis + Radius + HightBetweenParentAndChildNode);

            // Position our self.
            if (LeftChild != null)
            {
                if (RightChild != null)
                    //If we have both children centre the node
                    _currentNodeXaxis = (LeftChild._currentNodeXaxis + RightChild._currentNodeXaxis)/2;
                else
                    //only left child - centre it
                    _currentNodeXaxis = LeftChild._currentNodeXaxis + 10;
            }
            else if (RightChild != null)
            {
                // No left child. centre over right child.
                _currentNodeXaxis = RightChild._currentNodeXaxis - 10;
            }
            else
            {
                // No children. We're on our own.
                _currentNodeXaxis = xAxis + Radius/2;
                xAxis += Radius;
            }
        }

        // Draw branches to our children.
        public void DrawBranches(Canvas grid, int thickness)
        {
            Storyboard storyboard = new Storyboard();
            Line linePath;

            if (LeftChild != null)
            {
                //0

                #region Line

                linePath = new Line // Create a new Line
                    {
                        X1 = _currentNodeXaxis + (Radius/2), // x position
                        Y1 = _currentNodeYaxis + Radius, // y position
                        X2 = LeftChild._currentNodeXaxis + (Radius/2), // destination x value
                        Y2 = LeftChild._currentNodeYaxis, // destination y value
                        Stroke = new SolidColorBrush(Colors.Black), // Stroke colour
                        StrokeThickness = thickness // Stroke thickness (0 = invisible)
                    };

                #endregion

                //================
                int cordx = ((_currentNodeXaxis + LeftChild._currentNodeXaxis)/2) - 5;
                int cordy = ((_currentNodeYaxis + LeftChild._currentNodeYaxis)/2);
                TextBlock textBlock = new TextBlock
                    {
                        Text = 0.ToString(),
                        Foreground = new SolidColorBrush(Colors.Black),
                        Margin = new Thickness(cordx, cordy, 0, 0)
                    };
                grid.Children.Add(textBlock);
                //================

                // add the line to the Grid(or other controls) and begin animations
                grid.Children.Add(linePath);

                // begin storyboard animations
                storyboard.Begin();
                LeftChild.DrawBranches(grid, thickness);
            }
            if (RightChild == null) return;
            //1

            #region Line

            linePath = new Line // Create a new Line
                {
                    X1 = _currentNodeXaxis + (Radius/2), // x position
                    Y1 = _currentNodeYaxis + Radius, // y position
                    X2 = RightChild._currentNodeXaxis + (Radius/2), // destination x value
                    Y2 = RightChild._currentNodeYaxis, // destination y value
                    Stroke = new SolidColorBrush(Colors.Black), // Stroke colour
                    StrokeThickness = thickness // Stroke thickness (0 = invisible)
                };

            #endregion

            //================
            int cordnx = ((_currentNodeXaxis + RightChild._currentNodeXaxis)/2) + 12;
            int cordny = ((_currentNodeYaxis + RightChild._currentNodeYaxis)/2);
            TextBlock textBlockn = new TextBlock
                {
                    Text = 1.ToString(),
                    Foreground = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(cordnx, cordny, 0, 0)
                };
            grid.Children.Add(textBlockn);
            //================

            // add the line to the Grid(or other controls) and begin animations
            grid.Children.Add(linePath);

            // begin storyboard animations
            storyboard.Begin();
            RightChild.DrawBranches(grid, thickness);
        }

        // Draw the node in its assigned position.
        public void DrawNode(Canvas grid, int thickness)
        {
            NodeDesign node = new NodeDesign
                {
                    Width = Radius,
                    Height = Radius,
                    NodeText =
                        {
                            Content = Symbol.ToString() == "*" ? Frequency.ToString() : Symbol.ToString(),
                            FontSize = 10,
                            Foreground = new SolidColorBrush(Colors.Black)
                        },
                    NodeCircle = {StrokeThickness = thickness, Stroke = new SolidColorBrush(Colors.Red)},
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(_currentNodeXaxis, _currentNodeYaxis, 0, 0)
                };

            grid.Children.Add(node);

            // Draw our children.
            if (LeftChild != null) LeftChild.DrawNode(grid, thickness);
            if (RightChild != null) RightChild.DrawNode(grid, thickness);
        }

        #endregion
    }
}