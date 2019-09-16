using System;

namespace GameBoyCore
{
    public class MemoryManager : IMemoryMap
    {
        private readonly IMemoryMap _cartridge;

        public MemoryManager(IMemoryMap cartridge)
        {
            _cartridge = cartridge;
        }

        public bool CanRead(ushort address)
        {
            return true;
        }

        public byte GetByte(ushort address)
        {
            if (_cartridge.CanRead(address)) return _cartridge.GetByte(address);
            throw new NotSupportedException($"The memory address {address} can't be read");
        }

        public void SetByte(ushort address, byte value)
        {
            if (_cartridge.CanRead(address)) _cartridge.SetByte(address, value);
            throw new NotSupportedException($"The memory address {address} can't be written");
        }
    }
}