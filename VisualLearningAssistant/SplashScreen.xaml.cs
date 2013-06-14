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
// //	File Name:		SplashScreen.xaml.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Resources;
using System.Windows.Threading;
using System.Xml.Serialization;
using VLAPluginLib;

#endregion

namespace VisualLearningAssistant
{
    /// <summary>
    ///     Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen
    {
        private readonly MainWindow _main;

        /// <summary>
        ///     Splash Screen Constructor
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
            _main = new MainWindow();
            ThreadPool.QueueUserWorkItem(Load);
            TextBlockVersion.Text += " " + Assembly.GetExecutingAssembly().GetName().Version;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += LoadFromSameFolder;
        }

        #region Function

        private void Load(object a)
        {
            try
            {
                #region legal

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(
                    "Visual Learning Assistant is a learning application to aid by visual representations of knowledge, " +
                    "concepts, thoughts, or ideas.");
                sb.AppendLine("Copyright (C) 2013 ");
                sb.AppendLine("This program is free software: you can redistribute it and/or modify " +
                              "it under the terms of the GNU General Public License as published by " +
                              "the Free Software Foundation, either version 3 of the License, or " +
                              "(at your option) any later version.");
                sb.AppendLine("This program is distributed in the hope that it will be useful, " +
                              "but WITHOUT ANY WARRANTY; without even the implied warranty of " +
                              "MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the " +
                              "GNU General Public License for more details. ");
                sb.AppendLine("You should have received a copy of the GNU General Public License " +
                              "along with this program.  If not, see http://www.gnu.org/licenses/.");
                _main.SetLog(sb.ToString(), LogType.None);

                #endregion

                _main.SetLog("Welcome to Visual Learning Assistant.", LogType.Info);
                _main.SetLog("Version: " + Assembly.GetExecutingAssembly().GetName().Version, LogType.Info);
                _main.SetLog("Loading Language Packs.", LogType.Info);

                try
                {
                    string pathToLanguageFile = AppDomain.CurrentDomain.BaseDirectory +
                                                "Language-Pack\\DictionaryDetails.xml";

                    if (!Functions.CheckAndCreateDirectory("Language-Pack"))
                    {
                        _main.SetLog("Language-Pack Folder Missing!", LogType.Info);
                        _main.SetLog("Language-Pack Folder Created!", LogType.Info);
                    }

                    if (!Functions.CheckAndCreateFile("Language-Pack\\DictionaryDetails.xml"))
                    {
                        _main.SetLog("DictionaryDetails.xml File Missing!", LogType.Info);
                        _main.SetLog("DictionaryDetails.xml File Created!", LogType.Info);

                        Uri uri = new Uri("pack://application:,,,/Language-Pack/DictionaryDetails.xml");
                        StreamResourceInfo info = Application.GetResourceStream(uri);


                        if (info != null)
                        {
                            StreamReader textReader = new StreamReader(info.Stream);
                            FileStream file =
                                new FileStream(
                                    AppDomain.CurrentDomain.BaseDirectory + "Language-Pack/DictionaryDetails.xml",
                                    FileMode.OpenOrCreate);

                            StreamWriter streamWriter = new StreamWriter(file);
                            while (!textReader.EndOfStream)
                            {
                                var s = textReader.ReadLine();
                                streamWriter.WriteLine(s);
                                Console.WriteLine(s);
                            }
                            textReader.Close();
                            streamWriter.Close();
                        }
                        else
                        {
                            _main.SetLog("Internal Error - Game.Vla is missing!", LogType.Error);
                        }
                    }

                    //De-serializing XML
                    List<Languages> languages;
                    XmlSerializer deserializer = new XmlSerializer(typeof (List<Languages>));
                    using (TextReader textReader = new StreamReader(pathToLanguageFile))
                    {
                        languages = (List<Languages>) deserializer.Deserialize(textReader);
                        textReader.Close();
                    }
                    //Add each language
                    foreach (Languages language in languages)
                    {
                        _main.AddLanguage(language);
                        _main.SetLog(language.Name + " Language Added!", LogType.Info);
                    }

                    //Enable the Language Menu Item button
                    MainWindow.IsEnable(_main.MainMenuLanguage, true);
                    _main.SetLog(languages.Count + " Language/s Loaded.", LogType.Info);
                }
                catch (Exception ex)
                {
                    _main.SetLog(ex.Message + " (Language Pack)", LogType.Error);
                }


                try
                {
                    _main.SetLog("Loading Plug-ins", LogType.Info);
                    string pathToPlugins = AppDomain.CurrentDomain.BaseDirectory + "Plug-in";
                    if (!Functions.CheckAndCreateDirectory("Plug-in"))
                    {
                        _main.SetLog("Plug-in Folder Missing!", LogType.Info);
                        _main.SetLog("Plug-in Folder Created!", LogType.Info);
                    }

                    //Load user generated libraries
                    _main.PluginService = new PluginService(pathToPlugins);
                    int index = 0;
                    //Load Illustrations
                    foreach (VLAPlugin pluginData in _main.PluginService.PluginIllustrations)
                    {
                        _main.AddListViewItems(pluginData);
                        _main.AddUniqueModuleName(pluginData);
                        _main.SetLog(pluginData.IllustrationName + " Plug-in Added!", LogType.Info);
                        index++;
                    }
                    _main.SetLog(_main.PluginService.PluginIllustrations.Count + " Total Plug-in/s Loaded.",
                                 LogType.Info);
                    //Load _main application add-ons
                    index = 0;
                    foreach (VLAPlugin pluginData in _main.PluginService.PluginSystem)
                    {
                        _main.AddMenuItem(pluginData.SystemName, index);
                        _main.SetLog(pluginData.SystemName + " System Add-on Added!", LogType.Info);
                        index++;
                    }
                    _main.SetLog(_main.PluginService.PluginSystem.Count + " Total Add-On/s Loaded.", LogType.Info);
                }
                catch (Exception ex)
                {
                    _main.SetLog(ex.Message + " (Plug Pack)", LogType.Error);
                }

                string filePath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
                if (!Functions.CheckAndCreateFile("config.ini"))
                {
                    _main.SetLog("Config.ini File Missing!", LogType.Info);
                    _main.SetLog("Config.ini File Created!", LogType.Info);
                }
                _main.LoadConfig(filePath);
            }
            catch (Exception e)
            {
                _main.SetLog(e.Message, LogType.Error);
                _main.ShowMessageBox(e.Message, "Visual Learning Assistant", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Thread.Sleep(2000);
                Dispatcher.Invoke(DispatcherPriority.Normal, (Action) Close);
                LoadMain(_main);
            }
        }

        #endregion

        #region Safe Thread

        private void LoadMain(MainWindow main)
        {
            try
            {
                if (Dispatcher.CheckAccess())
                {
                    main.Show();
                    Close();
                }
                else
                    Dispatcher.Invoke(DispatcherPriority.Normal, new LoadMainDelegate(LoadMain), main);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Event Handlers

        private static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            string folderPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory + "Plug-in\\");
            if (folderPath != null)
            {
                string assemblyPath = Path.Combine(folderPath, new AssemblyName(args.Name).Name + ".dll");
                if (File.Exists(assemblyPath) == false) return null;
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                return assembly;
            }
            return null;
        }

        #endregion

        private delegate void LoadMainDelegate(MainWindow main);
    }
}