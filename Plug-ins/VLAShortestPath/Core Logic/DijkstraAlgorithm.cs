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
// //	File Name:		DijkstraAlgorithm.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VLAShortestPath.Structures;

#endregion

namespace VLAShortestPath.Core_Logic
{
    /// <summary>
    /// </summary>
    public class DijkstraAlgorithm
    {
        public List<SaveConnections> SaveData;
        public uint ShortestWeight;

        #region Properties

        /// <summary>
        ///     Contains the list of Vertices in the Algorithm
        /// </summary>
        public List<Vertex> Vertices { set; get; }

        /// <summary>
        ///     Returns the current amount of vertices in the Algorithm
        /// </summary>
        public int Count { set; get; }

        #endregion

        /// <summary>Dijkstra's Algorithm Constructor</summary>
        /// <param name="amountOfNode">The maximum amout of vertices allowed</param>
        public DijkstraAlgorithm(uint amountOfNode)
        {
            SaveData = new List<SaveConnections>();
            Vertices = new List<Vertex>((int) amountOfNode);
            Count = 0;
        }

        /// <summary>
        ///     The insert method for the algorithm
        /// </summary>
        /// <param name="canvas">The canvas we want to draw on.</param>
        /// <param name="value">The content string value of the node.</param>
        /// <param name="x">The x-axis coordintate</param>
        /// <param name="y"></param>
        public void Insert(Canvas canvas, string value, double x, double y)
        {
            if (Count >= Vertices.Capacity) return; //return if over capacity
            if (SearchDublicate(value)) return; //return if we have duplicate
            Vertex vertex = new Vertex //initialise the new vertex
                {
                    Value = value,
                    CurrentNodeXaxis = x,
                    CurrentNodeYaxis = y
                };
            Vertices.Add(vertex); //add it to the list of vertices
            Count++; //increase the current count
            vertex.DrawVertex(canvas, null); //draw the vertex into the vertex at it's coordinate
        }

        /// <summary>Connect one vertex to another</summary>
        /// <param name="canvas">The canvas to draw the connection</param>
        /// <param name="value">The first vertex</param>
        /// <param name="value2">The second vertex</param>
        /// <param name="weight">The weight to travel between vertices</param>
        public void Connect(Canvas canvas, string value, string value2, uint weight)
        {
            Vertex vertex1 = FindVertex(value); //Find and return the vertex base on value which is unique
            Vertex vertex2 = FindVertex(value2);

            SaveConnections sc = new SaveConnections();
            sc.From = vertex1;
            sc.To = vertex2;
            sc.Weight = weight;
            SaveData.Add(sc);

            vertex1.ConnectEdges(vertex2, weight); //Connect the edges together
            vertex2.ConnectEdges(vertex1, weight);
            vertex1.DrawConnection(canvas, vertex2, weight, new SolidColorBrush(Colors.Black),
                                   new SolidColorBrush(Colors.White), null); //Draw the connection line
        }

        /// <summary>Connect one vertex to another</summary>
        /// <param name="canvas">The canvas to draw the connection</param>
        /// <param name="vertex1">The first vertex</param>
        /// <param name="vertex2">The second vertex</param>
        /// <param name="weight">The weight to travel between vertices</param>
        public void Connect(Canvas canvas, Vertex vertex1, Vertex vertex2, uint weight)
        {
            vertex1.ConnectEdges(vertex2, weight); //Connect the edges together
            vertex2.ConnectEdges(vertex1, weight);
            vertex1.DrawConnection(canvas, vertex2, weight, new SolidColorBrush(Colors.Black),
                                   new SolidColorBrush(Colors.White), null); //Draw the connection line
        }

