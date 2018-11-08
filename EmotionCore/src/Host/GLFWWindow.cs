// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using Emotion.Engine;
using Emotion.Primitives;
using glfw3;
using OpenTK.Graphics;
using OpenTK.Input;

#endregion

namespace Emotion.Host
{
    public class GLFWWindow : IHost
    {
        private GLFWwindow win;
        private Action<float> update;
        private Action<float> draw;

        public GLFWWindow(Settings settings)
        {
            Glfw.Init();

            win = Glfw.CreateWindow(settings.WindowWidth, settings.WindowHeight, settings.WindowTitle, null, null);
            Glfw.MakeContextCurrent(win);

            IntPtr winHandle = Glfw.GetWindowUserPointer(win);
            var openTkBridge = OpenTK.Platform.Utilities.CreateDummyWindowInfo();
            var context = new GraphicsContext(GraphicsMode.Default, openTkBridge);
            context.MakeCurrent(openTkBridge);

            //glfw3.
            //GraphicsContext a = new GraphicsContext();
        }

        public Vector2 Size { get; set; }
        public Vector2 RenderSize { get; }
        public bool Focused { get; }

        public void ApplySettings(Settings settings)
        {
        }

        public void SetHooks(Action<float> update, Action<float> draw)
        {
            this.update = update;
            this.draw = draw;
        }

        public void Run()
        {
            win.Show();
            while (Glfw.WindowShouldClose(win) == 1)
            {
                update?.Invoke(16);
                draw?.Invoke(16);

                Glfw.PollEvents();
            }
        }

        public void SwapBuffers()
        {
            win.SwapBuffers();
        }

        public event EventHandler<MouseButtonEventArgs> MouseDown;
        public event EventHandler<MouseButtonEventArgs> MouseUp;
        public event EventHandler<MouseMoveEventArgs> MouseMove;
        public event EventHandler<EventArgs> FocusedChanged;

        public void Close()
        {
            win.Close();
        }

        public void Dispose()
        {
            Glfw.Terminate();
        }
    }
}