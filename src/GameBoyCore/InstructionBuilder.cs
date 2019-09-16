namespace GameBoyCore
{
    public class InstructionBuilder
    {
        private readonly Instruction _instruction;

        private InstructionBuilder(byte opCode, string label)
        {
            _instruction = new Instruction(opCode, label)
            {
                Execute = (regs, mem) => 0
            };
        }

        public static InstructionBuilder Create(byte opCode, string label)
        {
            return new InstructionBuilder(opCode, label);
        }

        public InstructionBuilder CopyWord(IOperand source, IOperand target) {
            _instruction.Execute = (regs, mem) => {
                var word = source.ReadWord(regs, mem);
                target.WriteWord(regs, mem, word);
                
                return word;
            };

            return this;
        }

        public Instruction Build() {
            return _instruction;
        }
    }
}
