﻿// Emotion - https://github.com/Cryru/Emotion

#region Using

using System.Threading.Tasks;
using Emotion.Engine;
using Emotion.Game.Layering;
using Emotion.Graphics;
using Emotion.IO;
using Emotion.Primitives;
using Soul;

#endregion

namespace EmotionSandbox.Examples.Generic
{
    public class LoadingScreen : Layer
    {
        private float _deg;
        private bool _logoLoaded;
        private bool _circleLoaded;

        public override void Load()
        {
            Task.Run(() =>
            {
                Context.AssetLoader.Get<Texture>("EmotionLogo.png");
                _logoLoaded = true;
            });
            Task.Run(() =>
            {
                Context.AssetLoader.Get<Texture>("LoadingCircleHalf.png");
                _circleLoaded = true;
            });
        }

        public override void Draw(Renderer renderer)
        {
            float size = Context.Settings.RenderHeight - 160;
            float centerX = Context.Settings.RenderWidth / 2 - size / 2;
            float centerY = Context.Settings.RenderHeight / 2 - size / 2;
            float logoCenterX = Context.Settings.RenderWidth / 2 - size / 4;
            float logoCenterY = Context.Settings.RenderHeight / 2 - size / 4;

            Matrix4 rotationMatrix =
                Matrix4.CreateTranslation(size / 2, size / 2, 0).Inverted() *
                Matrix4.CreateRotationZ(Convert.DegreesToRadians((int) _deg)) *
                Matrix4.CreateTranslation(size / 2, size / 2, 0) *
                Matrix4.CreateTranslation(centerX, centerY, 0);

            renderer.Render(new Vector3(0, 0, 0), Context.Settings.RenderSize, new Color("#35383d"));

            if (_circleLoaded)
            {
                renderer.MatrixStack.Push(rotationMatrix);
                renderer.Render(Vector3.Zero, new Vector2(size, size), Color.White, Context.AssetLoader.Get<Texture>("LoadingCircleHalf.png"));
                renderer.MatrixStack.Pop();
            }

            if (_logoLoaded) renderer.Render(new Vector3(logoCenterX, logoCenterY, 0), new Vector2(size / 2, size / 2), Color.White, Context.AssetLoader.Get<Texture>("EmotionLogo.png"));
        }

        public override void Update(float frameTime)
        {
            _deg += 0.5f * frameTime;
            if (_deg >= 360) _deg = 0;
        }

        public override void Unload()
        {
            Context.AssetLoader.Destroy("EmotionLogo.png");
            Context.AssetLoader.Destroy("LoadingCircleHalf.png");
        }
    }
}