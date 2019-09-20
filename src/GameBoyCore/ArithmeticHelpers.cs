namespace GameBoyCore
{
    public static class ArithmeticHelpers
    {
        public static ushort GetWord(this IMemoryMap memory, ushort address)
        {
            var loAddr = (ushort)(address + 1);
            var loVal = memory.GetByte(loAddr);
            return (ushort)(loVal << 8 | memory.GetByte(address));
        }
    }
}