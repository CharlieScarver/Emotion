// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Emotion.Debug;
using Emotion.Game.Layering;
using Emotion.Graphics;
using Emotion.Graphics.Batching;
using Emotion.Graphics.GLES;
using Emotion.Primitives;
using Emotion.System;
using Debugger = Emotion.Debug.Debugger;
using System.Linq;

#endregion

namespace EmotionSandbox.Tests
{
    public class MappingTest : Layer
    {
        private QuadMapBuffer testBuf;

        public static void Main()
        {
            Context.Setup();
            Context.LayerManager.Add(new MappingTest(), "Emotion Test - Mapping", 1);
            // Start the context.
            Context.Run();
        }

        public override void Load()
        {
        }

        VertexArray vao;
        IndexBuffer ibo;

        private void TestCode()
        {
            //testBuf = new QuadMapBuffer(Renderer.MaxRenderable);
            //testBuf.Start();

            //for (int i = 0; i < Renderer.MaxRenderable; i++)
            //{
            //    int line = (int) Math.Floor(i / 960f);
            //    testBuf.Add(new Vector3(i - line * 960, line, 1), new Vector2(1, 1), Color.White);
            //}

            //testBuf.FinishMapping();

            // end

            // Setup buffers.
            ushort[] indices = new ushort[Renderer.MaxRenderable * 6];
            uint offset = 0;
            for (int i = 0; i < indices.Length; i += 6)
            {
                indices[i] = (ushort)(offset + 0);
                indices[i + 1] = (ushort)(offset + 1);
                indices[i + 2] = (ushort)(offset + 2);
                indices[i + 3] = (ushort)(offset + 2);
                indices[i + 4] = (ushort)(offset + 3);
                indices[i + 5] = (ushort)(offset + 0);

                offset += 4;
            }

            ibo = new IndexBuffer(indices);

            // Calculate the size of the buffer.
            int quadSize = VertexData.SizeInBytes * 4;
            int bufferSize = Renderer.MaxRenderable * quadSize;

            Emotion.Graphics.GLES.Buffer vbo = Context.Renderer.CreateBuffer(bufferSize);

            vao = new VertexArray();

            vao.Bind();
            vbo.Bind();

            Context.Renderer.MapVertexAttrib();

            vbo.Unbind();
            vao.Unbind();

            // Done.

            Stopwatch totalStopwatch = new Stopwatch();
            totalStopwatch.Start();
            Stopwatch operationStopwatch = new Stopwatch();
            operationStopwatch.Start();
            string result = "";
            int counter = 0;

            List<int> mInit = new List<int>();
            List<int> mMax = new List<int>();
            List<int> mFlush = new List<int>();
            List<int> msingleVert = new List<int>();

            List<int> mExplicitStart = new List<int>();
            List<int> mExplicitFlush = new List<int>();

            if (false)
            {
                while (counter < 10000)
                {
                    counter++;

                    // Map - Start mapping.
                    Context.Renderer.StartMappingWithMap(vbo);
                    operationStopwatch.Stop();
                    mInit.Add((int) operationStopwatch.ElapsedTicks);
                    operationStopwatch.Restart();

                    // Map - Map all quads.
                    operationStopwatch.Start();
                    int total = 0;

                    for (int y = 0; y < 540; y += 5)
                    {
                        for (int x = 0; x < 960; x += 5)
                        {
                            Context.Renderer.MapQuad(new Vector3(x, y, 1), new Vector2(5, 5), Color.White);
                            total++;
                            if (total == Renderer.MaxRenderable) break;
                        }
                        if (total == Renderer.MaxRenderable) break;
                    }

                    operationStopwatch.Stop();
                    mMax.Add((int) operationStopwatch.ElapsedTicks);
                    operationStopwatch.Restart();

                    // Map - Flush
                    operationStopwatch.Start();
                    Context.Renderer.FlushMap();
                    operationStopwatch.Stop();
                    mFlush.Add((int) operationStopwatch.ElapsedTicks);
                    operationStopwatch.Restart();

                    // Map - Remap Single Vertex
                    operationStopwatch.Start();
                    Context.Renderer.RemapIndexExplicit(vbo, 3, Color.Red, new Vector3(15, 0, 2), new Vector2(5, 5), mExplicitStart, mExplicitFlush);
                    operationStopwatch.Stop();
                    msingleVert.Add((int) operationStopwatch.ElapsedTicks);
                    operationStopwatch.Restart();

                    // Remove unjitted.
                    if (counter < 100)
                    {
                        mInit.Clear();
                        mMax.Clear();
                        mFlush.Clear();
                        msingleVert.Clear();
                        mExplicitStart.Clear();
                        mExplicitFlush.Clear();
                    }
                }
            }
            else
            {
                while (counter < 3000)
                {
                    counter++;

                    // Sub - Start mapping.
                    VertexData[] data = new VertexData[Renderer.MaxRenderable * 4];

                    // Map - All
                    int total = 0;
                    int index = 0;
                    for (int y = 0; y < 540; y += 5)
                    {
                        for (int x = 0; x < 960; x += 5)
                        {
                            index = Context.Renderer.MapQuad(new Vector3(x, y, 1), new Vector2(5, 5), Color.White, ref data, index);
                            total++;
                            if (total == Renderer.MaxRenderable) break;
                        }
                        if (total == Renderer.MaxRenderable) break;
                    }
                    operationStopwatch.Stop();
                    mInit.Add((int) operationStopwatch.ElapsedTicks);
                    operationStopwatch.Restart();

                    // Map - Flush
                    operationStopwatch.Start();
                    Context.Renderer.MapSubBuffer(vbo, data);
                    operationStopwatch.Stop();
                    mFlush.Add((int) operationStopwatch.ElapsedTicks);
                    operationStopwatch.Restart();

                    // Map - Remap Single Vertex
                    operationStopwatch.Start();
                    Context.Renderer.RemapIndex(vbo, 3, Color.Red, new Vector3(15, 0, 2), new Vector2(5, 5), true);
                    operationStopwatch.Stop();
                    msingleVert.Add((int) operationStopwatch.ElapsedTicks);
                    operationStopwatch.Restart();

                    // Remove unjitted.
                    if (counter < 100)
                    {
                        mInit.Clear();
                        mMax.Clear();
                        mFlush.Clear();
                        msingleVert.Clear();
                        mExplicitStart.Clear();
                        mExplicitFlush.Clear();
                    }

                    Console.WriteLine(counter);
                } 
            }

            result = $"| {mInit.Average()} | {mFlush.Average()} | {msingleVert.Average()} |";
            Debugger.Log(MessageType.Error, MessageSource.Other, $"{result}");

            totalStopwatch.Stop();
            Debugger.Log(MessageType.Error, MessageSource.Other, $"Total Time: {totalStopwatch.ElapsedTicks}");
        }

        public override void Update(float fr)
        {
        }

        bool first = true;

        public override void Draw(Renderer renderer)
        {
            if (first) TestCode();
            first = false;
            Context.Renderer.RenderMapped(vao, ibo, Renderer.MaxRenderable * 6);
            //Context.Renderer.Render(testBuf);
        }

        public override void Unload()
        {
        }
    }
}