using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VLAPluginLib;
using System.Xml;
using System.Reflection;
using System.Net;
using System.IO;

//==================================================================//
// MainWindow partial class for Threads, delegates and Dispatchers  //
//==================================================================//

namespace VisualLearningAssistant
{
    public partial class MainWindow
    {
        private MessageBoxResult _messageboxResult = MessageBoxResult.No;//A way around getting a return value from dispatcher.invoke

        #region Dispatcher Check Access
        internal void AddLanguage(Languages item)
        {
            try
            {
                if (MainMenuLanguage.Dispatcher.CheckAccess())
                {
                    MenuItem newMenuItem = new MenuItem {Header = item.Name, Tag = item.LocName};
                    newMenuItem.Click += LanguageClickHandler;
                    if (item.LocName != "en-IE") //Default language in DLL also in main app
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

        internal void AddListViewItems(IVLAPlugin item)
        {
            try
            {
                if (PluginListView.Dispatcher.CheckAccess())
                {
                    PluginListItem data = new PluginListItem(item.Name, item.Version, false);
                    PluginListView.Items.Add(data);
                }
                else
                    PluginListView.Dispatcher.Invoke(DispatcherPriority.Normal, new AddItem(AddListViewItems), item);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal void IsEnable(Control item, bool isEnable)
        {
            try
            {
                if (item.Dispatcher.CheckAccess())
                {
                    item.IsEnabled = isEnable;
                }
                else
                    item.Dispatcher.Invoke(DispatcherPriority.Normal, new ControlIsEnable(IsEnable), item, isEnable);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SetLabel(Label label, string text)
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

        private void SetProgressBar(int min, int max, int current, [Optional, DefaultParameterValue("Idle")] string label)
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
                    MainProgressBar.Dispatcher.Invoke(DispatcherPriority.Normal, new ProgressBarDelegate(SetProgressBar), min, max, current, label);
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
                    Dispatcher.Invoke(DispatcherPriority.Normal, new MessageBoxShower(ShowMessageBox),
                                      message, header, messageButton, messageImage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        internal void SetLog(string text, LogType logType)
        {
            try
            {
                if (RichTextBoxLog.Dispatcher.CheckAccess())
                {
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
                    RichTextBoxLog.Dispatcher.Invoke(DispatcherPriority.Normal, new LogTextDisplayer(SetLog), text,
                                                     logType);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Function
        /// <summary>
        /// Convert value to file size
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>string value size</returns>
        private static string GetSize(double value)
        {
            if (value < 1024)
                return value + " bytes";
            value = value / 1024;
            if (value < 1024)
                return value + " KB";
            value = value/1024;
            if (value < 1024)
                return value + " MB";
            value = value / 1024;
            if (value < 1024)
                return value + " GB";
            value = value / 1024;
            if (value < 1024)
                return value + " TB";
            throw new Exception("Unknown Size !");
        }

        private int GetCurrentIndex(GetPositionDelegate getPosition)
        {
            int index = -1;
            for (int i = 0; i < this.PluginListView.Items.Count; ++i)
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

        private bool IsMouseOverTarget(Visual target, GetPositionDelegate getPosition)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            Point mousePos = getPosition((IInputElement) target);
            return bounds.Contains(mousePos);
        }

        private void OpenResize()
        {
            MainTabControl.Width = MainGrid.ActualWidth - LeftScrollViewer.ActualWidth - MoveInOutButton.ActualWidth;
            MainTabControl.Margin = new Thickness(261, 40, 0, 27);
        }

        private void CloseResize()
        {
            MainTabControl.Width = MainGrid.ActualWidth - MoveInOutButton.ActualWidth;
            MainTabControl.Margin = new Thickness(MoveInOutButton.ActualWidth, 40, 0, StatusGrid.ActualHeight);
        }

        private void CheckUpdateThread(object c)
        {
            try
            {
                SetLog("Checking update...", LogType.Info);
                SetLabel(StatusLabel, "Checking update.....");
                //xml location
                XmlReader xmlReader = XmlReader.Create("http://vla.azurewebsites.net/VLAUpdate.xml");
                //read and stop at lateVersion element
                xmlReader.Read();
                xmlReader.ReadToFollowing("latestVersion");
                //Read the contents of latest version to xmlVersion string
                string xmlVersion = xmlReader.ReadString();
                xmlReader.ReadToFollowing("latestDownload");
                string xmlDownloadLink = xmlReader.ReadString();
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
                        _messageboxResult = MessageBoxResult.No;
                        WebClient webclient = new WebClient();
                        //=========================================
                        //Rename the current exe to temp
                        string newPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VisualLearningAssistant.exe.bak");
                        if (File.Exists(newPath))
                        {
                            File.Delete(newPath);
                        }
                        File.Move(Assembly.GetExecutingAssembly().Location, newPath);
                        //==========================================

                        //Download file//xmlDownloadLink
                        webclient.DownloadFileAsync(new Uri(xmlDownloadLink), "VisualLearningAssistant.exe");
                        double downloadsize = 0;
                        webclient.DownloadProgressChanged += (s, ev) =>
                        {
                            downloadsize = ev.TotalBytesToReceive;
                            SetProgressBar(1, 100, ev.ProgressPercentage, "Updating... " + ev.ProgressPercentage + "% Complete");
                        };
                        
                        webclient.DownloadFileCompleted += (s, ev) =>
                        {
                            SetLog(GetSize(downloadsize) + " Downloaded!", LogType.Info);
                            webclient.Dispose();
                            ShowMessageBox("File Downloaded!", "Update",MessageBoxButton.OK,MessageBoxImage.Information);
                            //Create a new process containing the information for our updated version.
                            Process proccess = new Process
                            {
                                //Start the new updated process
                                StartInfo = new ProcessStartInfo(Assembly.GetExecutingAssembly().Location)
                            };
                            try
                            {
                                //We then start the process.
                                proccess.Start();
                                //And kill the current.
                                Process.GetCurrentProcess().Kill();
                            }
                            catch (Exception ex)
                            {
                                SetLog(ex.Message, LogType.Error);
                            }
                        };
                        
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