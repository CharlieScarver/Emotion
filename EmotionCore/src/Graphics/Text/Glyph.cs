﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emotion.Graphics.Text
{
    public struct Glyph
    {
        /// <summary>
        /// Coordinates within the texture atlas.
        /// </summary>
        
        public int X;
        public int Y;
        public int Width;
        public int Height;

        /// <summary>
        /// Offsets for renderings.
        /// </summary>

        public float XOffset;
        public float YOffset;
        public float Advance;
        public float YBearing;
        public float MinX;

        /// <summary>
        /// Used by string measuring
        /// </summary>

        public float MaxX;
        public float MinY;
        public float MaxY;
        public float Ascender;
    }
}
