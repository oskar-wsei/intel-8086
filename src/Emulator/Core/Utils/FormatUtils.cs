using System;

namespace Emulator.Core.Utils;

public class FormatUtils
{
    public static string ToBinary(byte value) => ToBinary(value, 8);

    public static string ToBinary(ushort value) => ToBinary(value, 16);

    public static string ToBinary(uint value) => ToBinary(value, 32);

    public static string ToBinary(uint value, int width)
    {
        return Convert.ToString(value, 2).PadLeft(width, '0');
    }

    public static string ToHex(byte value) => ToHex(value, 2);

    public static string ToHex(ushort value) => ToHex(value, 4);

    public static string ToHex(uint value) => ToHex(value, 8);

    public static string ToHex(uint value, int width)
    {
        return Convert.ToString(value, 16).PadLeft(width, '0');
    }
}
