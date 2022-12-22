using System;
using System.Collections.Generic;
using Emulator.Core.Components.Memory;
using Emulator.Core.Components.Registers;
using Emulator.Core.Instructions;
using Emulator.Core.Utils;

namespace Emulator.Core;

public class VirtualMachine
{
    public readonly MemoryComponent Memory = new(0x10000);
    public readonly RegistersComponent Registers = new();
    public readonly Dictionary<byte, Action<byte>> Instructions = new();

    public bool Halted { get; protected set; }

    public VirtualMachine()
    {
        new InstructionRegistry(this).RegisterInstructions();
    }

    public void Halt()
    {
        Halted = true;
    }

    public void Reset()
    {
        Registers.Reset();
        Memory.Reset();
        Halted = false;
    }

    public void LoadProgram(byte[] program)
    {
        for (uint i = 0; i < program.Length; i++)
        {
            Memory.SetByte(i, program[i]);
        }
    }

    public bool Step()
    {
        if (Halted) return false;

        byte opcode = NextByte();

        if (!Instructions.ContainsKey(opcode))
        {
            Halted = true;
            throw new InvalidInstructionException(opcode);
        }

        var handler = Instructions[opcode];
        handler(opcode);

        return !Halted;
    }

    public void RegisterInstruction(byte code, Action<byte> handler)
    {
        if (Instructions.ContainsKey(code))
            throw new EmulationException($"Instruction with code {FormatUtils.ToHex(code)} has been already registered!");

        Instructions.Add(code, handler);
    }

    public byte NextByte()
    {
        return Memory.GetByte(Registers.InstructionPointer.Value++);
    }

    public ushort NextWord()
    {
        ushort value = Memory.GetWord(Registers.InstructionPointer.Value);
        Registers.InstructionPointer.Value += 2;
        return value;
    }

    public void CheckZeroFlag(ushort value)
    {
        Registers.Flags.SetZero(value == 0);
    }

    public void CheckParityFlag(ushort value)
    {
        Registers.Flags.SetParity(ArithmeticUtils.CountSetBits(value) % 2 == 0);
    }

    public void CheckSignFlag(byte value)
    {
        Registers.Flags.SetSign(ArithmeticUtils.IsSignSet(value));
    }

    public void CheckSignFlag(ushort value)
    {
        Registers.Flags.SetSign(ArithmeticUtils.IsSignSet(value));
    }

    public void CheckValueFlags(byte value)
    {
        CheckZeroFlag(value);
        CheckParityFlag(value);
        CheckSignFlag(value);
    }

    public void CheckValueFlags(ushort value)
    {
        CheckZeroFlag(value);
        CheckParityFlag(value);
        CheckSignFlag(value);
    }
}
