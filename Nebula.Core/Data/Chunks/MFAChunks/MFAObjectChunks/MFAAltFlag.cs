﻿using Nebula.Core.Memory;

namespace Nebula.Core.Data.Chunks.MFAChunks
{
    public class MFAAltFlag : Chunk
    {
        public string Name = string.Empty;
        public bool Value;

        public MFAAltFlag()
        {
            ChunkName = "MFAAltFlag";
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {

        }

        public override void ReadMFA(ByteReader reader, params object[] extraInfo)
        {
            Name = reader.ReadAutoYuniversal();
            reader.Skip(4);
            Value = reader.ReadInt() == 1;
        }

        public override void WriteCCN(ByteWriter writer, params object[] extraInfo)
        {

        }

        public override void WriteMFA(ByteWriter writer, params object[] extraInfo)
        {
            writer.WriteAutoYunicode(Name);
            writer.Skip(4);
            writer.WriteInt(Value ? 1 : 0);
        }
    }
}
