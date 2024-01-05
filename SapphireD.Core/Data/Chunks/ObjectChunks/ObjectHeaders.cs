﻿using SapphireD.Core.Memory;
using System.Diagnostics;
using System.Drawing;

namespace SapphireD.Core.Data.Chunks.ObjectChunks
{
    public class ObjectHeaders : Chunk
    {
        public ObjectHeaders()
        {
            ChunkName = "ObjectHeaders";
            ChunkID = 0x2253;
        }

        public override void ReadCCN(ByteReader reader, params object[] extraInfo)
        {
            FrameItems newItems = new FrameItems();
            while (reader.Size() > reader.Tell())
            {
                ObjectInfo oI = new ObjectInfo();
                oI.Header.Handle = reader.ReadShort();
                oI.Header.Type = reader.ReadShort();
                oI.Header.ObjectFlags.Value = reader.ReadUShort();
                reader.Skip(2);
                oI.Header.InkEffect = reader.ReadShort();
                reader.Skip(2);
                if (oI.Header.InkEffect != 1)
                {
                    if (SapDCore.D3D == 0)
                        reader.Skip(4);
                    else
                    {
                        var b = reader.ReadByte();
                        var g = reader.ReadByte();
                        var r = reader.ReadByte();
                        oI.Header.RGBCoeff = Color.FromArgb(0, r, g, b);
                        oI.Header.BlendCoeff = (byte)(255 - reader.ReadByte());
                    }
                }
                else
                    oI.Header.InkEffectParam = reader.ReadUInt();
                newItems.Items.Add(oI.Header.Handle, oI);
            }
            SapDCore.PackageData.FrameItems = newItems;
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
