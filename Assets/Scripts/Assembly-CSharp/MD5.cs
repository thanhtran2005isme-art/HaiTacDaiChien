using System;
using System.IO;

public class MD5 : IDisposable
{
	private const byte S11 = 7;

	private const byte S12 = 12;

	private const byte S13 = 17;

	private const byte S14 = 22;

	private const byte S21 = 5;

	private const byte S22 = 9;

	private const byte S23 = 14;

	private const byte S24 = 20;

	private const byte S31 = 4;

	private const byte S32 = 11;

	private const byte S33 = 16;

	private const byte S34 = 23;

	private const byte S41 = 6;

	private const byte S42 = 10;

	private const byte S43 = 15;

	private const byte S44 = 21;

	private static byte[] PADDING;

	private uint[] state;

	private uint[] count;

	private byte[] buffer;

	protected byte[] HashValue;

	protected int State;

	protected int HashSizeValue;

	public virtual bool CanReuseTransform => false;

	public virtual bool CanTransformMultipleBlocks => false;

	public virtual byte[] Hash => null;

	public virtual int HashSize => 0;

	public virtual int InputBlockSize => 0;

	public virtual int OutputBlockSize => 0;

	public static MD5 Create(string hashName)
	{
		return null;
	}

	public static byte[] GetMd5Bytes(string source)
	{
		return null;
	}

	public static string GetMd5String(string source)
	{
		return null;
	}

	public static MD5 Create()
	{
		return null;
	}

	private static uint F(uint x, uint y, uint z)
	{
		return 0u;
	}

	private static uint G(uint x, uint y, uint z)
	{
		return 0u;
	}

	private static uint H(uint x, uint y, uint z)
	{
		return 0u;
	}

	private static uint I(uint x, uint y, uint z)
	{
		return 0u;
	}

	private static uint ROTATE_LEFT(uint x, byte n)
	{
		return 0u;
	}

	private static void FF(ref uint a, uint b, uint c, uint d, uint x, byte s, uint ac)
	{
	}

	private static void GG(ref uint a, uint b, uint c, uint d, uint x, byte s, uint ac)
	{
	}

	private static void HH(ref uint a, uint b, uint c, uint d, uint x, byte s, uint ac)
	{
	}

	private static void II(ref uint a, uint b, uint c, uint d, uint x, byte s, uint ac)
	{
	}

	internal MD5()
	{
	}

	public virtual void Initialize()
	{
	}

	protected virtual void HashCore(byte[] input, int offset, int count)
	{
	}

	protected virtual byte[] HashFinal()
	{
		return null;
	}

	private void Transform(byte[] block, int offset)
	{
	}

	private static void Encode(byte[] output, int outputOffset, uint[] input, int inputOffset, int count)
	{
	}

	private static void Decode(uint[] output, int outputOffset, byte[] input, int inputOffset, int count)
	{
	}

	public void Clear()
	{
	}

	public byte[] ComputeHash(byte[] buffer)
	{
		return null;
	}

	public byte[] ComputeHash(byte[] buffer, int offset, int count)
	{
		return null;
	}

	public byte[] ComputeHash(Stream inputStream)
	{
		return null;
	}

	public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
	{
		return 0;
	}

	public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
	{
		return null;
	}

	protected virtual void Dispose(bool disposing)
	{
	}

	public void Dispose()
	{
	}
}
