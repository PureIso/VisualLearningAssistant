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
// //	File Name:		Node.xaml.cs
// //	Created:		16-04-2013 Time: 21:31
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace VLAControlLib
{
    /// <summary>
    ///     Interaction logic for Node.xaml
    /// </summary>
    public partial class Node
    {
        #region Properties

        /// <summary>
        ///     The Node Value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        ///     The Parent Node
        /// </summary>
        public Node ParentNode { get; set; }

        /// <summary>
        ///     The Left Node
        /// </summary>
        public Node LeftChild { get; set; }

        /// <summary>
        ///     The right Node
        /// </summary>
        public Node RightChild { get; set; }

        #endregion

        /// <summary>
        ///     An Integer Node.
        /// </summary>
        /// <param name="value">The value in the node.</param>
        /// <param name="parent">The parent of the node.</param>
        /// <param name="leftChild">The left child of the node.</param>
        /// <param name="rightChild">The right child of the node</param>
        public Node(int value, Node parent, Node leftChild, Node rightChild)
        {
            InitializeComponent();
            Value = value;
            ParentNode = parent;
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        #region Drawing Animations

        private Line _linePath;
        private const int Radius = 30;
        private const int DistanceBetweenEachChildNode = 30;
        private const int HightBetweenParentAndChildNode = 30;
        private int _currentNodeXaxis;
        private int _currentNodeYaxis;

        /// <summary>
        ///     Find the position where we should draw the centre of this node.
        /// </summary>
        /// <param name="xAxis">The x Axis</param>
        /// <param name="yAxis">The y Axis</param>
        public void PositionNode(ref int xAxis, int yAxis)
        {
            _currentNodeYaxis = yAxis + Radius/2;

            if (LeftChild != null) // Position our left sub tree.
                LeftChild.PositionNode(ref xAxis, yAxis + Radius + HightBetweenParentAndChildNode);

            if (LeftChild != null && RightChild != null)
                xAxis += DistanceBetweenEachChildNode; //if we have both children set space between them

            if (RightChild != null) // Position our right sub tree.
                RightChild.PositionNode(ref xAxis, yAxis + Radius + HightBetweenParentAndChildNode);

            // Position this
            if (LeftChild != null)
            {
                if (RightChild != null)
                    //If we don't have both children centre the node
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

        /// <summary>
        ///     Draw the node on the specified Grid
        /// </summary>
        /// <param name="container">The Specified Grid</param>
        public void DrawNode(Grid container)
        {
            Width = Radius; // Set Node width
            Height = Radius; // Set Node height
            NodeText.Content = Value; // Set Node text
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Margin = new Thickness(_currentNodeXaxis, _currentNodeYaxis, 0, 0);

            Canvas.SetLeft(this, _currentNodeXaxis); // Set x position
            Canvas.SetTop(this, _currentNodeYaxis); // Set y position
            Panel.SetZIndex(this, Value); // Set draw position on top of everything

            container.Children.Add(this);

            var animation = (Storyboard) FindResource("NodeStoryBoard");
            animation.Begin();

            // Draw our children.
            if (LeftChild != null) LeftChild.DrawNode(container);
            if (RightChild != null) RightChild.DrawNode(container);
        }

        /// <summary>
        ///     Draw the branches that shows node connections.
        /// </summary>
        /// <param name="container">The Grid to draw on.</param>
        /// <param name="duration">The duration of the drawing animation.</param>
        public void DrawBranches(Grid container, TimeSpan duration)
        {
            DoubleAnimation lineAnimationX; // Animation for X value for the line
            DoubleAnimation lineAnimationY; // Animation for Y value for the line
            var storyboard = new Storyboard();

            if (LeftChild != null)
            {
                #region Line

                _linePath = new Line // Create a new Line
                    {
                        X1 = _currentNodeXaxis + (Radius/2), // x position
                        Y1 = _currentNodeYaxis + Radius, // y position
                        X2 = LeftChild._currentNodeXaxis + (Radius/2), // destination x value
                        Y2 = LeftChild._currentNodeYaxis, // destination y value
                        Stroke = Brushes.Black, // Stroke colour
                        StrokeThickness = 5 // Stroke thickness (0 = invisible)
                    };
                Panel.SetZIndex(_linePath, 90); // Set draw position on top of everything but below the Node

                // Create the Line Animations
                lineAnimationX = new DoubleAnimation(_linePath.X1, _linePath.X2, new Duration(duration));
                lineAnimationY = new DoubleAnimation(_linePath.Y1, _linePath.Y2, new Duration(duration));

                // Set Target Properties for the animations
                Storyboard.SetTarget(lineAnimationX, _linePath);
                Storyboard.SetTargetName(lineAnimationX, _linePath.Name);
                Storyboard.SetTargetProperty(lineAnimationX, new PropertyPath(Line.X2Property));

                Storyboard.SetTarget(lineAnimationY, _linePath);
                Storyboard.SetTargetName(lineAnimationY, _linePath.Name);
                Storyboard.SetTargetProperty(lineAnimationY, new PropertyPath(Line.Y2Property));

                #endregion

                // add the line to the Grid(or other controls) and begin animations
                container.Children.Add(_linePath);

                // Add the animations to the storyboard
                storyboard.Children.Add(lineAnimationX);
                storyboard.Children.Add(lineAnimationY);

                // begin storyboard animations
                storyboard.Begin();
                LeftChild.DrawBranches(container, duration);
            }
            if (RightChild == null) return;

            #region Line

            _linePath = new Line // Create a new Line
                {
                    X1 = _currentNodeXaxis + (Radius/2), // x position
                    Y1 = _currentNodeYaxis + Radius, // y position
                    X2 = RightChild._currentNodeXaxis + (Radius/2), // destination x value
                    Y2 = RightChild._currentNodeYaxis, // destination y value
                    Stroke = Brushes.Black, // Stroke colour
                    StrokeThickness = 5 // Stroke thickness (0 = invisible)
                };
            Panel.SetZIndex(_linePath, 90); // Set draw position on top of everything but below the Node

            // Create the Line Animations
            lineAnimationX = new DoubleAnimation(_linePath.X1, _linePath.X2, new Duration(duration));
            lineAnimationY = new DoubleAnimation(_linePath.Y1, _linePath.Y2, new Duration(duration));

            // Set Target Properties for the animations
            Storyboard.SetTarget(lineAnimationX, _linePath);
            Storyboard.SetTargetName(lineAnimationX, _linePath.Name);
            Storyboard.SetTargetProperty(lineAnimationX, new PropertyPath(Line.X2Property));

            Storyboard.SetTarget(lineAnimationY, _linePath);
            Storyboard.SetTargetName(lineAnimationY, _linePath.Name);
            Storyboard.SetTargetProperty(lineAnimationY, new PropertyPath(Line.Y2Property));

            #endregion

            // add the line to the Grid(or other controls) and begin animations
            container.Children.Add(_linePath);

            // Add the animations to the storyboard
            storyboard.Children.Add(lineAnimationX);
            storyboard.Children.Add(lineAnimationY);

            // begin storyboard animations
            storyboard.Begin();
            RightChild.DrawBranches(container, duration);
        }

        #endregion
    }
}