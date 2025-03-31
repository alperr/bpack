using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class BPack : IDisposable
{
    private MemoryStream buf;
    private BinaryWriter writer;
    private BinaryReader reader;
    private int offset;

    public BPack(int size)
    {
        buf = new MemoryStream(size);
        writer = new BinaryWriter(buf);
        reader = new BinaryReader(buf);
        offset = 0;
    }

    public BPack(byte[] buffer)
    {
        buf = new MemoryStream(buffer);
        writer = new BinaryWriter(buf);
        reader = new BinaryReader(buf);
        offset = 0;
    }

    public void WriteInt32(int val)
    {
        writer.Write(val);
        offset += 4;
    }

    public int ReadInt32(byte[] buffer, int offset)
    {
        return BitConverter.ToInt32(buffer, offset);
    }

    public void WriteUInt32(uint val)
    {
        writer.Write(val);
        offset += 4;
    }

    public uint ReadUInt32(byte[] buffer, int offset)
    {
        return BitConverter.ToUInt32(buffer, offset);
    }

    public void WriteInt16(short val)
    {
        writer.Write(val);
        offset += 2;
    }

    public short ReadInt16(byte[] buffer, int offset)
    {
        return BitConverter.ToInt16(buffer, offset);
    }

    public void WriteUInt16(ushort val)
    {
        writer.Write(val);
        offset += 2;
    }

    public ushort ReadUInt16(byte[] buffer, int offset)
    {
        return BitConverter.ToUInt16(buffer, offset);
    }

    public void WriteUInt8(byte val)
    {
        writer.Write(val);
        offset += 1;
    }

    public byte ReadUInt8(byte[] buffer, int offset)
    {
        return buffer[offset];
    }

    public void WriteFloat(float val)
    {
        writer.Write(val);
        offset += 4;
    }

    public float ReadFloat(byte[] buffer, int offset)
    {
        return BitConverter.ToSingle(buffer, offset);
    }

    public void WriteDouble(double val)
    {
        writer.Write(val);
        offset += 8;
    }

    public double ReadDouble(byte[] buffer, int offset)
    {
        return BitConverter.ToDouble(buffer, offset);
    }

    public void WriteString(string val)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(val);
        WriteInt32(bytes.Length);
        writer.Write(bytes);
        offset += bytes.Length;
    }

    public string ReadString(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        return Encoding.UTF8.GetString(buffer, offset, len);
    }

    public void WriteBuffer(byte[] buffer)
    {
        WriteInt32(buffer.Length);
        writer.Write(buffer);
        offset += buffer.Length;
    }

    public byte[] ReadBuffer(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        byte[] result = new byte[len];
        Array.Copy(buffer, offset, result, 0, len);
        return result;
    }

    public void WriteBufferArray(byte[][] arr)
    {
        WriteInt32(arr.Length);
        foreach (byte[] buffer in arr)
        {
            WriteBuffer(buffer);
        }
    }

    public byte[][] ReadBufferArray(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        byte[][] result = new byte[len][];
        for (int i = 0; i < len; i++)
        {
            result[i] = ReadBuffer(buffer, offset);
            offset += result[i].Length + 4;
        }
        return result;
    }

    public void WriteInt32Array(int[] arr)
    {
        WriteInt32(arr.Length);
        foreach (int val in arr)
        {
            WriteInt32(val);
        }
    }

    public int[] ReadInt32Array(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        int[] result = new int[len];
        for (int i = 0; i < len; i++)
        {
            result[i] = BitConverter.ToInt32(buffer, offset);
            offset += 4;
        }
        return result;
    }

    public void WriteUInt32Array(uint[] arr)
    {
        WriteInt32(arr.Length);
        foreach (uint val in arr)
        {
            WriteUInt32(val);
        }
    }

    public uint[] ReadUInt32Array(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        uint[] result = new uint[len];
        for (int i = 0; i < len; i++)
        {
            result[i] = BitConverter.ToUInt32(buffer, offset);
            offset += 4;
        }
        return result;
    }

    public void WriteInt16Array(short[] arr)
    {
        WriteInt32(arr.Length);
        foreach (short val in arr)
        {
            WriteInt16(val);
        }
    }

    public short[] ReadInt16Array(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        short[] result = new short[len];
        for (int i = 0; i < len; i++)
        {
            result[i] = BitConverter.ToInt16(buffer, offset);
            offset += 2;
        }
        return result;
    }

    public void WriteUInt16Array(ushort[] arr)
    {
        WriteInt32(arr.Length);
        foreach (ushort val in arr)
        {
            WriteUInt16(val);
        }
    }

    public ushort[] ReadUInt16Array(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        ushort[] result = new ushort[len];
        for (int i = 0; i < len; i++)
        {
            result[i] = BitConverter.ToUInt16(buffer, offset);
            offset += 2;
        }
        return result;
    }

    public void WriteUInt8Array(byte[] arr)
    {
        WriteInt32(arr.Length);
        foreach (byte val in arr)
        {
            WriteUInt8(val);
        }
    }

    public byte[] ReadUInt8Array(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        byte[] result = new byte[len];
        for (int i = 0; i < len; i++)
        {
            result[i] = buffer[offset];
            offset += 1;
        }
        return result;
    }

    public void WriteFloatArray(float[] arr)
    {
        WriteInt32(arr.Length);
        foreach (float val in arr)
        {
            WriteFloat(val);
        }
    }

    public float[] ReadFloatArray(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        float[] result = new float[len];
        for (int i = 0; i < len; i++)
        {
            result[i] = BitConverter.ToSingle(buffer, offset);
            offset += 4;
        }
        return result;
    }

    public void WriteDoubleArray(double[] arr)
    {
        WriteInt32(arr.Length);
        foreach (double val in arr)
        {
            WriteDouble(val);
        }
    }

    public double[] ReadDoubleArray(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        double[] result = new double[len];
        for (int i = 0; i < len; i++)
        {
            result[i] = BitConverter.ToDouble(buffer, offset);
            offset += 8;
        }
        return result;
    }

    public void WriteStringArray(string[] arr)
    {
        WriteInt32(arr.Length);
        foreach (string val in arr)
        {
            WriteString(val);
        }
    }

    public string[] ReadStringArray(byte[] buffer, int offset)
    {
        int len = BitConverter.ToInt32(buffer, offset);
        offset += 4;
        string[] result = new string[len];
        for (int i = 0; i < len; i++)
        {
            int strLen = BitConverter.ToInt32(buffer, offset);
            offset += 4;
            result[i] = Encoding.UTF8.GetString(buffer, offset, strLen);
            offset += strLen;
        }
        return result;
    }

    public byte[] GetBuffer()
    {
        writer.Flush();
        return buf.ToArray();
    }

    public void Dispose()
    {
        writer?.Dispose();
        reader?.Dispose();
        buf?.Dispose();
    }
}
