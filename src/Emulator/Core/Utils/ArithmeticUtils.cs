namespace Emulator.Core.Utils;

public class ArithmeticUtils
{
    public static ushort SignExtend(byte a)
    {
        return IsSignSet(a) ? (ushort)((0xff << 8) | a) : a;
    }

    public static uint SignExtend(ushort a)
    {
        return IsSignSet(a) ? (uint)((0xffff << 16) | a) : a;
    }

    public static ushort AddSigned(ushort a, byte b)
    {
        return (ushort)(a + SignExtend(b));
    }

    public static bool IsSignSet(byte a)
    {
        return (a & 0x80) != 0;
    }

    public static bool IsSignSet(ushort a)
    {
        return (a & 0x8000) != 0;
    }

    public static byte Negate(byte a)
    {
        return (byte)(~a + 1);
    }

    public static ushort Negate(ushort a)
    {
        return (ushort)(~a + 1);
    }

    public static uint CountSetBits(uint value)
    {
        uint count = 0;

        while (value != 0)
        {
            count += value & 0x1;
            value >>= 1;
        }

        return count;
    }
}
