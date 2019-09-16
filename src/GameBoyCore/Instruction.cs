using System.Collections.Generic;
using System;

namespace GameBoyCore
{
    /// <summary>
    /// CPU Instruction
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Mnemonic code
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// Instruction opcode bytes
        /// </summary>
        public byte Opcode { get; protected set; }

        public Queue<IOperand> Ops {get; set; }

        public Func<Registers, IMemoryMap, ushort> Execute;

        public Instruction(byte opCode, string label) {
            Opcode = opCode;
            Label = label;
            Ops = new Queue<IOperand>();
        }
    }
}