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
// //	File Name:		MainWindow.xaml.cs
// //	Created:		06-04-2013 Time: 00:10
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using VLAPluginLib;

#endregion

namespace VisualLearningAssistant
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Variables

        internal string CurrentTheme;
        internal PluginService PluginService;

        private const double Maintabthicknessleft = 271.332;
        private readonly Storyboard _close;
        private readonly Storyboard _open;
        private bool _closing;
        private bool _leftMenuInOut;
        private List<int> _moduleIndex;
        private int _selectPluginIndex;

        //A way around getting a return value from dispatcher.invoke
        private MessageBoxResult _messageboxResult = MessageBoxResult.No;
        private RichTextBox _richTextBox;

        #endregion

        /// <summary>
        ///     Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            ModuleNamesComboBox.Items.Add("None");
            ModuleNamesComboBox.SelectedIndex = 0;
            _close = (Storyboard) FindResource("LeftMove");
            _open = (Storyboard) FindResource("MoveBack");
            _selectPluginIndex = -1;
            _leftMenuInOut = false;
        }

        #region Event Handlers

        //Close Animation button Click
        private void ClosingAnimationTrigger(object sender, CancelEventArgs e)
        {
            Storyboard unloaded = (Storyboard) FindResource("OnUnloaded1");
            Storyboard loadAnimation = unloaded.Clone();
            loadAnimation.Completed += (s, a) =>
                {
                    _closing = true;
                    Close();
                };
            BeginStoryboard(loadAnimation);
            if (_closing == false)
                e.Cancel = true;
        }

        //Plug-in Module Change
        private void ModuleNamesComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PluginListView.Items.Clear();
            _moduleIndex = new List<int>();

            if (ModuleNamesComboBox.SelectedIndex == -1) return;
            if (PluginService == null) return;

            string module = ModuleNamesComboBox.Items.GetItemAt(ModuleNamesComboBox.SelectedIndex).ToString();
            SetLog(module + " module filter selected.", LogType.Info);

            int index = 0;
            foreach (VLAPlugin plugin in PluginService.PluginIllustrations)
            {
                if ("None" == module && plugin.PluginType != PluginType.System)
                {
                    _moduleIndex.Add(index);
                    AddListViewItems(plugin);
                }
                else
                {
                    foreach (string pluginModule in plugin.IllustrationModuleName)
                    {
                        if (module == pluginModule)
                        {
                            _moduleIndex.Add(index);
                            AddListViewItems(plugin);
                        }
                    }
                }
                index++;
            }

            IsEnable(LoadButton, false);
            PluginInfoNameTextBlock.Clear();
            PluginInfoAuthorTextBlock.Clear();
            PluginInfoVersionTextBlock.Clear();
            PluginInfoDescriptionTextBlock.Clear();
            PluginInfoSupportedLanguagesTextBlock.Clear();
            PluginInfoModuleNameTextBlock.Clear();
        }

        //Double click the Header to enlarge the application or make the size default
        private void IsoAppHeaderMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal
                              ? WindowState.Maximized
                              : WindowState.Normal;
        }

        //Load plug-in button click
        private void LoadPluginButtonClick(object sender, RoutedEventArgs e)
        {
            IsEnable(LoadButton, false);
            IEnumerable items = PluginListView.Items;
            foreach (PluginListItem item in items)
            {
                if (!item.Selected) continue;
                try
                {
                    MainTab.SelectedIndex = 1;
                    SetLog(
                        "Visual Learning Assistant - " +
                        PluginService.PluginIllustrations[_selectPluginIndex].IllustrationName +
                        " Loaded.",
                        LogType.Info);
                    PluginService.PluginIllustrations[_selectPluginIndex].Display(DisplayCanvas, SetLog, SetProgressBar);
                }
                catch (Exception ex)
                {
                    SetLog(ex.Message, LogType.Error);
                    ShowMessageBox(ex.Message, "Visual Learning Assistant", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            IsEnable(LoadButton, true);
        }

        //Load application add on type plug-in
        private void AddOnHandler(object sender, RoutedEventArgs e)
        {
            //uncheck all items
            foreach (MenuItem item in AddOnMenuItem.Items)
                item.IsChecked = false;

            //Get the menu item sender of the click event
            MenuItem menuItem = (MenuItem) sender;
            if (menuItem == null) return;
            //Set the check of sender to true
            menuItem.IsChecked = true;
            //Call the app instance switch language
            PluginService.PluginSystem[int.Parse(menuItem.Tag.ToString())].Display(null, SetLog, SetProgressBar);
        }

        //Plug-in Checkbox selection
        private void ListViewCheckBox(object sender, RoutedEventArgs e)
        {
            if (_selectPluginIndex == -1)
                return;

            PluginListItem selectedPlugin = (PluginListItem) PluginListView.Items[_selectPluginIndex];
            selectedPlugin.Selected = true;
            for (int i = 0; i < PluginListView.Items.Count; i++)
            {
                if (i == _selectPluginIndex)
                    continue;
                PluginListItem item = (PluginListItem) PluginListView.Items[i];
                item.Selected = false;
            }

            IEnumerable items = PluginListView.Items;
            int index = 0;
            foreach (PluginListItem item in items)
            {
                if (!item.Selected)
                {
                    index++;
                    continue;
                }
                try
                {
                    if (_moduleIndex != null)
                        if (_moduleIndex.Count != 0)
                            _selectPluginIndex = _moduleIndex[index];

                    IsEnable(LoadButton, true);
                    PluginInfoNameTextBlock.Text =
                        PluginService.PluginIllustrations[_selectPluginIndex].IllustrationName;
                    PluginInfoAuthorTextBlock.Text =
                        PluginService.PluginIllustrations[_selectPluginIndex].IllustrationAuthor;
                    PluginInfoVersionTextBlock.Text =
                        PluginService.PluginIllustrations[_selectPluginIndex].IllustrationVersion;
                    PluginInfoDescriptionTextBlock.Text =
                        PluginService.PluginIllustrations[_selectPluginIndex].IllustrationDescription;

                    PluginInfoSupportedLanguagesTextBlock.Clear();
                    foreach (
                        string language in
                            PluginService.PluginIllustrations[_selectPluginIndex].IllustrationSupportedLanguages)
                    {
                        PluginInfoSupportedLanguagesTextBlock.Text += language + Environment.NewLine;
                    }

                    PluginInfoModuleNameTextBlock.Clear();
                    foreach (
                        string module in PluginService.PluginIllustrations[_selectPluginIndex].IllustrationModuleName)
                    {
                        PluginInfoModuleNameTextBlock.Text += module + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message, "Visual Learning Assistant", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Option button click
        private void OptionClick(object sender, RoutedEventArgs e)
        {
            Option option = new Option(this) {Owner = this};
            option.MessageBoxEvent += ShowMessageBox;
            option.ShowDialog();
        }

        //Language selection change
        private void LanguageClickHandler(object sender, RoutedEventArgs e)
        {
            //uncheck all items
            foreach (MenuItem item in MainMenuLanguage.Items)
                item.IsChecked = false;

            //Get the menu item sender of the click event
            MenuItem menuItem = (MenuItem) sender;
            if (menuItem == null) return;
            //Set the check of sender to true
            menuItem.IsChecked = true;
            //Call the app instance switch language
            SetLog("Language Changed to " + menuItem.Header, LogType.Info);
            App.Instance.SwitchLanguage(menuItem.Tag.ToString());
        }

        private void IsoAppHeaderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return; // Or use SystemInformation.MouseButtonsSwapped
            _selectPluginIndex = GetPluginIndex(e.GetPosition);
        }

        private void MoveInOutButtonClick(object sender, RoutedEventArgs e)
        {
            if (_leftMenuInOut)
            {
                BeginStoryboard(_open);
                _leftMenuInOut = false;
                OpenResize();
            }
            else
            {
                BeginStoryboard(_close);
                _leftMenuInOut = true;
                CloseResize();
            }
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_leftMenuInOut)
            {
                CloseResize();
            }
            else
            {
                OpenResize();
            }
        }

        private void AzureHandleRequestNavigate(object sender, RoutedEventArgs e)
        {
            string navigateUri = Azure.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
            SetLog("Navigated to " + navigateUri, LogType.Info);
        }

        private void GoogleCodeHandleRequestNavigate(object sender, RoutedEventArgs e)
        {
            string navigateUri = GoogleCode.NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
            SetLog("Navigated to " + navigateUri, LogType.Info);
        }

        private void UpdateMenuItemClick(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(CheckUpdateThread);
        }

        private void AboutMenuItemClick(object sender, RoutedEventArgs e)
        {
            About about = new About(this) {Owner = this};
            about.ShowDialog();
        }

        #endregion

        #region Dispatcher Check Access

        internal void AddLanguage(Languages item)
        {
            try
            {
                if (MainMenuLanguage.Dispatcher.CheckAccess())
                {
                    Brush br = (Brush) FindResource("Brush1");
                    MenuItem newMenuItem = new MenuItem {Header = item.Name, Tag = item.LocName, Background = br};
                    newMenuItem.Click += LanguageClickHandler;
                    if (item.Name == "English")
                        newMenuItem.IsChecked = true;
                    MainMenuLanguage.Items.Add(newMenuItem);
                }
                else
                    MainMenuLanguage.Dispatcher.Invoke(DispatcherPriority.Normal, new AddLanguageDelegate(AddLanguage),
                                                       item);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal void AddMenuItem(string item, int tagIndex)
        {
            try
            {
                if (AddOnMenuItem.Dispatcher.CheckAccess())
                {
                    Brush br = (Brush) FindResource("Brush1");
                    MenuItem newMenuItem = new MenuItem {Header = item, Tag = tagIndex.ToString(), Background = br};
                    newMenuItem.Click += AddOnHandler;
                    AddOnMenuItem.Items.Add(newMenuItem);
                }
                else
                    AddOnMenuItem.Dispatcher.Invoke(DispatcherPriority.Normal, new AddMenuItemDelegate(AddMenuItem),
                                                    item, tagIndex);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal void AddListViewItems(VLAPlugin item)
        {
            try
            {
                if (PluginListView.Dispatcher.CheckAccess())
                {
                    PluginListItem data = new PluginListItem(item.IllustrationName, item.IllustrationVersion, false);
                    PluginListView.Items.Add(data);
                }
                else
                    PluginListView.Dispatcher.Invoke(DispatcherPriority.Normal, new AddItemDelegate(AddListViewItems),
                                                     item);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal void AddUniqueModuleName(VLAPlugin item)
        {
            try
            {
                if (ModuleNamesComboBox.Dispatcher.CheckAccess())
                {
                    foreach (string module in item.IllustrationModuleName)
                    {
                        if (ModuleNamesComboBox.Items.Contains(module)) return;
                        ModuleNamesComboBox.Items.Add(module);
                    }
                }
                else
                    ModuleNamesComboBox.Dispatcher.Invoke(DispatcherPriority.Normal,
                                                          new AddItemDelegate(AddUniqueModuleName),
                                                          item);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal static void IsEnable(Control item, bool isEnable)
        {
            try
            {
                if (item.Dispatcher.CheckAccess())
                {
                    item.IsEnabled = isEnable;
                }
                else
                    item.Dispatcher.Invoke(DispatcherPriority.Normal, new ControlIsEnableDelegate(IsEnable), item,
                                           isEnable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal void ShowMessageBox(string message, string header, MessageBoxButton messageButton,
                                     MessageBoxImage messageImage)
        {
            try
            {
                if (Dispatcher.CheckAccess())
                    _messageboxResult = MessageBox.Show(this, message, header, messageButton, messageImage);
                else
                    Dispatcher.Invoke(DispatcherPriority.Normal, new MessageBoxDelegate(ShowMessageBox),
                                      message, header, messageButton, messageImage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal RichTextBox SetLog(string text, LogType logType)
        {
            try
            {
                if (RichTextBoxLog.Dispatcher.CheckAccess())
                {
                    _richTextBox = RichTextBoxLog;
                    if (text == null && logType == LogType.None) return _richTextBox;
                    Uri uri = logType == LogType.Info
                                  ? new Uri(
                                        "pack://application:,,,/VisualLearningAssistant;component/Images/information.ico")
                                  : new Uri("pack://application:,,,/VisualLearningAssistant;component/Images/cancel.ico");

                    if (logType == LogType.None)
                    {
                        RichTextBoxLog.AppendText(text);
                    }
                    else
                    {
                        BitmapImage bitmap = new BitmapImage(uri);
                        Image image = new Image {Source = bitmap, Width = 14, Height = 14};
                        InlineUIContainer container = new InlineUIContainer(image);
                        string log = " " + DateTime.Now.ToString("HH:mm:ss tt") + " " + text;
                        Paragraph paragraph = new Paragraph(container) {Margin = new Thickness(0)};
                        paragraph.Inlines.Add(log.Trim());
                        RichTextBoxLog.Document.Blocks.Add(paragraph);
                    }
                }
                else
                {
                    RichTextBoxLog.Dispatcher.Invoke(DispatcherPriority.Normal, new LogTextDelegate(SetLog), text,
                                                     logType);
                }
                return _richTextBox;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal void SetBrush(string mainborderBrush, string brushMain, string thickBorder, string formBackground)
        {
            try
            {
                object convertFromString = ColorConverter.ConvertFromString(mainborderBrush);
                if (MainBorder.Dispatcher.CheckAccess())
                {
                    //MainBorder Brush
                    if (convertFromString != null)
                    {
                        Color color = (Color) convertFromString;
                        MainBorder.BorderBrush = new SolidColorBrush(color);
                    }
                    //Main Background
                    Brush br = (Brush) FindResource(brushMain);
                    MainGrid.Background = br;

                    //Status Strip
                    br = (Brush) FindResource(thickBorder);
                    StatusRectangle.Fill = br;
                    DisplayTabItem.Background = br;
                    LogTabItem.Background = br;
                    LoadButton.Background = br;

                    foreach (MenuItem item in AddOnMenuItem.Items)
                        item.Background = br;
                    foreach (MenuItem item in VLAMenuItem.Items)
                        item.Background = br;
                    foreach (MenuItem item in HelpMenuItem.Items)
                        item.Background = br;
                    foreach (MenuItem item in MainMenuLanguage.Items)
                        item.Background = br;

                    //Other Background
                    br = (Brush) FindResource(formBackground);
                    PluginDetailGrid.Background = br;
                    PluginFilterGrid.Background = br;
                    PluginListGrid.Background = br;
                }
                else
                    MainBorder.Dispatcher.Invoke(DispatcherPriority.Normal, new SetMultiBrushDelegate(SetBrush),
                                                 mainborderBrush, brushMain, thickBorder, formBackground);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static void SetLabel(Label label, string text)
        {
            try
            {
                if (label.Dispatcher.CheckAccess())
                {
                    label.Content = text;
                }
                else
                    label.Dispatcher.Invoke(DispatcherPriority.Normal, new LabelDelegate(SetLabel), label, text);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SetProgressBar(int min, int max, int current,
                                    [Optional, DefaultParameterValue("Idle")] string label)
        {
            try
            {
                if (MainProgressBar.Dispatcher.CheckAccess())
                {
                    MainProgressBar.Minimum = min;
                    MainProgressBar.Maximum = max;
                    MainProgressBar.Value = current;
                    StatusLabel.Content = label;
                    if (max == current)
                    {
                        StatusLabel.Content = "Idle";
                        MainProgressBar.Minimum = 0;
                        MainProgressBar.Value = 0;
                    }
                }
                else
                    MainProgressBar.Dispatcher.Invoke(DispatcherPriority.Normal, new ProgressBarDelegate(SetProgressBar),
                                                      min, max, current, label);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Function

        internal void LoadConfig(string filePath)
        {
            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    switch (line)
                    {
                        case "#Theme#":
                            line = file.ReadLine();
                            switch (line)
                            {
                                case "Default":
                                    SetLog("Default Theme Selected.", LogType.Info);
                                    CurrentTheme = "Default";
                                    SetBrush("#FF7272C2", "Brush2Trans", "Brush2", "Brush1");
                                    break;
                                case "Light":
                                    SetLog("Light Theme Selected.", LogType.Info);
                                    CurrentTheme = "Light";
                                    SetBrush("#FF77CBD6", "Brush4", "Brush4Thick", "Brush4Flat");
                                    break;
                            }
                            break;
                    }
                }
            }
        }

        private static bool IsMouseOverTarget(Visual target, GetPositionDelegate getPosition)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = getPosition((IInputElement) target);
            return bounds.Contains(mousePos);
        }

        private int GetPluginIndex(GetPositionDelegate getPosition)
        {
            int index = -1;
            for (int i = 0; i < PluginListView.Items.Count; ++i)
            {
                ListViewItem item;
                if (PluginListView.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    item = null;
                else
                {
                    item = (ListViewItem) PluginListView.ItemContainerGenerator.ContainerFromIndex(i);
                }

                if (!IsMouseOverTarget(item, getPosition)) continue;
                index = i;
                break;
            }
            return index;
        }

        private void OpenResize()
        {
            MainTab.Width = MainGrid.ActualWidth - LeftScrollViewer.ActualWidth - MoveInOutButton.ActualWidth;
            MainTab.Margin = new Thickness(Maintabthicknessleft, 0, 0, 0);
        }

        private void CloseResize()
        {
            MainTab.Width = MainGrid.ActualWidth - MoveInOutButton.ActualWidth;
            MainTab.Margin = new Thickness(MoveInOutButton.ActualWidth, 0, 0, StatusGrid.ActualHeight);
        }

        private void CheckUpdateThread(object c)
        {
            try
            {
                SetLog("Checking update...", LogType.Info);
                SetLabel(StatusLabel, "Checking update.....");
                //xml location
                XmlReader xmlReader = XmlReader.Create("https://github.com/PureIso/VisualLearningAssistant/tree/master/VisualLearningAssistant/Resources/VLAUpdate.xml");
                //read and stop at lateVersion element
                xmlReader.Read();
                xmlReader.ReadToFollowing("latestVersion");
                //Read the contents of latest version to xmlVersion string
                string xmlVersion = xmlReader.ReadString();
                xmlReader.ReadToFollowing("latestDownload");
                xmlReader.Close();

                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //comparing version
                if (String.CompareOrdinal(xmlVersion, version) > 0)
                {
                    SetLog("Update Found, Version: " + xmlVersion, LogType.Info);
                    SetLabel(StatusLabel, "Checking update.....");

                    ShowMessageBox("Update found! " + Environment.NewLine + "New Version: " + xmlVersion +
                                   Environment.NewLine + "Would you like to download?", "Update", MessageBoxButton.YesNo,
                                   MessageBoxImage.Information);

                    if (_messageboxResult == MessageBoxResult.Yes)
                    {
                        const string navigateUri = "http://code.google.com/p/visual-learning-assistant/downloads/list";
                        Process.Start(new ProcessStartInfo(navigateUri));
                        SetLog("User redirected to update download page: " + navigateUri, LogType.Info);
                    }
                    else
                    {
                        SetLog("User was not redirected to site.", LogType.Info);
                    }
                }
                else
                {
                    SetLog("No updates found, you have the latest version.", LogType.Info);
                    ShowMessageBox("No Update Found", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                SetLog(ex.Message, LogType.Info);
                ShowMessageBox(ex.Message, "Update", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                SetLabel(StatusLabel, "Idle");
            }
        }

        #endregion
    }
}