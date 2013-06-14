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
// //	File Name:		Game.xaml.cs
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
using System.Media;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;
using System.Windows.Resources;
using System.Windows.Threading;
using VLAControlLib;
using VLAPluginLib;
using VLAShortestPath.Core_Logic;
using VLAShortestPath.Structures;

#endregion

namespace VLAShortestPath
{
    /// <summary>
    ///     Interaction logic for Game.xaml
    /// </summary>
    public partial class Game
    {
        private const string FilePath = "Plug-in\\DA-Data\\game.vla";
        private readonly DispatcherTimer _countdownTimer;
        private readonly SoundPlayer _splayer;
        private bool _back;
        private string _currentLevel = "Level1";
        private DijkstraAlgorithm _dijkstraAlgorithm;
        private Hashtable _fileHashTable;
        private uint _highestScore;
        private int _time;

        public Game()
        {
            try
            {
                InitializeComponent();
                _back = false;
                _countdownTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 1)};
                _countdownTimer.Tick += CountdownTimerStep;
                LoadFile();

                Uri uri = new Uri("/vlashortestpath;component/DA-Data/bgMusic.wav", UriKind.Relative);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                if (info == null)
                {
                    throw new Exception("Internal Error - Music is missing!");
                }
                _splayer = new SoundPlayer(info.Stream);
                _splayer.PlayLooping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VLA - Shortest Path", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Event Handlers

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            _dijkstraAlgorithm = new DijkstraAlgorithm(0);
            _dijkstraAlgorithm.SaveData = new List<SaveConnections>();
            _dijkstraAlgorithm.SaveData = (List<SaveConnections>) _fileHashTable["Level1"];
            //Get current level
            CurrentLevelTextBox.Text = "Level1";
            LevelLoader();

            enterButton.IsEnabled = true;
            _time = 10;
            _countdownTimer.Start();
        }

