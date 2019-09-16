namespace GameBoyCore {
    public interface IMemoryMap
    {
        bool CanRead(ushort address);
        void SetByte(ushort address, byte value);
        byte GetByte(ushort address);
    }
}