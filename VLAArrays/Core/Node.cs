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
// //	File Name:		Node.cs
// //	Created:		06-04-2013 Time: 00:08
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using VLAArrays.Design;

#endregion

namespace VLAArrays.Core
{
    public class Node
    {
        private TimeSpan _duration = TimeSpan.FromSeconds(1);

        public Node(string value, double positionY, ref double longestWidth)
        {
            Longest = false;
            Value = value;
            double myPositionY = positionY;
            double myPositionX = 88;
            _members = new List<char>(value.Length);
            _positionY = new List<double>(value.Length);
            _positionX = new List<double>(value.Length);

            foreach (char member in value)
            {
                _members.Add(member);
                _positionY.Add(myPositionY);
                _positionX.Add(myPositionX);
                myPositionX += 43;
                if (myPositionX > longestWidth)
                    longestWidth = myPositionX;
            }
        }

        #region Properties

        private readonly List<char> _members;
        private readonly List<double> _positionX;
        private readonly List<double> _positionY;

        private string Value { get; set; }

        public Node Next { get; set; }

        public bool Longest { get; set; }

        #endregion

        #region Methods

        public void Animate(Canvas canvas)
        {
            for (int index = 0; index < _members.Count(); index++)
            {
                ArrayNode node = new ArrayNode
                    {
                        TextBlock = {Text = _members[index].ToString()},
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                    };
                Canvas.SetLeft(node, _positionX[index] + 6); // Set x position
                Canvas.SetTop(node, 0); // Set y position
                canvas.Children.Add(node);

                double y1 = Canvas.GetTop(node);

                TranslateTransform translateTrasform = new TranslateTransform();
                node.RenderTransform = translateTrasform;

                DoubleAnimation animationY = new DoubleAnimation(y1, _positionY[index] - y1, _duration);
                animationY.Completed += (s, e) =>
                    {
                        _duration += TimeSpan.FromSeconds(1);
                        if ((index) == _members.Count())
                            if (Next != null)
                                Next.Animate(canvas);
                    };

                //Translate
                translateTrasform.BeginAnimation(TranslateTransform.YProperty, animationY);
            }
        }

        public void AnimateMemory(Canvas canvas, int startIndex, int longestIndex, double initPosition)
        {
            IndexNode node = new IndexNode
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    LocationTextBlock = {Text = startIndex.ToString()},
                };
            Canvas.SetLeft(node, _positionX[0] - 68); // Set x position
            Canvas.SetTop(node, _positionY[0]); // Set y position
            canvas.Children.Add(node);

            for (int index = 0; index < _members.Count(); index++)
            {
                EmptyMemoryNode emptyMemoryNode = new EmptyMemoryNode
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left,
                    };
                Canvas.SetLeft(emptyMemoryNode, _positionX[index] + 5); // Set x position
                Canvas.SetTop(emptyMemoryNode, _positionY[index]); // Set y position
                canvas.Children.Add(emptyMemoryNode);

                if (startIndex == longestIndex)
                {
                    ReverseIndexNode reverseIndexNode = new ReverseIndexNode
                        {
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            LocationTextBlock = {Text = index.ToString()},
                        };
                    Canvas.SetLeft(reverseIndexNode, _positionX[index]); // Set x position
                    Canvas.SetTop(reverseIndexNode, 40); // Set y position
                    canvas.Children.Add(reverseIndexNode);
                }
            }

            startIndex++;
            if (Next != null)
                Next.AnimateMemory(canvas, startIndex, longestIndex, initPosition - 55);
        }

        #endregion
    }
}