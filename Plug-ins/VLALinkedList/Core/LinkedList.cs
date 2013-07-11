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
// //	File Name:		LinkedList.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using VLALinkedList.Design;
using VLAPluginLib;

#endregion

namespace VLALinkedList.Core
{
    public class LinkedList
    {
        #region Variables

        private readonly Canvas _canvas;
        private readonly int _currentNodeYaxis;
        private readonly Duration _duration;
        private readonly LabelNodeTail _labelNodeTail = new LabelNodeTail();
        private readonly int _length;
        public int Count;
        private int _currentNodeXaxis;
        public event SetText SetText;

        #endregion

        public LinkedList(int xAxis, int yAxis, Canvas canvas, Duration duration)
        {
            canvas.Children.Clear();
            _duration = duration;
            Count = 0;
            _canvas = canvas;
            _length = 161;
            _currentNodeXaxis = xAxis;
            _currentNodeYaxis = yAxis;
            Head = new Node(null, null, null);
            Tail = new Node(null, null, null);
        }

        #region Properties

        private Node Head { get; set; }

        private Node Tail { get; set; }

        public double DrawWidth
        {
            get { return _currentNodeXaxis; }
        }

        #endregion

        #region Methods

        public void Add(string value)
        {
            Node temp = Head;
            while (temp != null)
            {
                if (temp.Value == value)
                    throw new Exception("No duplicate values allowed.");
                temp = temp.Next;
            }

            SetText("======================>");
            if (Head.Value == null)
            {
                SetText("Head is Null");
                Head.Value = value;
                Tail = Head;
                SetText(value + " is now head and tail.");
                SetText(value + " next is: null.");
                SetText(value + " previous is: null.");
            }
            else
            {
                temp = new Node(value, Tail, null);
                SetText(value + " is now tail.");
                SetText(value + " next is: null");
                Tail = temp;
                SetText(value + " previous is: " + Tail.Previous.Value);
            }
            Position();
            Draw();
            Count++;
        }

        private void Position()
        {
            Tail.XAxis = _currentNodeXaxis;
            Tail.YAxis = _currentNodeYaxis;
            _currentNodeXaxis += _length;
        }

        private void Draw()
        {
            ListNode listNode = new ListNode
                {
                    LocationTextBlock = {Text = Tail.Value,},
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                };
            Canvas.SetLeft(listNode, Tail.XAxis); // Set x position
            Canvas.SetTop(listNode, 0); // Set y position
            _canvas.Children.Add(listNode);

            double from = Canvas.GetTop(listNode);
            //Set renderer
            TranslateTransform translateTrasform = new TranslateTransform();
            listNode.RenderTransform = translateTrasform;

            //Set up double animation
            DoubleAnimation animationY = new DoubleAnimation(from, Tail.YAxis - from, _duration);
            animationY.Completed += (s, ev) =>
                {
                    if (Tail.Previous == null) //Head
                    {
                        LabelNodeHead labelNodeHead = new LabelNodeHead();
                        Canvas.SetLeft(labelNodeHead, Tail.XAxis*2); // Set x position
                        Canvas.SetTop(labelNodeHead, 0); // Set y position
                        _canvas.Children.Add(labelNodeHead);

                        Canvas.SetLeft(_labelNodeTail, Tail.XAxis*2); // Set x position
                        Canvas.SetTop(_labelNodeTail, Tail.YAxis + listNode.ActualHeight);
                        // Set y position
                        _labelNodeTail.Uid = "LabelNodeTail";
                        UseCanvas(_canvas, "LabelNodeTail");
                        _canvas.Children.Add(_labelNodeTail);
                    }
                    else
                    {
                        Canvas.SetLeft(listNode, Tail.XAxis); // Set x position
                        Canvas.SetTop(listNode, Tail.YAxis); // Set y position

                        Console.WriteLine("Tail X:" + Tail.XAxis);
                        Console.WriteLine("Left:" + Canvas.GetLeft(listNode));

                        from = Tail.XAxis - _currentNodeXaxis + _length;
                        double to = Tail.XAxis - _currentNodeXaxis + _length - 17;
                        Console.WriteLine("From Y2:" + from);
                        Console.WriteLine("To Y2:" + to);

                        TranslateTransform translateTrasformb = new TranslateTransform();
                        listNode.RenderTransform = translateTrasformb;
                        DoubleAnimation animationX = new DoubleAnimation(from, to,
                                                                         TimeSpan.FromSeconds(
                                                                             .05));
                        animationX.Completed += (n, eN) =>
                            {
                                // Create a diagonal linear gradient with four stops.   
                                LinearGradientBrush
                                    myLinearGradientBrush =
                                        new LinearGradientBrush();
                                myLinearGradientBrush.GradientStops.Add(
                                    new GradientStop(Colors.White, 0));
                                myLinearGradientBrush.GradientStops.Add(
                                    new GradientStop(Colors.Black, 1));
                                myLinearGradientBrush.GradientStops.Add(
                                    new GradientStop(Colors.Green, 0.906));
                                listNode.InnerEllipse.Fill =
                                    myLinearGradientBrush;

                                Canvas.SetLeft(_labelNodeTail,
                                               Tail.XAxis);
                                // Set x position
                                Canvas.SetTop(_labelNodeTail,
                                              Tail.YAxis +
                                              listNode.ActualHeight);
                                // Set y position
                                UseCanvas(_canvas, "LabelNodeTail");
                                _canvas.Children.Add(_labelNodeTail);
                            };
                        //Translate
                        translateTrasformb.BeginAnimation(TranslateTransform.XProperty,
                                                          animationX);
                        _currentNodeXaxis -= 17;
                    }
                };
            //Translate
            translateTrasform.BeginAnimation(TranslateTransform.YProperty, animationY);
        }

        #endregion

        private void UseCanvas(Canvas canvas, string uid)
        {
            try
            {
                if (canvas.Dispatcher.CheckAccess())
                {
                    for (int index = 0; index < canvas.Children.Count; index++)
                    {
                        UIElement ui = canvas.Children[index];
                        if (ui.Uid != uid) continue;
                        canvas.Children.Remove(ui);
                    }
                }
                else
                    canvas.Dispatcher.Invoke(DispatcherPriority.Normal, new UseCanvasDelegate(UseCanvas),
                                             canvas, uid);
            }
            catch (Exception e)
            {
                EntryPoint.HostLogger(e.Message, LogType.Error);
                MessageBox.Show(e.Message, "VLA Linked List", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}