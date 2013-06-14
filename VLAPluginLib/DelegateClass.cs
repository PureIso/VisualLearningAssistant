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
// //	File Name:		DelegateClass.cs
// //	Created:		06-04-2013 Time: 00:09
// //	Last Edited:	16-04-2013 Time: 21:41
// //=======================================================================//
// //=======================================================================//

#endregion

#region

using System.Windows;
using System.Windows.Controls;

#endregion

namespace VLAPluginLib
{
    /// <summary>
    ///     Add Delegate to remove a specific uid from canvas
    /// </summary>
    /// <param name="canvas">The canvas to use</param>
    /// <param name="uid">The uid to remove</param>
    public delegate void UseCanvasDelegate(Canvas canvas, string uid);

    /// <summary>
    ///     Set a text
    /// </summary>
    /// <param name="value">The text to set</param>
    public delegate void SetText(string value);

    /// <summary>
    ///     Add Language to the Menu Item
    /// </summary>
    /// <param name="item">The Language</param>
    public delegate void AddLanguageDelegate(Languages item);

    /// <summary>
    ///     Set Brush Delegate for Themes
    /// </summary>
    /// <param name="brush">The brush colour in Hex or String of the theme</param>
    /// <param name="background">The background of the theme</param>
    /// <param name="buttonBackground">The button background of the theme</param>
    public delegate void SetBrushDelegate(string brush, string background, string buttonBackground);

    /// <summary>
    ///     Set multiple brush delegate
    /// </summary>
    /// <param name="brushBorder">The border brush</param>
    /// <param name="brushMain">The main brush</param>
    /// <param name="thickBorder">The thickness border brush</param>
    /// <param name="formBackground">The foreground brush</param>
    public delegate void SetMultiBrushDelegate(
        string brushBorder, string brushMain, string thickBorder, string formBackground);

    /// <summary>
    ///     Display a Message box
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="header">The header of the message</param>
    /// <param name="messageButton">The message box button type</param>
    /// <param name="messageImage">The message box image</param>
    public delegate void MessageBoxDelegate(
        string message, string header, MessageBoxButton messageButton, MessageBoxImage messageImage);

    /// <summary>
    ///     Get a point of a point target
    /// </summary>
    /// <param name="element">The Element the mouse would be on</param>
    public delegate Point GetPositionDelegate(IInputElement element);

    /// <summary>
    ///     Enable or Disable a Control
    /// </summary>
    /// <param name="item">The control name</param>
    /// <param name="isEnable">True to Enable and False to Disable</param>
    public delegate void ControlIsEnableDelegate(Control item, bool isEnable);

    /// <summary>
    ///     Display information into a Rich text box
    /// </summary>
    /// <param name="text">The text we will display</param>
    /// <param name="logType">Specify the type of Icon we want</param>
    public delegate RichTextBox LogTextDelegate(string text, LogType logType);

    /// <summary>
    ///     Add IVLAPlugin to a list view
    /// </summary>
    /// <param name="item">The IVLAPlugin</param>
    public delegate void AddItemDelegate(VLAPlugin item);

    /// <summary>
    ///     Change the text on a Label
    /// </summary>
    /// <param name="label">The Label Control</param>
    /// <param name="text">The string text</param>
    public delegate void LabelDelegate(Label label, string text);

    /// <summary>
    ///     Manipulate the progress bar
    /// </summary>
    /// <param name="min">The min value</param>
    /// <param name="max">The max value</param>
    /// <param name="current">The current value</param>
    /// <param name="label">The label</param>
    public delegate void ProgressBarDelegate(int min, int max, int current, string label);

    /// <summary>
    ///     Add a menuItem delegate
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="tagIndex">The index tag to identify the plug-in or add-on</param>
    public delegate void AddMenuItemDelegate(string name, int tagIndex);
}