
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace VLAHuffman.Logic
{
    public class HuffmanTree
    {
        private readonly Dictionary<char, int> _frequencies = new Dictionary<char, int>();
        private readonly List<HuffmanNode> _nodes = new List<HuffmanNode>();
        private HuffmanNode RootNode { get; set; }

        public void Build(string source)
        {
            foreach (char theChar in source)
            {
                if (!_frequencies.ContainsKey(theChar))
                {
                    _frequencies.Add(theChar, 0);
                }

                _frequencies[theChar]++;
            }

            foreach (KeyValuePair<char, int> symbol in _frequencies)
            {
                _nodes.Add(new HuffmanNode {Symbol = symbol.Key, Frequency = symbol.Value});
            }

            while (_nodes.Count > 1)
            {
                List<HuffmanNode> orderedNodes = _nodes.OrderBy(node => node.Frequency).ToList();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<HuffmanNode> taken = orderedNodes.Take(2).ToList();

                    // Create a parent node by combining the frequencies
                    HuffmanNode parent = new HuffmanNode
                        {
                            Symbol = '*',
                            Frequency = taken[0].Frequency + taken[1].Frequency,
                            LeftChild = taken[0],
                            RightChild = taken[1]
                        };

                    _nodes.Remove(taken[0]);
                    _nodes.Remove(taken[1]);
                    _nodes.Add(parent);
                }

                RootNode = _nodes.FirstOrDefault();
            }
        }

        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            foreach (char t in source)
            {
                List<bool> encodedSymbol = RootNode.Traverse(t, new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            HuffmanNode current = RootNode;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.RightChild != null)
                    {
                        current = current.RightChild;
                    }
                }
                else
                {
                    if (current.LeftChild != null)
                    {
                        current = current.LeftChild;
                    }
                }

                if (!IsLeaf(current)) continue;
                decoded += current.Symbol;
                current = RootNode;
            }

            return decoded;
        }

        private bool IsLeaf(HuffmanNode node)
        {
            return (node.LeftChild == null && node.RightChild == null);
        }

        /// <summary>Draw the binary tree</summary>
        /// <param name="grid"></param>
        /// <param name="xAxis">The x-axis of the coordinate</param>
        /// <param name="yAxis">The y-axis of the coordinate</param>
        /// <param name="thickness"></param>
        public void DrawTree(Canvas grid, int xAxis, int yAxis, int thickness)
        {
            grid.Children.Clear();
            if (RootNode == null) return; //Don't draw anything
            // Position all of the nodes.
            RootNode.PositionNode(ref xAxis, yAxis);
            // Draw all of the links.
            RootNode.DrawBranches(grid, thickness);
            // Draw all of the nodes.
            RootNode.DrawNode(grid, thickness);
        }
    }
}