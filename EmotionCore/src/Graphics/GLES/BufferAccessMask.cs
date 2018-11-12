// Emotion - https://github.com/Cryru/Emotion

#region Using

using System;

#endregion

namespace Emotion.Graphics.GLES
{
    [Flags]
    public enum BufferAccessMask
    {
        MapReadBit = 0x0001,
        MapWriteBit = 0x0002,
        MapInvalidateRangeBit = 0x0004,
        MapInvalidateBufferBit = 0x0008,
        MapFlushExplicitBit = 0x0010,
        MapUnsynchronizedBit = 0x0020
    }
}