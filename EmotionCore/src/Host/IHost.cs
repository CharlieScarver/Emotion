// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using Emotion.Engine;
using Emotion.Primitives;

#endregion

namespace Emotion.Host
{
    public interface IHost
    {
        Vector2 Size { get; set; }
        Vector2 RenderSize { get; }
        bool Focused { get; }

        void ApplySettings(Settings settings);
        void SetHooks(Action<float> update, Action<float> draw);

        void Run();
        void SwapBuffers();

        // Events.
        event EventHandler<EventArgs> MouseDown;
        event EventHandler<EventArgs> MouseUp;
        event EventHandler<EventArgs> MouseMove;
        event EventHandler<EventArgs> FocusedChanged;

        // Cleanup.
        void Close();
        void Dispose();
    }
}