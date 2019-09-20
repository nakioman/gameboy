using Dynamitey;
using ImpromptuInterface;

namespace GameBoyCore
{
    public static class Operands
    {
        public static IOperand BC = new
        {
            Label = "BC",
            ReadWord = Return<ushort>.Arguments<Registers, IMemoryMap>((regs, mem) => regs.BC),
            WriteWord = Return<ushort>.Arguments<Registers, IMemoryMap, ushort>((regs, mem, val) => regs.BC = val)
        }.ActLike<IOperand>();

        public static IOperand d16 = new
        {
            Label = "d16",
            ReadWord = Return<ushort>.Arguments<Registers, IMemoryMap>((regs, mem) =>
            {
                var val = mem.GetWord(regs.PC);
                regs.PC += 2;
                return val;
            }),
        }.ActLike<IOperand>();
    }
}