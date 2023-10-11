﻿using SapphireD.Core.Memory;

namespace SapphireD.Core.Data.Chunks.ObjectChunks.ObjectCommon
{
    public class ObjectProperties : Chunk
    {
        public ObjectProperties()
        {
            ChunkName = "ObjectProperties";
            ChunkID = 0x4446;
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {

        }

        public override void ReadMFA(ByteReader reader, params object[] extraInfo)
        {

        }

        public override void WriteCCN(ByteWriter writer, params object[] extraInfo)
        {

        }

        public override void WriteMFA(ByteWriter writer, params object[] extraInfo)
        {

        }
    }
}
