using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace application
{
  [ComVisible(true)]
  [Serializable]
  public class MalformedBinaryWriter : IDisposable
  {
    public static readonly MalformedBinaryWriter Null = new MalformedBinaryWriter();
    protected Stream OutStream;
    private byte[] _buffer;
    private Encoding _encoding;
    private Encoder _encoder;
    [OptionalField]
    private bool _leaveOpen;
    [OptionalField]
    private char[] _tmpOneCharBuffer;
    private byte[] _largeByteBuffer;
    private int _maxChars;
    private const int LargeByteBufferSize = 256;

    protected MalformedBinaryWriter()
    {
      this.OutStream = Stream.Null;
      this._buffer = new byte[16];
      this._encoding = (Encoding) new UTF8Encoding(false, true);
      this._encoder = this._encoding.GetEncoder();
    }

    public MalformedBinaryWriter(Stream output)
      : this(output, (Encoding) new UTF8Encoding(false, true), false)
    {
    }

    public MalformedBinaryWriter(Stream output, Encoding encoding)
      : this(output, encoding, false)
    {
    }

    public MalformedBinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
    {
      if (output == null)
        throw new ArgumentNullException(nameof (output));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      if (!output.CanWrite)
        throw new ArgumentException("Stream was not writable.");
      this.OutStream = output;
      this._buffer = new byte[16];
      this._encoding = encoding;
      this._encoder = this._encoding.GetEncoder();
      this._leaveOpen = leaveOpen;
    }

    public virtual void Close()
    {
      this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      if (this._leaveOpen)
        this.OutStream.Flush();
      else
        this.OutStream.Close();
    }

    public void Dispose()
    {
      this.Dispose(true);
    }

    public virtual Stream BaseStream
    {
      get
      {
        this.Flush();
        return this.OutStream;
      }
    }

    public virtual void Flush()
    {
      this.OutStream.Flush();
    }

    public virtual long Seek(int offset, SeekOrigin origin)
    {
      return this.OutStream.Seek((long) offset, origin);
    }

    public virtual void Write(bool value)
    {
      this._buffer[0] = value ? (byte) 1 : (byte) 0;
      this.OutStream.Write(this._buffer, 0, 1);
    }

    public virtual void Write(byte value)
    {
      this.OutStream.WriteByte(value);
    }

    [CLSCompliant(false)]
    public virtual void Write(sbyte value)
    {
      this.OutStream.WriteByte((byte) value);
    }

    public virtual void Write(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      this.OutStream.Write(buffer, 0, buffer.Length);
    }

    public virtual void Write(byte[] buffer, int index, int count)
    {
      this.OutStream.Write(buffer, index, count);
    }

    [SecuritySafeCritical]
    public virtual unsafe void Write(char ch)
    {
      if (char.IsSurrogate(ch))
        throw new ArgumentException("Unicode surrogate characters must be written out as pairs together in the same call, not individually. Consider passing in a character array instead.");
      int bytes1;
      fixed (byte* bytes2 = this._buffer)
        bytes1 = this._encoder.GetBytes(&ch, 1, bytes2, this._buffer.Length, true);
      this.OutStream.Write(this._buffer, 0, bytes1);
    }

    public virtual void Write(char[] chars)
    {
      if (chars == null)
        throw new ArgumentNullException(nameof (chars));
      byte[] bytes = this._encoding.GetBytes(chars, 0, chars.Length);
      this.OutStream.Write(bytes, 0, bytes.Length);
    }

    public virtual void Write(char[] chars, int index, int count)
    {
      byte[] bytes = this._encoding.GetBytes(chars, index, count);
      this.OutStream.Write(bytes, 0, bytes.Length);
    }

    public virtual void Write(short value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) ((uint) value >> 8);
      this.OutStream.Write(this._buffer, 0, 2);
    }

    [CLSCompliant(false)]
    public virtual void Write(ushort value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) ((uint) value >> 8);
      this.OutStream.Write(this._buffer, 0, 2);
    }

    public virtual void Write(int value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    [CLSCompliant(false)]
    public virtual void Write(uint value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this.OutStream.Write(this._buffer, 0, 4);
    }

    public virtual void Write(long value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this._buffer[4] = (byte) (value >> 32);
      this._buffer[5] = (byte) (value >> 40);
      this._buffer[6] = (byte) (value >> 48);
      this._buffer[7] = (byte) (value >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    [CLSCompliant(false)]
    public virtual void Write(ulong value)
    {
      this._buffer[0] = (byte) value;
      this._buffer[1] = (byte) (value >> 8);
      this._buffer[2] = (byte) (value >> 16);
      this._buffer[3] = (byte) (value >> 24);
      this._buffer[4] = (byte) (value >> 32);
      this._buffer[5] = (byte) (value >> 40);
      this._buffer[6] = (byte) (value >> 48);
      this._buffer[7] = (byte) (value >> 56);
      this.OutStream.Write(this._buffer, 0, 8);
    }

    [SecuritySafeCritical]
    public virtual unsafe void Write(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      int byteCount = this._encoding.GetByteCount(value);
//      byteCount = 600909900;
      this.Write7BitEncodedInt(byteCount);
      if (this._largeByteBuffer == null)
      {
        this._largeByteBuffer = new byte[256];
        this._maxChars        = this._largeByteBuffer.Length / this._encoding.GetMaxByteCount(1);
      }
      if (byteCount <= this._largeByteBuffer.Length)
      {
        this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
        this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
      }
      else
      {
        int num = 0;
        int charCount;
        for (int length = value.Length; length > 0; length -= charCount)
        {
          charCount = length > this._maxChars ? this._maxChars : length;
          if (num < 0 || charCount < 0 || checked (num + charCount) > value.Length)
            throw new ArgumentOutOfRangeException("charCount");
          int bytes;
          fixed (char* ptr = value)
          {
            fixed (byte* largeByteBuffer = this._largeByteBuffer)
            {
              bytes = this._encoder.GetBytes(ptr + num, charCount, largeByteBuffer, 256, charCount == length);
            }
          }
          this.OutStream.Write(this._largeByteBuffer, 0, bytes);
          num += charCount;
        }
      }
    }
    
    [SecuritySafeCritical]
    public virtual unsafe void WriteBadString(string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      int byteCount = this._encoding.GetByteCount(value);
      Console.WriteLine("It should have sent this many bytes: " + byteCount.ToString());
      this.Write7BitEncodedInt(222223);
      if (this._largeByteBuffer == null)
      {
        this._largeByteBuffer = new byte[256];
        this._maxChars        = this._largeByteBuffer.Length / this._encoding.GetMaxByteCount(1);
      }
      if (byteCount <= this._largeByteBuffer.Length)
      {
        this._encoding.GetBytes(value, 0, value.Length, this._largeByteBuffer, 0);
        this.OutStream.Write(this._largeByteBuffer, 0, byteCount);
      }
      else
      {
        int num = 0;
        int charCount;
        for (int length = value.Length; length > 0; length -= charCount)
        {
          charCount = length > this._maxChars ? this._maxChars : length;
          if (num < 0 || charCount < 0 || checked (num + charCount) > value.Length)
            throw new ArgumentOutOfRangeException("charCount");
          int bytes;
          fixed (char* ptr = value)
          {
            fixed (byte* largeByteBuffer = this._largeByteBuffer)
            {
              bytes = this._encoder.GetBytes(ptr + num, charCount+50, largeByteBuffer, 256, charCount == length);
            }
          }
          this.OutStream.Write(this._largeByteBuffer, 0, bytes + 125);
          num += charCount;
        }
      }
    }

    protected void Write7BitEncodedInt(int value)
    {
      uint num;
      for (num = (uint) value; num >= 128u; num >>= 7)
        this.Write((byte) (num | 128u));
      this.Write((byte) num);
    }
  }
}