        /// <summary>
        ///     Find the shortest path from one vertex to another
        /// </summary>
        /// <param name="canvas">The canvas in which to draw the shortest path</param>
        /// <param name="value">The start vertex</param>
        /// <param name="value2">The end vertex</param>
        public void FindShortestPath(Canvas canvas, string value, string value2)
        {
            Vertex vertex1 = FindVertex(value); //Find and return the vertex base on value which is unique
            Vertex vertex2 = FindVertex(value2);

            List<Vertex> path = new List<Vertex>(); //Contains the list of all possible paths from vertex 1 to 2
            AllPossiblePaths(vertex1, vertex2, ref path, new List<Vertex>());
            ShortestPath shortestPath = SelectBestPath(path, vertex2);
            //Get the shortest path from all the possible paths

            Color colorOne = (Color) ColorConverter.ConvertFromString("#FF084383");
            Color colorTwo = (Color) ColorConverter.ConvertFromString("#F3C6DAC5");

            RadialGradientBrush myRGB = new RadialGradientBrush();
            myRGB.RadiusX = 0.474;
            myRGB.RadiusY = 0.569;
            myRGB.GradientOrigin = new Point(0.258, 0.126);
            myRGB.Center = new Point(0.258, 0.126);
            myRGB.GradientStops.Add(new GradientStop(colorOne, 1));
            myRGB.GradientStops.Add(new GradientStop(colorTwo, 0.167));

            ShortestWeight = 0;
            for (int i = 0; i < shortestPath.Paths.Count; i++)
            {
                foreach (Edge edge in shortestPath.Paths[i].Edges)
                {
                    int next = i + 1;
                    if (next >= shortestPath.Paths.Count) break;
                    //if current edge destination vertex is the same as the next shortest path vertex
                    if (edge.Destination == shortestPath.Paths[next])
                    {
                        ShortestWeight += edge.Weight;
                        //Draw path line
                        shortestPath.Paths[i].DrawConnection(canvas, edge.Destination, edge.Weight,
                                                             new SolidColorBrush(Colors.Blue),
                                                             new SolidColorBrush(Colors.Blue), myRGB);
                        break;
                    }
                }
                //Draw vertex
                shortestPath.Paths[i].DrawVertex(canvas, myRGB);
            }
        }


        public void ReDraw(Canvas canvas)
        {
            canvas.Children.Clear();
            Count = Vertices.Capacity;
            foreach (SaveConnections saveConnections in SaveData)
            {
                Connect(canvas, saveConnections.From, saveConnections.To, saveConnections.Weight);
            }
        }

        public void ReDrawGame(Canvas canvas)
        {
            canvas.Children.Clear();
            Count = Vertices.Capacity;
            foreach (SaveConnections saveConnections in SaveData)
            {
                Connect(canvas, saveConnections.From, saveConnections.To, saveConnections.Weight);
            }
        }

        #region private functions

        private ShortestPath SelectBestPath(List<Vertex> allPossiblePaths, Vertex destinationVertex)
        {
            List<ShortestPath> finalOutput = new List<ShortestPath>();

            int end = 0;
            int start = 0;
            while (start < allPossiblePaths.Count) //While we not at the end of the list
            {
                uint weight = 0;
                List<Vertex> shortestPath = new List<Vertex>();
                while (allPossiblePaths[end] != destinationVertex) //While we haven't reached the destinated vertex
                {
                    shortestPath.Add(allPossiblePaths[end]); //Add the current vertex to the path
                    foreach (Edge edge in allPossiblePaths[end].Edges)
                    {
                        int index = end + 1;
                        //if the current edge is part of the shortestpath add the weight to the total weight
                        if (edge.Destination == allPossiblePaths[index])
                            weight += edge.Weight;
                    }
                    end++;
                }
                shortestPath.Add(allPossiblePaths[end]); //Add the destination vertex to the path
                end++;

                ShortestPath temp = new ShortestPath {Paths = shortestPath, TotalWeight = weight};
                finalOutput.Add(temp);
                start = end;
            }
            finalOutput.Sort(Compare);
            return finalOutput[0]; //return the smallest weight
        }

        private int Compare(ShortestPath x, ShortestPath y)
        {
            return x.TotalWeight.CompareTo(y.TotalWeight);
        }

        private void AllPossiblePaths(Vertex currentVertex, Vertex destinationVertex, ref List<Vertex> path,
                                      List<Vertex> searchedList)
        {
            if (searchedList.Contains(currentVertex)) return; //Return if we have already searched the current vertex
            if (currentVertex == destinationVertex) //if we reach the destination vertex
            {
                foreach (Vertex vertex in searchedList)
                {
                    //Add all the vertices we passed along the way to the path
                    path.Add(vertex);
                }
                path.Add(currentVertex); //Add the destinated vertex
            }
            else
            {
                List<Vertex> newList = new List<Vertex>(); //Create a new list to keep track of the parent vertex
                foreach (Vertex vertex in searchedList)
                {
                    //Add all the vertices we passed along the way to the path
                    newList.Add(vertex);
                }
                newList.Add(currentVertex); //Flag the vertex as searched
                foreach (Edge edge in currentVertex.Edges)
                {
                    Vertex vertex = edge.Destination;
                    AllPossiblePaths(vertex, destinationVertex, ref path, newList);
                }
                newList.Clear();
            }
        }

        private bool SearchDublicate(string value)
        {
            foreach (Vertex item in Vertices)
            {
                if (item.Value == value) return true;
            }
            return false;
        }

        private Vertex FindVertex(string value)
        {
            foreach (Vertex item in Vertices)
            {
                if (item.Value == value) return item;
            }
            return null;
        }

        #endregion
    }
}