using System;
using System.IO;
using System.Text;

public class BPack : IDisposable
{
    private MemoryStream buf;
    private BinaryWriter writer;
    private BinaryReader reader;
    private int readOffset;
    private int writeOffset;

    public BPack(int size)
    {
        buf = new MemoryStream(size);
        writer = new BinaryWriter(buf);
        reader = new BinaryReader(buf);
    }

    public BPack(byte[] buffer)
    {
        buf = new MemoryStream(buffer);
        writer = new BinaryWriter(buf);
        reader = new BinaryReader(buf);
    }

    // ===== WRITE METHODS =====
    public void WriteInt32(int val)
    {
        writer.Write(val);
        writeOffset += 4;
    }

    public void WriteUInt32(uint val)
    {
        writer.Write(val);
        writeOffset += 4;
    }

    public void WriteInt16(short val)
    {
        writer.Write(val);
        writeOffset += 2;
    }

    public void WriteUInt16(ushort val)
    {
        writer.Write(val);
        writeOffset += 2;
    }

    public void WriteByte(byte val)
    {
        writer.Write(val);
        writeOffset += 1;
    }

    public void WriteFloat(float val)
    {
        writer.Write(val);
        writeOffset += 4;
    }

    public void WriteDouble(double val)
    {
        writer.Write(val);
        writeOffset += 8;
    }

    public void WriteString(string val)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(val);
        WriteInt32(bytes.Length);
        writer.Write(bytes);
        writeOffset += bytes.Length;
    }

    public void WriteBuffer(byte[] buffer)
    {
        WriteInt32(buffer.Length);
        writer.Write(buffer);
        writeOffset += buffer.Length;
    }

    // ===== WRITE ARRAY METHODS =====
    public void WriteInt32Array(int[] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteInt32(val);
    }

    public void WriteUInt32Array(uint[] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteUInt32(val);
    }

    public void WriteInt16Array(short[] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteInt16(val);
    }

    public void WriteUInt16Array(ushort[] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteUInt16(val);
    }

    public void WriteByteArray(byte[] arr)
    {
        WriteInt32(arr.Length);
        writer.Write(arr);
        writeOffset += arr.Length;
    }

    public void WriteFloatArray(float[] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteFloat(val);
    }

    public void WriteDoubleArray(double[] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteDouble(val);
    }

    public void WriteStringArray(string[] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteString(val);
    }

    public void WriteBufferArray(byte[][] arr)
    {
        WriteInt32(arr.Length);
        foreach (var val in arr)
            WriteBuffer(val);
    }

    // ===== READ METHODS =====
    public int ReadInt32()
    {
        int val = reader.ReadInt32();
        readOffset += 4;
        return val;
    }

    public uint ReadUInt32()
    {
        uint val = reader.ReadUInt32();
        readOffset += 4;
        return val;
    }

    public short ReadInt16()
    {
        short val = reader.ReadInt16();
        readOffset += 2;
        return val;
    }

    public ushort ReadUInt16()
    {
        ushort val = reader.ReadUInt16();
        readOffset += 2;
        return val;
    }

    public byte ReadByte()
    {
        byte val = reader.ReadByte();
        readOffset += 1;
        return val;
    }

    public float ReadFloat()
    {
        float val = reader.ReadSingle();
        readOffset += 4;
        return val;
    }

    public double ReadDouble()
    {
        double val = reader.ReadDouble();
        readOffset += 8;
        return val;
    }

    public string ReadString()
    {
        int length = ReadInt32();
        byte[] bytes = reader.ReadBytes(length);
        readOffset += length;
        return Encoding.UTF8.GetString(bytes);
    }

    public byte[] ReadBuffer()
    {
        int length = ReadInt32();
        byte[] buffer = reader.ReadBytes(length);
        readOffset += length;
        return buffer;
    }

    // ===== READ ARRAY METHODS =====
    public int[] ReadInt32Array()
    {
        int length = ReadInt32();
        int[] arr = new int[length];
        for (int i = 0; i < length; i++)
            arr[i] = ReadInt32();
        return arr;
    }

    public uint[] ReadUInt32Array()
    {
        int length = ReadInt32();
        uint[] arr = new uint[length];
        for (int i = 0; i < length; i++)
            arr[i] = ReadUInt32();
        return arr;
    }

    public short[] ReadInt16Array()
    {
        int length = ReadInt32();
        short[] arr = new short[length];
        for (int i = 0; i < length; i++)
            arr[i] = ReadInt16();
        return arr;
    }

    public ushort[] ReadUInt16Array()
    {
        int length = ReadInt32();
        ushort[] arr = new ushort[length];
        for (int i = 0; i < length; i++)
            arr[i] = ReadUInt16();
        return arr;
    }

    public byte[] ReadByteArray()
    {
        int length = ReadInt32();
        byte[] arr = reader.ReadBytes(length);
        readOffset += length;
        return arr;
    }

    public float[] ReadFloatArray()
    {
        int length = ReadInt32();
        float[] arr = new float[length];
        for (int i = 0; i < length; i++)
            arr[i] = ReadFloat();
        return arr;
    }

    public double[] ReadDoubleArray()
    {
        int length = ReadInt32();
        double[] arr = new double[length];
        for (int i = 0; i < length; i++)
            arr[i] = ReadDouble();
        return arr;
    }

    public string[] ReadStringArray()
    {
        int length = ReadInt32();
        string[] arr = new string[length];
        for (int i = 0; i < length; i++)
            arr[i] = ReadString();
        return arr;
    }

    public byte[][] ReadBufferArray()
    {
        int length = ReadInt32();
        byte[][] arr = new byte[length][];
        for (int i = 0; i < length; i++)
            arr[i] = ReadBuffer();
        return arr;
    }

    // ===== UTILITY METHODS =====
    public byte[] GetBuffer()
    {
        writer.Flush();
        return buf.ToArray();
    }

    public void ResetRead()
    {
        buf.Seek(0, SeekOrigin.Begin);
        readOffset = 0;
    }

    public void Dispose()
    {
        writer?.Dispose();
        reader?.Dispose();
        buf?.Dispose();
    }
}
