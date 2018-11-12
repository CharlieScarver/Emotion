// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using Emotion.Engine.Threading;
using Emotion.Primitives;
using OpenGL;

#endregion

namespace Emotion.Graphics.GLES
{
    /// <summary>
    /// A Vertex Buffer Object (VBO) is an OpenGL feature that provides methods for uploading vertex data (position, normal
    /// vector, color, etc.) to the video device for non-immediate-mode rendering.
    /// </summary>
    public class Buffer : IGLObject
    {
        #region Properties

        /// <summary>
        /// The number of components contained within the data.
        /// </summary>
        public uint ComponentCount { get; private set; }

        /// <summary>
        /// The size of the buffer in vertices.
        /// </summary>
        public uint Size { get; private set; }

        #endregion

        #region Static

        /// <summary>
        /// The currently bound buffer.
        /// </summary>
        public static uint BoundPointer { get; internal set; }

        #endregion

        /// <summary>
        /// The pointer of the buffer within OpenGL.
        /// </summary>
        protected uint _pointer { get; private set; }

        /// <summary>
        /// Create a new buffer, and allocate empty space for it.
        /// </summary>
        /// <param name="size">The size of buffer to allocate.</param>
        /// <param name="componentCount">The number of components contained within the data.</param>
        /// <param name="usageHint">What the buffer will be used for.</param>
        public Buffer(uint size, uint componentCount, BufferUsage usageHint = BufferUsage.StaticDraw)
        {
            Size = size;
            _pointer = Gl.GenBuffer();
            Upload(size, componentCount, usageHint);
        }

        #region API

        /// <summary>
        /// Use this buffer for any following operations.
        /// </summary>
        public void Bind()
        {
            if (BoundPointer == _pointer) return;
            BoundPointer = _pointer;
            Gl.BindBuffer(BufferTarget.ArrayBuffer, _pointer);
        }

        /// <summary>
        /// Stop using any buffer.
        /// </summary>
        public void Unbind()
        {
            BoundPointer = 0;
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// Uploads an empty size buffer.
        /// </summary>
        /// <param name="size">The size to allocate.</param>
        /// <param name="componentCount">The number of components contained within the data.</param>
        /// <param name="usageHint">What the buffer will be used for.</param>
        public void Upload(uint size, uint componentCount, BufferUsage usageHint)
        {
            if (_pointer == 0) throw new Exception("Cannot allocate in a destroyed buffer.");

            ComponentCount = componentCount;

            GLThread.ExecuteGLThread(() =>
            {
                Bind();
                Gl.BufferData(BufferTarget.ArrayBuffer, size, IntPtr.Zero, usageHint);
            });
        }

        /// <summary>
        /// Uploads the provided vertex data to the GPU.
        /// </summary>
        /// <param name="data">The vertex data for this VBO.</param>
        /// <param name="componentCount">The number of components contained within the data.</param>
        /// <param name="usageHint">What the buffer will be used for.</param>
        public void Upload(float[] data, uint componentCount, BufferUsage usageHint)
        {
            if (_pointer == 0) throw new Exception("Cannot upload data ot a destroyed buffer.");

            ComponentCount = componentCount;

            GLThread.ExecuteGLThread(() =>
            {
                Bind();
                Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (data.Length * sizeof(float)), data, usageHint);
            });
        }

        /// <summary>
        /// Uploads the provided vertex data to the GPU.
        /// </summary>
        /// <param name="data">The vertex data for this VBO.</param>
        /// <param name="componentCount">The number of components contained within the data.</param>
        /// <param name="usageHint">What the buffer will be used for.</param>
        public void Upload(uint[] data, uint componentCount, BufferUsage usageHint)
        {
            if (_pointer == 0) throw new Exception("Cannot upload data ot a destroyed buffer.");

            ComponentCount = componentCount;

            GLThread.ExecuteGLThread(() =>
            {
                Bind();
                Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (data.Length * sizeof(uint)), data, usageHint);
            });
        }

        /// <summary>
        /// Uploads the provided vertex data to the GPU.
        /// </summary>
        /// <param name="data">The vertex data for this VBO.</param>
        /// <param name="usageHint">What the buffer will be used for.</param>
        public void Upload(Vector3[] data, BufferUsage usageHint)
        {
            if (_pointer == 0) throw new Exception("Cannot upload data ot a destroyed buffer.");

            ComponentCount = 3;

            GLThread.ExecuteGLThread(() =>
            {
                Bind();
                Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (data.Length * Vector3.SizeInBytes), data, usageHint);
            });
        }

        /// <summary>
        /// Uploads the provided vertex data to the GPU.
        /// </summary>
        /// <param name="data">The vertex data for this VBO.</param>
        /// <param name="usageHint">What the buffer will be used for.</param>
        public void Upload(Vector2[] data, BufferUsage usageHint)
        {
            if (_pointer == 0) throw new Exception("Cannot upload data ot a destroyed buffer.");

            ComponentCount = 2;

            GLThread.ExecuteGLThread(() =>
            {
                Bind();
                Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (data.Length * Vector2.SizeInBytes), data, usageHint);
            });
        }

        /// <summary>
        /// Delete the buffer and its data, freeing memory.
        /// </summary>
        public void Delete()
        {
            Gl.DeleteBuffers(_pointer);
            _pointer = 0;
        }

        #endregion
    }
}