// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using Emotion.Engine.Threading;
using OpenGL;

#endregion

namespace Emotion.Graphics.GLES
{
    /// <summary>
    /// The index buffer is a list of pointers within the vertex buffer.
    /// </summary>
    public sealed class IndexBuffer : IGLObject
    {
        #region Properties

        /// <summary>
        /// The number of indices.
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Static

        /// <summary>
        /// The currently bound buffer.
        /// </summary>
        public static uint BoundPointer;

        #endregion

        /// <summary>
        /// The pointer of the buffer within OpenGL.
        /// </summary>
        private uint _pointer;

        /// <summary>
        /// Create a new buffer.
        /// </summary>
        /// <param name="data">The initial data to upload.</param>
        public IndexBuffer(ushort[] data)
        {
            _pointer = Gl.GenBuffer();
            Upload(data);
        }

        #region API

        /// <summary>
        /// Use this buffer for any following operations.
        /// </summary>
        public void Bind()
        {
            if (BoundPointer == _pointer) return;
            BoundPointer = _pointer;
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, _pointer);
        }

        /// <summary>
        /// Stop using any buffer.
        /// </summary>
        public void Unbind()
        {
            BoundPointer = 0;
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        /// <summary>
        /// Uploads the provided vertex data to the GPU.
        /// </summary>
        /// <param name="data">The vertex data for this VBO.</param>
        public void Upload(ushort[] data)
        {
            if (_pointer == 0) throw new Exception("Cannot upload data to a destroyed buffer.");

            Count = data.Length;
            GLThread.ExecuteGLThread(() =>
            {
                Bind();
                Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint) (data.Length * sizeof(ushort)), data, BufferUsage.StaticDraw);
                Unbind();
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