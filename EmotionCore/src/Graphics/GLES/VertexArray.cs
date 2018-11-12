// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using System.Collections.Generic;
using Emotion.Engine.Threading;
using OpenGL;

#endregion

namespace Emotion.Graphics.GLES
{
    public sealed class VertexArray : IGLObject
    {
        private List<Buffer> _buffers = new List<Buffer>();

        /// <summary>
        /// The pointer of the vertex array.
        /// </summary>
        private uint _pointer;

        public VertexArray()
        {
            _pointer = Gl.GenVertexArray();
        }

        /// <summary>
        /// Attach a buffer to the vertex array.
        /// </summary>
        /// <param name="buffer">The buffer to attach to the vertex array.</param>
        /// <param name="index">The shader index of the buffer.</param>
        /// <param name="stride">The byte offset between consecutive generic vertex attributes</param>
        /// <param name="offset">The offset of the first piece of data.</param>
        /// <param name="componentCount">
        /// The component count of the buffer. If under or equal to 0 the component count within the
        /// buffer object will be used.
        /// </param>
        /// <param name="byteType">The type of data within the buffer. Float by default.</param>
        /// <param name="normalized">Whether the value is normalized.</param>
        public void AttachBuffer(Buffer buffer, uint index, int stride = 0, int offset = 0, int componentCount = -1, VertexAttribType byteType = VertexAttribType.Float,
            bool normalized = false)
        {
            if (_pointer == 0) throw new Exception("Cannot add a buffer to a destroyed array.");
            if (componentCount <= 0) componentCount = (int) buffer.ComponentCount;

            GLThread.ExecuteGLThread(() =>
            {
                Bind();
                buffer.Bind();
                Gl.EnableVertexAttribArray(index);
                Gl.VertexAttribPointer(index, componentCount, byteType, normalized, stride, offset);
                buffer.Unbind();
                Unbind();

                _buffers.Add(buffer);
            });
        }

        /// <summary>
        /// Use the buffers attached to this vertex array.
        /// </summary>
        public void Bind()
        {
            Gl.BindVertexArray(_pointer);
        }

        /// <summary>
        /// Stop using any vertex array.
        /// </summary>
        public void Unbind()
        {
            Gl.BindVertexArray(0);
        }

        /// <summary>
        /// Delete the array, freeing memory. Deletes attached buffers.
        /// </summary>
        public void Delete()
        {
            Gl.DeleteVertexArrays(_pointer);
            _pointer = 0;

            foreach (Buffer b in _buffers)
            {
                b.Delete();
            }

            _buffers.Clear();
            _buffers = null;
        }
    }
}