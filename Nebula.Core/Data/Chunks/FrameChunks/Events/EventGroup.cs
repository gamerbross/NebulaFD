﻿using Nebula.Core.Memory;

namespace Nebula.Core.Data.Chunks.FrameChunks.Events.Parameters
{
    public class EventGroup : Chunk
    {
        public short Handle;
        public string Name = string.Empty;
        public string UUID = string.Empty;

        public EventGroup()
        {
            ChunkName = "EventGroup";
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {

        }

        public override void ReadMFA(ByteReader reader, params object[] extraInfo)
        {
            Handle = reader.ReadShort();
            Name = reader.ReadAutoYuniversal();
            UUID = reader.ReadYunicodeStop(75);
        }

        public override void WriteCCN(ByteWriter writer, params object[] extraInfo)
        {

        }

        public override void WriteMFA(ByteWriter writer, params object[] extraInfo)
        {

        }
    }
}
