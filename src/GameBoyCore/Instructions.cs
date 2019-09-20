using System;
using System.Collections.Generic;

namespace GameBoyCore
{
    public static class Instructions
    {
        private static readonly IDictionary<byte, Instruction> _instructions = new Dictionary<byte, Instruction>();

        public static ushort Execute(byte opCode, Registers registers, IMemoryMap memory)
        {
            var instr = Get(opCode);
            return instr.Execute(registers, memory);
        }

        static Instructions()
        {
            var instructionBuilders = new List<InstructionBuilder>();

            BuildMiscControlInstructions(instructionBuilders);
            BuildJumpCallsInstructions(instructionBuilders);
            BuildLoadStoreMoveInstructions(instructionBuilders);
            BuildArithmeticInstructions(instructionBuilders);
            BuildRotationShiftInstructions(instructionBuilders);

            foreach (var builder in instructionBuilders)
            {
                var inst = builder.Build();
                _instructions.Add(inst.Opcode, inst);
            }
        }

        private static IDictionary<byte, IOperand> BuildDicc(byte start, byte step, IEnumerable<IOperand> values)
        {
            var dicc = new Dictionary<byte, IOperand>();
            var opCode = start;
            foreach (var value in values)
            {
                dicc.Add(opCode, value);
                opCode += step;
            }
            return dicc;
        }

        private static InstructionBuilder Add(this IList<InstructionBuilder> list, byte opCode, string label)
        {
            var builder = InstructionBuilder.Create(opCode, label);
            list.Add(builder);

            return builder;
        }

        private static Instruction Get(byte opCode)
        {
            if (!_instructions.ContainsKey(opCode))
            {
                throw new NotImplementedException($"The instruction with opCode 0x{opCode:X} is not implemented");
            }
            return _instructions[opCode];
        }

        private static void BuildMiscControlInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            instructionBuilders.Add(0x00, "NOP");
        }

        private static void BuildJumpCallsInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            throw new NotImplementedException();
        }

        private static void BuildLoadStoreMoveInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            Build8BitLoadStoreMoveInstructions(instructionBuilders);
            Build16BitLoadStoreMoveInstructions(instructionBuilders);
        }

        private static void Build8BitLoadStoreMoveInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            throw new NotImplementedException();
        }

        private static void Build16BitLoadStoreMoveInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            foreach (var keyValue in BuildDicc(0x01, 0x10, new List<IOperand> { Operands.BC }))
            {
                instructionBuilders.Add(keyValue.Key, "LD").CopyWord(Operands.d16, keyValue.Value);
            }
        }

        private static void BuildArithmeticInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            Build8BitArithmeticInstructions(instructionBuilders);
            Build16BitArithmeticInstructions(instructionBuilders);
        }

        private static void Build8BitArithmeticInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            throw new NotImplementedException();
        }

        private static void Build16BitArithmeticInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            throw new NotImplementedException();
        }

        private static void BuildRotationShiftInstructions(IList<InstructionBuilder> instructionBuilders)
        {
            throw new NotImplementedException();
        }
    }
}