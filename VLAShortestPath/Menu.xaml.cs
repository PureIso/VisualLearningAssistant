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
// //	File Name:		Menu.xaml.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using VLAPluginLib;
using VLAShortestPath.Core_Logic;
using VLAShortestPath.Structures;

#endregion

namespace VLAShortestPath
{
    /// <summary>
    ///     Interaction logic for SaveMenu.xaml
    /// </summary>
    public partial class SaveMenu
    {
        private const string FilePath = "SaveData\\dijkstraAlgorithm.sav";
        private readonly Hashtable _fileHashTable;
        private readonly DijkstraAlgorithmDisplay _menu;

        public SaveMenu(DijkstraAlgorithmDisplay menu)
        {
            InitializeComponent();
            //initialise variables
            _menu = menu;

            //Check if a save data folder exist or not
            Functions.CheckAndCreateDirectory("SaveData");
            //Check if the save file exist or not
            bool existsAlready = Functions.CheckAndCreateFile(FilePath);
            //We already have a dijkstraAlgo.sav file
            if (existsAlready)
            {
                FileStream filestream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FilePath, FileMode.Open);
                StreamReader readMap = new StreamReader(filestream);
                BinaryFormatter binaryFormatter = new BinaryFormatter
                    {
                        AssemblyFormat = FormatterAssemblyStyle.Full
                    };
                readMap.BaseStream.Position = 0;
                _fileHashTable = (Hashtable) binaryFormatter.Deserialize(readMap.BaseStream);
                filestream.Close();
            }
            else
            {
                //Create a new DijkstraAlgorithm empty
                DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(0)
                    {
                        SaveData = new List<SaveConnections>()
                    };
                //We create a new hastable and load it into the file
                _fileHashTable = new Hashtable
                    {
                        {"Save1", dijkstraAlgorithm.SaveData},
                        {"Save2", dijkstraAlgorithm.SaveData},
                        {"Save3", dijkstraAlgorithm.SaveData},
                        {"Save4", dijkstraAlgorithm.SaveData},
                        {"Save5", dijkstraAlgorithm.SaveData},
                        {"Save6", dijkstraAlgorithm.SaveData},
                        {"Save7", dijkstraAlgorithm.SaveData},
                        {"Save8", dijkstraAlgorithm.SaveData},
                        {"Save9", dijkstraAlgorithm.SaveData},
                        {"Save10", dijkstraAlgorithm.SaveData}
                    };
                BinaryFormatter binaryformatter = new BinaryFormatter();
                FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FilePath, FileMode.OpenOrCreate);
                StreamWriter streamwriter = new StreamWriter(file);
                binaryformatter.Serialize(streamwriter.BaseStream, _fileHashTable);
                file.Close();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (saveComboBox.SelectedIndex == -1) return;
            //create a temporary hashtable
            Hashtable hashtable = new Hashtable();
            //go through all the keys from our save file and add them to the temporary hash
            for (int index = 1; index <= _fileHashTable.Count; index++)
            {
                //if the keys are the same simple add the current saveData to it
                if (saveComboBox.Text == "Save" + index)
                {
                    if (_menu.DijkstraAlgorithm == null) return;
                    hashtable.Add(saveComboBox.Text, _menu.DijkstraAlgorithm.SaveData);
                }
                else
                {
                    //if the keys are not add what is from the file
                    hashtable.Add("Save" + index, _fileHashTable["Save" + index]);
                }
            }
            BinaryFormatter binaryformatter = new BinaryFormatter();
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FilePath, FileMode.OpenOrCreate);
            StreamWriter streamwriter = new StreamWriter(file);
            binaryformatter.Serialize(streamwriter.BaseStream, hashtable);
            file.Close();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            if (saveComboBox.SelectedIndex == -1) return;

            _menu.DijkstraAlgorithm = new DijkstraAlgorithm(0);
            _menu.DijkstraAlgorithm.SaveData = new List<SaveConnections>();
            _menu.DijkstraAlgorithm.SaveData = (List<SaveConnections>) _fileHashTable[saveComboBox.Text];

            List<string> names = new List<string>();
            foreach (SaveConnections connections in _menu.DijkstraAlgorithm.SaveData)
            {
                if (!names.Contains(connections.From.Value))
                {
                    names.Add(connections.From.Value);
                    _menu.DijkstraAlgorithm.Vertices.Add(connections.From);
                }
                if (!names.Contains(connections.To.Value))
                {
                    names.Add(connections.To.Value);
                    _menu.DijkstraAlgorithm.Vertices.Add(connections.To);
                }
            }
            _menu.fromVertexComboBox.Items.Clear();
            _menu.toVertexComboBox.Items.Clear();
            _menu.fromVertexSPComboBox.Items.Clear();
            _menu.toVertexSPComboBox.Items.Clear();

            foreach (string name in names)
            {
                _menu.fromVertexComboBox.Items.Add(name);
                _menu.toVertexComboBox.Items.Add(name);
                _menu.fromVertexSPComboBox.Items.Add(name);
                _menu.toVertexSPComboBox.Items.Add(name);
            }
            _menu.DijkstraAlgorithm.ReDraw(_menu.mainCanvas);
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}