﻿// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using System.Drawing;
using Emotion.GLES;
using Color = Emotion.Primitives.Color;

#endregion

namespace Emotion.Engine
{
    public sealed class Settings
    {
        #region Window Settings

        /// <summary>
        /// The window's title.
        /// </summary>
        public string WindowTitle = "Untitled";

        /// <summary>
        /// The width size of the window.
        /// </summary>
        public int WindowWidth = 960;

        /// <summary>
        /// The height size of the window.
        /// </summary>
        public int WindowHeight = 540;

        /// <summary>
        /// The window mode.
        /// </summary>
        public WindowMode WindowMode = WindowMode.Windowed;

        /// <summary>
        /// The window icon.
        /// </summary>
        public Icon WindowIcon;

        #endregion

        #region Render Settings

        /// <summary>
        /// The color to clear the window with.
        /// </summary>
        public Color ClearColor = Color.CornflowerBlue;

        /// <summary>
        /// The width to render at.
        /// </summary>
        public int RenderWidth = 960;

        /// <summary>
        /// The height to render at.
        /// </summary>
        public int RenderHeight = 540;

        /// <summary>
        /// The maximum fps to render at.
        /// </summary>
        public int CapFPS = 60;

        #endregion

        #region Scripting Settings

        /// <summary>
        /// The maximum time a script can run before it is stopped.
        /// </summary>
        public TimeSpan ScriptTimeout = new TimeSpan(0, 0, 0, 0, 500);

        /// <summary>
        /// Whether to crash on scripting errors.
        /// </summary>
        public bool StrictScripts = false;

        #endregion

        #region Sound Settings

        /// <summary>
        /// Whether to play sound.
        /// </summary>
        public bool Sound = true;

        /// <summary>
        /// The volume to play sound at. From 0 to 100.
        /// </summary>
        public int Volume = 50;

        #endregion
    }
}