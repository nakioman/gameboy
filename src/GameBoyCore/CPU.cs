using System.Collections.Generic;
using System;

namespace GameBoyCore
{
    public class CPU
    {
        private readonly Registers _registers;
        private readonly IMemoryMap _mmu;

        public CPU(IMemoryMap mmu)
        {
            _registers = new Registers();
            _mmu = mmu;
        }

        public void Tick()
        {
            var opCode = _mmu.GetByte(_registers.PC++);
            Instructions.Execute(opCode, _registers, _mmu);
        }
    }
}