﻿// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;

#endregion

namespace Emotion.Engine.Threading
{
    public static class GLThread
    {
        private static ThreadManager _threadManager;

        static GLThread()
        {
            _threadManager = new ThreadManager("GL Thread");
        }

        /// <summary>
        /// Binds the current thread as the GL thread.
        /// </summary>
        internal static void BindThread()
        {
            _threadManager.BindThread();
        }

        /// <summary>
        /// Performs queued tasks on the GL thread.
        /// </summary>
        internal static void Run()
        {
            _threadManager.Run();
        }

        #region API

        /// <summary>
        /// Returns whether the executing thread is the GL thread.
        /// </summary>
        /// <returns>True if the thread on which this is called is the GL thread, false otherwise.</returns>
        public static bool IsGLThread()
        {
            return _threadManager.IsManagedThread();
        }

        /// <summary>
        /// Execute the action on the GL thread. Will block the current thread until ready.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public static void ExecuteGLThread(Action action)
        {
            _threadManager.ExecuteOnThread(action);
        }

        /// <summary>
        /// Check whether the executing thread is the GL thread. If it's not an exception is thrown.
        /// </summary>
        public static void ForceGLThread()
        {
            _threadManager.ForceThread();
        }

        #endregion
    }
}