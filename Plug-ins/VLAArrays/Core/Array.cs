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
// //	File Name:		Array.cs
// //	Created:		06-04-2013 Time: 00:08
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Windows.Controls;
using VLAPluginLib;

#endregion

namespace VLAArrays.Core
{
    public class Array
    {
        #region Variables

        public int Count;
        private Node _head;
        private Node _current;
        private int _longestIndex;
        private double _initPosition = 100;
        private double _longestWidth;

        #endregion

        public Array()
        {
            Count = 0;
            _current = null;
            _head = _current;
            _longestIndex = 0;
        }

        public event SetText SetText;

        public void Add(string[] values)
        {
            Count = values.Length;
            SetText(Count + " String values added");
            SetText(Count > 1 ? "The is a multi-dimensional array." : "The is a single-dimensional array.");

            _current = new Node(values[0], _initPosition, ref _longestWidth);
            _head = _current;
            int length = values[0].Length;
            int index = 0;
            string longestString = values[0];

            SetText("===================");
            SetText("Arrays of Values");
            SetText("===================");
            foreach (string theString in values)
            {
                SetText(theString);
                if (theString.Length > length)
                {
                    length = theString.Length;
                    _longestIndex = index;
                    longestString = theString;
                }
                index++;

                if (theString == values[0]) continue;
                _initPosition += 50;
                _current.Next = new Node(theString, _initPosition, ref _longestWidth);
                _current = _current.Next;
            }
            Count = values.Length;
            SetText("=====================");
            SetText("Longest string: " + longestString);
            SetText("Longest string at Index: " + _longestIndex);
            SetText("=====================");
        }

        #region Properties

        public double DrawHeight
        {
            get { return _initPosition + 50; }
        }

        public double DrawWidth
        {
            get { return _longestWidth; }
        }

        #endregion

        #region Methods

        public void AnimateMemory(Canvas canvas)
        {
            canvas.Children.Clear();
            _head.AnimateMemory(canvas, 0, _longestIndex, _initPosition);
        }

        public void Animate(Canvas canvas)
        {
            _head.Animate(canvas);
        }

        #endregion
    }
}