// Emotion - https://github.com/Cryru/Emotion

#if ANDROID

#region Using

using System;
using Emotion.Debug;
using Emotion.Engine;
using OpenTK;
using OpenTK.Platform.Android;

#endregion

namespace Emotion.Mobile
{
    public class GLView : AndroidGameView
    {
        /// <summary>
        /// The context this window belongs to.
        /// </summary>
        internal Context EmotionContext;

        public GLView(Android.Content.Context androidContext, Context emotionContext) : base(androidContext)
        {
            EmotionContext = emotionContext;

            // Apply settings to properties.
            //Title = EmotionContext.Settings.WindowTitle;
            Width = EmotionContext.Settings.WindowWidth;
            Height = EmotionContext.Settings.WindowHeight;

            // Setup window location.
            //X = DisplayDevice.Default.Width / 2 - Width / 2;
            //Y = DisplayDevice.Default.Height / 2 - Height / 2;

            AutoSetContextOnRenderFrame = false;
            RenderOnUIThread = false;
        }

        internal void Run(int a, int b)
        {
            Run(a);
        }

        internal void Destroy()
        {
            Close();
        }

        internal void Exit()
        {

        }

        protected override void OnResize(EventArgs e)
        {
            EmotionContext.Renderer.SetViewport();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            EmotionContext.LoopUpdate((float) (e.Time * 1000));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            EmotionContext.LoopDraw();
        }

        #region Platform

        protected override void CreateFrameBuffer()
        {
            try
            {
                Debugger.Log(MessageType.Info, MessageSource.Engine, "Creating context with default settings...");

                base.CreateFrameBuffer();
                return;
            }
            catch (Exception e)
            {
                Debugger.Log(MessageType.Error, MessageSource.Engine, e.ToString());
            }

            // if default creation failed, try with low settings.
            try
            {
                Debugger.Log(MessageType.Info, MessageSource.Engine, "Creating context with low settings...");
                GraphicsMode = new AndroidGraphicsMode(0, 0, 0, 0, 0, false);

                base.CreateFrameBuffer();
                return;
            }
            catch (Exception e)
            {
                Debugger.Log(MessageType.Info, MessageSource.Engine, e.ToString());
            }

            throw new Exception("Can't load egl, aborting");
        }

        #endregion
    }
}

#endif