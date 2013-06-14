#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VLAShortestPath.Designs;

#endregion

namespace VLAShortestPath.Core_Logic
{
    public class Node
    {
        public Node(int value, Node parent, Node leftChild, Node rightChild)
        {
            Value = value;
            ParentNode = parent;
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        #region Drawing Animations

        private const int Radius = 30;
        private const int DistanceBetweenEachChildNode = 20;
        private const int HightBetweenParentAndChildNode = 20;
        private int _currentNodeXaxis;
        private int _currentNodeYaxis;

        // Find the position where we should draw the center of this node.
        public void PositionNode(ref int xAxis, int yAxis)
        {
            _currentNodeYaxis = yAxis + Radius/2;

            if (LeftChild != null) // Position our left subtree.
                LeftChild.PositionNode(ref xAxis, yAxis + Radius + HightBetweenParentAndChildNode);

            if (LeftChild != null && RightChild != null)
                xAxis += DistanceBetweenEachChildNode; //if we have both children set space between them

            if (RightChild != null) // Position our right subtree.
                RightChild.PositionNode(ref xAxis, yAxis + Radius + HightBetweenParentAndChildNode);

            // Position ourself.
            if (LeftChild != null)
            {
                if (RightChild != null)
                    //If we have both children center the node
                    _currentNodeXaxis = (LeftChild._currentNodeXaxis + RightChild._currentNodeXaxis)/2;
                else
                    //only left child - center it
                    _currentNodeXaxis = LeftChild._currentNodeXaxis + 10;
            }
            else if (RightChild != null)
            {
                // No left child. Center over right child.
                _currentNodeXaxis = RightChild._currentNodeXaxis - 10;
            }
            else
            {
                // No children. We're on our own.
                _currentNodeXaxis = xAxis + Radius/2;
                xAxis += Radius;
            }
        }

        public void DrawNode(Grid grid, int thickness)
        {
            NodeDesign node = new NodeDesign();
            node.Width = Radius;
            node.Height = Radius;
            node.NodeText.Content = Value;
            node.NodeText.FontSize = 10;
            node.NodeText.Foreground = new SolidColorBrush(Colors.Red);
            node.NodeCircle.StrokeThickness = thickness;
            node.NodeCircle.Stroke = new SolidColorBrush(Colors.Red);

            node.VerticalAlignment = VerticalAlignment.Top;
            node.HorizontalAlignment = HorizontalAlignment.Left;
            node.Margin = new Thickness(_currentNodeXaxis, _currentNodeYaxis, 0, 0);

            Canvas.SetLeft(node, _currentNodeXaxis); // Set x position
            Canvas.SetTop(node, _currentNodeYaxis); // Set y position

            grid.Children.Add(node);

            // Draw our children.
            if (LeftChild != null) LeftChild.DrawNode(grid, thickness);
            if (RightChild != null) RightChild.DrawNode(grid, thickness);
        }

        public void DrawBranches(Grid grid, int thickness, TimeSpan duration)
        {
            Line linePath;

            if (LeftChild != null)
            {
                linePath = new Line // Create a new Line
                    {
                        X1 = _currentNodeXaxis + (Radius/2), // x position
                        Y1 = _currentNodeYaxis + Radius, // y position
                        X2 = LeftChild._currentNodeXaxis + (Radius/2), // destination x value
                        Y2 = LeftChild._currentNodeYaxis, // destination y value
                        Stroke = new SolidColorBrush(Colors.Black), // Stroke colour
                        StrokeThickness = thickness // Stroke thickness (0 = invisible)
                    };
                // add the line to the Grid(or other controls) and begin animations
                grid.Children.Add(linePath);

                // begin storyboard animations
                LeftChild.DrawBranches(grid, thickness, duration);
            }

            if (RightChild == null) return;
            linePath = new Line // Create a new Line
                {
                    X1 = _currentNodeXaxis + (Radius/2), // x position
                    Y1 = _currentNodeYaxis + Radius, // y position
                    X2 = RightChild._currentNodeXaxis + (Radius/2), // destination x value
                    Y2 = RightChild._currentNodeYaxis, // destination y value
                    Stroke = new SolidColorBrush(Colors.Black), // Stroke colour
                    StrokeThickness = thickness // Stroke thickness (0 = invisible)
                };
            // add the line to the Grid(or other controls) and begin animations
            grid.Children.Add(linePath);
            RightChild.DrawBranches(grid, thickness, duration);
        }

        #endregion

        public int Value { get; set; }
        public Node ParentNode { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
    }
}