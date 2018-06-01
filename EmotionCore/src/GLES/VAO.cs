// Emotion - https://github.com/Cryru/Emotion

#region Using

using OpenTK.Graphics.ES30;

#endregion

namespace Emotion.GLES
{
    /// <summary>
    /// A Vertex Array Object (VAO) is an OpenGL Object that stores all of the state needed to supply vertex data.
    /// </summary>
    public class VAO
    {
        internal int Pointer;

        /// <summary>
        /// Create a new VAO.
        /// </summary>
        public VAO()
        {
            // Generate a new vertex array object.
            GL.GenVertexArrays(1, out Pointer);
        }

        public void Use()
        {
            GL.BindVertexArray(Pointer);
        }

        public void Destroy()
        {
            GL.DeleteVertexArrays(1, ref Pointer);
        }

        public void StopUsing()
        {
            GL.BindVertexArray(0);
        }
    }
}