        private void EnterButtonClick(object sender, RoutedEventArgs e)
        {
            _dijkstraAlgorithm.FindShortestPath(GameCanvas, ShortestPathFromTextBox.Text, ShortestPathToTextBox.Text);
            _countdownTimer.Stop();
            if (_dijkstraAlgorithm.ShortestWeight == uint.Parse(answerTextBox.Text))
            {
                MessageBox.Show("Correct!", "VLA - Dijkstra Game", MessageBoxButton.OK, MessageBoxImage.Information);
                scoreTextBox.Text = (uint.Parse(scoreTextBox.Text) + 2*_time).ToString();
                CurrentLevelTextBox.Text = _currentLevel;
                SaveData();
                LoadLevel();
                _countdownTimer.Start();
                answerTextBox.Clear();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Fail!" + Environment.NewLine + "Try Again?",
                                                          "VLA - Dijkstra Game", MessageBoxButton.YesNo,
                                                          MessageBoxImage.Information);
                Clear();
                if (result == MessageBoxResult.Yes)
                {
                    //Loads the game file
                    LoadFile();
                    //Get the current level
                    _currentLevel = (string) _fileHashTable["CurrentLevel"];
                    //Get the current Score
                    scoreTextBox.Text = ((uint) _fileHashTable["Score"]).ToString();
                    //Get the timer
                    timerTextBox.Text = ((uint) _fileHashTable["Timer"]).ToString();
                    //Get current level
                    CurrentLevelTextBox.Text = _currentLevel;
                    if (scoreTextBox.Text == "")
                        scoreTextBox.Text = 0.ToString();
                    LoadLevel();
                    enterButton.IsEnabled = true;
                }
            }
        }

        private void CountdownTimerStep(object sender, EventArgs e)
        {
            try
            {
                if (_time > 0)
                {
                    _time--;
                    timerTextBox.Text = _time.ToString();
                }
                else
                {
                    _countdownTimer.Stop();
                    if (_back == false)
                        MessageBox.Show("Game Over!", "VLA - Shortest Path", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    if (uint.Parse(scoreTextBox.Text) > _highestScore)
                    {
                        MessageBox.Show("Congratulations!" + Environment.NewLine +
                                        "New Highest Score!", "VLA - Shortest Path", MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                        HighestScoreTextBlock.Text = scoreTextBox.Text;
                        _highestScore = uint.Parse(scoreTextBox.Text);
                    }

                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VLA - Shortest Path", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void continueGameButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            //Loads the game file
            LoadFile();
            //Get the current level
            _currentLevel = (string) _fileHashTable["CurrentLevel"];
            //Get the current Score
            scoreTextBox.Text = ((uint) _fileHashTable["Score"]).ToString();
            //Get the timer
            timerTextBox.Text = ((uint) _fileHashTable["Timer"]).ToString();
            //Get current level
            CurrentLevelTextBox.Text = _currentLevel;
            if (scoreTextBox.Text == "")
                scoreTextBox.Text = 0.ToString();
            //Load the level
            LoadLevel();
            enterButton.IsEnabled = true;
        }

        private void answerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterButtonClick(sender, null);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            _back = true;
            _splayer.Stop();
            _time = 0;
            Switcher.Switch(new Main());
        }

        #endregion

        #region Functions

        private void Clear()
        {
            GameCanvas.Children.Clear();
            ShortestPathFromTextBox.Text = "0";
            ShortestPathToTextBox.Text = "0";
            timerTextBox.Text = "0";
            scoreTextBox.Text = "0";
            _time = 0;
            answerTextBox.Clear();
            CurrentLevelTextBox.Text = "Level0";
            enterButton.IsEnabled = false;
        }

        private void LevelLoader()
        {
            try
            {
                //Temporary
                DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm(0);
                dijkstraAlgorithm.SaveData = new List<SaveConnections>();
                _dijkstraAlgorithm.SaveData = (List<SaveConnections>) _fileHashTable[_currentLevel];

                List<string> names = new List<string>();
                foreach (SaveConnections connections in _dijkstraAlgorithm.SaveData)
                {
                    Random rand = new Random((int) DateTime.Now.Ticks);
                    int numIterations = rand.Next(1, 20);

                    if (!names.Contains(connections.From.Value))
                    {
                        names.Add(connections.From.Value);
                        Vertex newVertex = new Vertex
                            {
                                CurrentNodeXaxis = connections.From.CurrentNodeXaxis,
                                CurrentNodeYaxis = connections.From.CurrentNodeYaxis,
                                Value = connections.From.Value,
                            };
                        dijkstraAlgorithm.Vertices.Add(newVertex);
                    }
                    if (!names.Contains(connections.To.Value))
                    {
                        names.Add(connections.To.Value);
                        Vertex newVertex = new Vertex
                            {
                                CurrentNodeXaxis = connections.To.CurrentNodeXaxis,
                                CurrentNodeYaxis = connections.To.CurrentNodeYaxis,
                                Value = connections.To.Value,
                            };
                        dijkstraAlgorithm.Vertices.Add(newVertex);
                    }

                    dijkstraAlgorithm.Connect(GameCanvas, connections.From.Value, connections.To.Value,
                                              (uint) numIterations);
                }
                _dijkstraAlgorithm = dijkstraAlgorithm;
                _dijkstraAlgorithm.ReDrawGame(GameCanvas);

                List<Vertex> local = new List<Vertex>(_dijkstraAlgorithm.Vertices);
                string[] s = Randomize(local);

                ShortestPathFromTextBox.Text = s[0];
                ShortestPathToTextBox.Text = s[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VLA - Shortest Path", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string[] Randomize(IList<Vertex> list)
        {
            string[] s = new string[2];
            Random random = new Random();
            for (int i = 0; i < 2; i++)
            {
                int index = random.Next(list.Count);
                s[i] = list[index].Value;
                list.RemoveAt(index);
            }
            return s;
        }

        private void SaveData()
        {
            //create a temporary hashtable
            Hashtable hashtable = new Hashtable();
            //go through all the keys from our save file and add them to the temporary hash
            for (int index = 1; index <= 14; index++)
            {
                //if the keys are the same simple add the current saveData to it
                switch (index)
                {
                    case 11:
                        {
                            uint value = uint.Parse(_currentLevel.Substring(5)) + 1;
                            if (value > 10) value = 10;
                            string newLevel = "Level" + value;
                            hashtable.Add("CurrentLevel", newLevel);
                            _currentLevel = newLevel;
                        }
                        break;
                    case 12:
                        hashtable.Add("Score", uint.Parse(scoreTextBox.Text));
                        break;
                    case 13:
                        hashtable.Add("Timer", uint.Parse(timerTextBox.Text));
                        break;
                    case 14:
                        hashtable.Add("HighestScore",
                                      uint.Parse(scoreTextBox.Text) > _highestScore
                                          ? uint.Parse(scoreTextBox.Text)
                                          : _highestScore);
                        break;
                    default:
                        hashtable.Add("Level" + index, _fileHashTable["Level" + index]);
                        break;
                }
            }
            BinaryFormatter binaryformatter = new BinaryFormatter();
            FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FilePath,
                                             FileMode.OpenOrCreate);
            StreamWriter streamwriter = new StreamWriter(file);
            binaryformatter.Serialize(streamwriter.BaseStream, hashtable);
            file.Close();
        }

        private void LoadLevel()
        {
            _dijkstraAlgorithm = new DijkstraAlgorithm(0);
            _dijkstraAlgorithm.SaveData = new List<SaveConnections>();
            _dijkstraAlgorithm.SaveData = (List<SaveConnections>) _fileHashTable[_currentLevel];

            LevelLoader();

            _time += 5;
            _countdownTimer.Start();
        }

        private void LoadFile()
        {
            try
            {
                //Check if a save data folder exist or not
                Functions.CheckAndCreateDirectory("Plug-in\\DA-Data");
                //Check if the save file exist or not
                bool existsAlready = Functions.CheckAndCreateFile(FilePath);
                //We already have a dijkstraAlgo.sav file
                if (existsAlready)
                {
                    FileStream filestream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FilePath,
                                                           FileMode.Open);
                    StreamReader readMap = new StreamReader(filestream);
                    BinaryFormatter binaryFormatter = new BinaryFormatter
                        {
                            AssemblyFormat = FormatterAssemblyStyle.Full
                        };
                    readMap.BaseStream.Position = 0;
                    _fileHashTable = (Hashtable) binaryFormatter.Deserialize(readMap.BaseStream);
                    filestream.Close();


                    BinaryFormatter binaryformatter = new BinaryFormatter();
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FilePath,
                                                     FileMode.OpenOrCreate);
                    StreamWriter streamwriter = new StreamWriter(file);
                    binaryformatter.Serialize(streamwriter.BaseStream, _fileHashTable);
                    file.Close();

                    //Get the timer
                    _highestScore = ((uint) _fileHashTable["HighestScore"]);
                    HighestScoreTextBlock.Text = _highestScore.ToString();
                }
                else
                {
                    Uri uri = new Uri("/vlashortestpath;component/DA-Data/game.vla", UriKind.Relative);
                    StreamResourceInfo info = Application.GetResourceStream(uri);
                    if (info == null)
                    {
                        throw new Exception("Internal Error - Game.Vla is missing!");
                    }
                    StreamReader readMap = new StreamReader(info.Stream);
                    BinaryFormatter binaryFormatter = new BinaryFormatter
                        {
                            AssemblyFormat = FormatterAssemblyStyle.Full
                        };
                    readMap.BaseStream.Position = 0;
                    _fileHashTable = (Hashtable) binaryFormatter.Deserialize(readMap.BaseStream);
                    info.Stream.Close();
                    readMap.Close();

                    BinaryFormatter binaryformatter = new BinaryFormatter();
                    FileStream file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FilePath,
                                                     FileMode.OpenOrCreate);
                    StreamWriter streamwriter = new StreamWriter(file);
                    binaryformatter.Serialize(streamwriter.BaseStream, _fileHashTable);
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VLA - Shortest Path", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        private void musicButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string) musicButton.Content == "Pause")
            {
                _splayer.Stop();
                musicButton.Content = "Play";
            }
            else
            {
                _splayer.PlayLooping();
                musicButton.Content = "Pause";
            }
        }
    }
}