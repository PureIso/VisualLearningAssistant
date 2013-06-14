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
// //	File Name:		Vertex.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using VLAShortestPath.Designs;
using VLAShortestPath.Structures;

#endregion

namespace VLAShortestPath.Core_Logic
{
    /// <summary>
    ///     Vertex class
    /// </summary>
    [Serializable]
    public class Vertex
    {
        #region Main Vertex

        /// <summary>Nonnegative edge path connecting one vertex to another</summary>
        public List<Edge> Edges = new List<Edge>();

        /// <summary>The value integer the node/vertex</summary>
        public string Value { get; set; }

        /// <summary>Connect to another vertex</summary>
        /// <param name="destination">The vertex you are connecting to</param>
        /// <param name="weight">The weight or the cost to connect</param>
        public void ConnectEdges(Vertex destination, uint weight)
        {
            Edge newConnection = new Edge
                {
                    Destination = destination,
                    Weight = weight
                };
            Edges.Add(newConnection); //Add new connection to the list of edges your connected to
        }

        #endregion

        #region Design

        private const double Radius = 25;

        /// <summary>X axis for drawing</summary>
        public double CurrentNodeXaxis;

        /// <summary>Y axis for drawing</summary>
        public double CurrentNodeYaxis;

        /// <summary>
        ///     Draw vertex
        /// </summary>
        /// <param name="canvas">The canvas to draw on</param>
        /// <param name="rgb">The Radical Gradient Brush</param>
        public void DrawVertex(Canvas canvas, RadialGradientBrush rgb)
        {
            VertexDesign node = new VertexDesign();
            node.vertexText.Content = Value;
            if (rgb != null)
                node.vertexCircle.Fill = rgb;
            node.VerticalAlignment = VerticalAlignment.Top;
            node.HorizontalAlignment = HorizontalAlignment.Left;
            node.Margin = new Thickness(CurrentNodeXaxis, CurrentNodeYaxis, 0, 0);
            node.Uid = "VertexNode-" + node.vertexText;

            //remove current layer from the canvas layer
            RemoveUiElement clean = IfLayerPresent(canvas, node);
            if (clean.UiElementPresent) canvas.Children.Remove(clean.UiElement);
            canvas.Children.Add(node);
        }

        private static RemoveUiElement IfLayerPresent(Panel panel, UIElement uid)
        {
            RemoveUiElement newObject = new RemoveUiElement();
            //get all the layers on the panel
            foreach (UIElement ui in panel.Children)
            {
                if (ui.Uid != uid.Uid) continue; //if this is not the UID we want go to the next UID
                newObject.UiElement = ui; //Set the UI element
                newObject.UiElementPresent = true; //Set the UI Element as present
                return newObject;
            }
            newObject.UiElementPresent = false;
            return newObject;
        }

        public double CalculateAngle(double px1, double py1, double px2, double py2)
        {
            // Negate X and Y values
            double pxRes = px2 - px1;

            double pyRes = py2 - py1;
            double angle = 0.0;

            // Calculate the angle
            if (pxRes == 0.0)
            {
                if (pxRes == 0.0)

                    angle = 0.0;
                else if (pyRes > 0.0) angle = Math.PI/2.0;

                else
                    angle = Math.PI*3.0/2.0;
            }
            else if (pyRes == 0.0)
            {
                if (pxRes > 0.0)

                    angle = 0.0;

                else
                    angle = Math.PI;
            }

            else
            {
                if (pxRes < 0.0)

                    angle = Math.Atan(pyRes/pxRes) + Math.PI;
                else if (pyRes < 0.0) angle = Math.Atan(pyRes/pxRes) + (2*Math.PI);

                else
                    angle = Math.Atan(pyRes/pxRes);
            }

            // Convert to degrees
            angle = angle*180/Math.PI;
            return angle - 90;
        }

        private static double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2));
        }

        /// <summary>
        ///     Draw vertex connections
        /// </summary>
        /// <param name="canvas">The canvas to draw on</param>
        /// <param name="destinationVertex">The destination vertex</param>
        /// <param name="weight">The weight between current vertex and destination vertex</param>
        /// <param name="lineColour">The colour of the line</param>
        /// <param name="lineTextColour">The colour of the line text </param>
        /// <param name="rgb">The Radical Gradient Brush</param>
        public void DrawConnection(Canvas canvas, Vertex destinationVertex, uint weight, Brush lineColour,
                                   Brush lineTextColour, RadialGradientBrush rgb)
        {
            VertexArrow arrow = new VertexArrow();
            double angle = CalculateAngle(CurrentNodeXaxis + Radius,
                                          CurrentNodeYaxis + Radius,
                                          destinationVertex.CurrentNodeXaxis + Radius,
                                          destinationVertex.CurrentNodeYaxis + Radius);

            double distance = CalculateDistance(CurrentNodeXaxis + Radius*2,
                                                CurrentNodeYaxis + Radius*2,
                                                destinationVertex.CurrentNodeXaxis + Radius*2,
                                                destinationVertex.CurrentNodeYaxis + Radius*2);
            if (rgb != null)
                arrow.ArrowRectangle.Fill = Brushes.Blue;

            //Point to destination
            arrow.Margin = new Thickness(CurrentNodeXaxis + Radius,
                                         CurrentNodeYaxis + Radius, 0, 0);
            RotateTransform transform = new RotateTransform(angle);
            arrow.RenderTransform = transform;

            // if you use the same animation for X & Y you don't need anim1, anim2 
            DoubleAnimation doubleAnimation = new DoubleAnimation(0, distance,
                                                                  new Duration(TimeSpan.FromMilliseconds(500)));
            arrow.BeginAnimation(FrameworkElement.HeightProperty, doubleAnimation);

            arrow.Uid = "Arrow-" + Value + "-" + destinationVertex.Value;

            //remove current layer from the canvas layer
            RemoveUiElement clean = IfLayerPresent(canvas, arrow);
            if (clean.UiElementPresent) canvas.Children.Remove(clean.UiElement);
            arrow.Uid = "Arrow-" + destinationVertex.Value + "-" + Value;
            //remove current layer from the canvas layer
            clean = IfLayerPresent(canvas, arrow);
            if (clean.UiElementPresent) canvas.Children.Remove(clean.UiElement);
            canvas.Children.Add(arrow);

            //Draw on top of the lines
            DrawVertex(canvas, rgb);
            destinationVertex.DrawVertex(canvas, rgb);

            //Mid Point formula
            double cordx = (CurrentNodeXaxis + Radius + destinationVertex.CurrentNodeXaxis + Radius)/2;
            double cordy = (CurrentNodeYaxis + Radius + destinationVertex.CurrentNodeYaxis + Radius)/2;

            WeightDesign vertexWeight = new WeightDesign();
            vertexWeight.WeightValue.Text = weight.ToString();
            vertexWeight.Margin = new Thickness(cordx, cordy, 0, 0);
            vertexWeight.Uid = "WeightDesign-" + cordx + cordy;

            //remove current layer from the canvas layer
            clean = IfLayerPresent(canvas, vertexWeight);
            if (clean.UiElementPresent) canvas.Children.Remove(clean.UiElement);
            canvas.Children.Add(vertexWeight);
        }

        #endregion
    }
}