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
// //	File Name:		App.xaml.cs
// //	Created:		23-03-2013 Time: 11:46
// //	Last Edited:	23-03-2013 Time: 11:46
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

#endregion

namespace VisualLearningAssistant
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        ///     Current app instance
        /// </summary>
        public static App Instance;

        /// <summary>
        ///     App constructor
        /// </summary>
        public App()
        {
            Instance = this;
            //Set the default Language
            SetLanguageResourceDictionary(GetXamlFilePath("en-IE"));
        }

        #region Functions

        /// <summary>
        ///     Dynamically load a Localization ResourceDictionary from a file
        /// </summary>
        public void SwitchLanguage(string language)
        {
            if (CultureInfo.CurrentCulture.Name.Equals(language))
                return;
            CultureInfo cultureInfo = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            SetLanguageResourceDictionary(GetXamlFilePath(language));
        }

        /// <summary>
        ///     Returns the path to the ResourceDictionary file based on the language character string.
        /// </summary>
        /// <param name="language">The language loc-Name</param>
        /// <returns>A new Uri with the resource location</returns>
        private static ResourceDictionary GetXamlFilePath(string language)
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "Language-Pack\\LocalizationDictionary." +
                              language + ".xaml";
            ResourceDictionary dic = null;
            if (File.Exists(fileName))
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    // Read in ResourceDictionary File
                    dic = (ResourceDictionary)XamlReader.Load(fileStream);
                }
            }
            return dic;
        }

        /// <summary>
        ///     Sets or replaces the ResourceDictionary by dynamically loading
        ///     a Localization ResourceDictionary from the file path passed in.
        /// </summary>
        /// <param name="inFile">The Uri file location</param>
        private void SetLanguageResourceDictionary(ResourceDictionary inFile)
        {
            if (inFile == null) return;
            try
            {
                // Remove any previous Localization dictionaries loaded
                int langDictId = -1;
                for (int i = 0; i < Resources.MergedDictionaries.Count; i++)
                {
                    ResourceDictionary md = Resources.MergedDictionaries[i];
                    // Make sure your Localization ResourceDictionarys have the ResourceDictionaryName
                    // key and that it is set to a value starting with "Loc-".
                    if (!md.Contains("ResourceDictionaryName"))
                        continue;
                    if (!md["ResourceDictionaryName"].ToString().StartsWith("Loc-"))
                        continue;
                    langDictId = i;
                    break;
                }
                if (langDictId == -1)
                {
                    // Add in newly loaded Resource Dictionary
                    Resources.MergedDictionaries.Add(inFile);
                }
                else
                {
                    // Replace the current language dictionary with the new one
                    Resources.MergedDictionaries[langDictId] = inFile;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion
    }
}