// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Emotion.Debug;
using Emotion.Engine;
using Emotion.Primitives;
using Emotion.Utils;
using glfw3;
using OpenGL;
using Debugger = Emotion.Debug.Debugger;

#endregion

namespace Emotion.Host
{
    public class GLFWWindow : IHost
    {
        #region Properties

        public Vector2 Size { get; set; }
        public Vector2 RenderSize { get; private set; }
        public bool Focused { get; private set; } = true;

        #endregion

        #region Private Objects

        private GLFWwindow _win;
        private Action<float> _update;
        private Action<float> _draw;

        // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
        private GLFWwindowfocusfun _focusCallback;
        private GLFWkeyfun _keyCallback;
        private GLFWerrorfun _errorCallback;
        // ReSharper enable PrivateFieldCanBeConvertedToLocalVariable

        #endregion

        #region Events

        public event EventHandler<EventArgs> MouseDown;
        public event EventHandler<EventArgs> MouseUp;
        public event EventHandler<EventArgs> MouseMove;
        public event EventHandler<EventArgs> FocusedChanged;

        #endregion

        public GLFWWindow(Settings settings)
        {
            // Set settings.
            RenderSize = new Vector2(settings.RenderWidth, settings.RenderHeight);

            // Initiate GLFW window.
            Glfw.Init();

            Glfw.WindowHint((int)State.ContextVersionMajor, 3);
            Glfw.WindowHint((int)State.ContextVersionMinor, 3);
            Glfw.WindowHint((int)State.OpenglForwardCompat, 1);
            Glfw.WindowHint((int) State.OpenglProfile, (int) State.OpenglCompatProfile);
            //Glfw.WindowHint((int) State.ClientApi, (int) State.OpenglEsApi);

#if DEBUG
            Glfw.WindowHint((int) State.OpenglDebugContext, 1);
#endif

            Gl.Initialize();

            _win = Glfw.CreateWindow(settings.WindowWidth, settings.WindowHeight, settings.WindowTitle, null, null);
            Glfw.MakeContextCurrent(_win);

            Gl.BindAPI();

            // Attach callbacks.
            _keyCallback = KeyCallback;
            Glfw.SetKeyCallback(_win, _keyCallback);

            _focusCallback = FocusCallback;
            Glfw.SetWindowFocusCallback(_win, _focusCallback);

            _errorCallback = ErrorCallback;
            Glfw.SetErrorCallback(_errorCallback);

            _win.SizeChanged += (sender, args) => { ResizeCallback(); };

            // Initiate


            //IntPtr winHandle = Glfw.GetWindowUserPointer(win);
            //DeviceContext device = DeviceContext.Create(IntPtr.Zero, winHandle);
            //IntPtr context = device.CreateContext(IntPtr.Zero);
            //device.MakeCurrent(context);

            //var openTkBridge = OpenTK.Platform.Utilities.CreateDummyWindowInfo();
            //var context = new GraphicsContext(GraphicsMode.Default, openTkBridge);
            //context.MakeCurrent(openTkBridge);

            //glfw3.
            //GraphicsContext a = new GraphicsContext();
        }

        #region Callbacks

        private void KeyCallback(IntPtr win, int key, int scanCode, int action, int mods)
        {
        }

        private void FocusCallback(IntPtr win, int state)
        {
            Focused = state == 1;
        }

        private void ErrorCallback(int errorCode, string errorText)
        {
            Debugger.Log(MessageType.Error, MessageSource.Engine, errorText);
        }

        private void ResizeCallback()
        {
            int clientSizeWidth = 0;
            int clientSizeHeight = 0;
            Glfw.GetWindowSize(_win, ref clientSizeWidth, ref clientSizeHeight);

            //Calculate borderbox / pillarbox.
            float targetAspectRatio = RenderSize.X / RenderSize.Y;

            float width = clientSizeWidth;
            float height = (int) (width / targetAspectRatio + 0.5f);

            // If the height is bigger then the black bars will appear on the top and bottom, otherwise they will be on the left and right.
            if (height > clientSizeHeight)
            {
                height = clientSizeHeight;
                width = (int) (height * targetAspectRatio + 0.5f);
            }

            int vpX = (int) (clientSizeWidth / 2 - width / 2);
            int vpY = (int) (clientSizeWidth / 2 - height / 2);

            // Set viewport.
            Gl.Viewport(vpX, vpY, (int) width, (int) height);
            Gl.Scissor(vpX, vpY, (int) width, (int) height);

            Helpers.CheckError("window resize");
        }

        #endregion


        public void ApplySettings(Settings settings)
        {
        }

        public void SetHooks(Action<float> update, Action<float> draw)
        {
            _update = update;
            _draw = draw;
        }

        public void Run()
        {
            _win.Show();
            ResizeCallback();
  
            Stopwatch timeCounter = new Stopwatch();
            while (Glfw.WindowShouldClose(_win) == 0)
            {
                Glfw.PollEvents();

                _update?.Invoke(timeCounter.ElapsedMilliseconds);
                _draw?.Invoke(timeCounter.ElapsedMilliseconds);

                timeCounter.Restart();

                Task.Delay(16).Wait();
            }
        }

        public void SwapBuffers()
        {
            _win.SwapBuffers();
        }

        public void Close()
        {
            _win.Close();
        }

        public void Dispose()
        {
            _win.Dispose();
            Glfw.Terminate();
        }
    }
}