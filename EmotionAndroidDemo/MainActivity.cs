using System;

using Android.App;
using Android.OS;
using Android.Content.PM;
using Emotion;

namespace EmotionAndroidDemo
{
    // the ConfigurationChanges flags set here keep the EGL context
    // from being destroyed whenever the device is rotated or the
    // keyboard is shown (highly recommended for all GL apps)
    [Activity(Label = "EmotionAndroidDemo",
                    ConfigurationChanges = ConfigChanges.KeyboardHidden,
                    ScreenOrientation = ScreenOrientation.SensorLandscape,
                    MainLauncher = true,
                    Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Starter.SetAndroidContext(this);

            Emotion.Engine.Context emotionContext = Starter.GetEmotionContext();
            SetContentView(emotionContext.Window);
            emotionContext.Start();
        }

        protected override void OnPause()
        {

        }

        protected override void OnResume()
        {

        }
    }
}