namespace GameBoyCore
{
    public interface IOperand
    {
        string Label { get; }
        byte ReadByte(Registers registers, IMemoryMap memorySpace);
        byte WriteByte(Registers registers, IMemoryMap memorySpace, byte value);
        ushort ReadWord(Registers registers, IMemoryMap memorySpace);
        ushort WriteWord(Registers registers, IMemoryMap memorySpace, ushort value);

    }
}