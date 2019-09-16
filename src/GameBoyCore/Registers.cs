namespace GameBoyCore
{
    public class Registers
    {
        /// <summary>
        /// Flags register
        /// </summary>
        private byte _f;

        /// <summary>
        /// Accumulator
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// General 8bit register
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// General 8bit register
        /// </summary>
        public byte C { get; set; }

        /// <summary>
        /// General 8bit register
        /// </summary>
        public byte D { get; set; }

        /// <summary>
        /// General 8bit register
        /// </summary>
        public byte E { get; set; }

        /// <summary>
        /// General 8bit register
        /// </summary>
        public byte H { get; set; }

        /// <summary>
        /// General 8bit register
        /// </summary>
        public byte L { get; set; }

        /// <summary>
        /// Accumulator &amp; Flags
        /// </summary>
        public ushort AF
        {
            get => (ushort)(A << 8 | _f);
            set
            {
                A = (byte)(value >> 8);
                _f = (byte)(value & 0xF0);
            }
        }

        /// <summary>
        /// General 16bit register
        /// </summary>
        public ushort BC
        {
            get => (ushort)(B << 8 | C);
            set
            {
                B = (byte)(value >> 8);
                C = (byte)value;
            }
        }

        /// <summary>
        /// General 16bit register
        /// </summary>
        public ushort DE
        {
            get => (ushort)(D << 8 | E);
            set
            {
                D = (byte)(value >> 8);
                E = (byte)value;
            }
        }

        /// <summary>
        /// General 16bit register
        /// </summary>
        public ushort HL
        {
            get => (ushort)(H << 8 | L);
            set
            {
                H = (byte)(value >> 8);
                L = (byte)value;
            }
        }

        /// <summary>
        /// Stack Pointer
        /// </summary>
        public ushort SP { get; set; }

        /// <summary>
        /// Program Counter/Pointer
        /// </summary>
        public ushort PC { get; set; }



        /// <summary>
        /// This bit becomes set (1) if the result of an operation has been zero (0).
        /// Used for conditional jumps.
        /// </summary>
        public bool ZeroFlag
        {
            get => (_f & 0x80) != 0;
            set
            {
                _f = value ? (byte)(_f | 0x80) : (byte)(_f & ~0x80);
            }
        }

        /// <summary>
        /// These flag is (rarely) used for the DAA instruction only.
        /// Indicates whether the previous instruction has been an addition or subtraction
        /// </summary>
        public bool BCDNFlag
        {
            get => (_f & 0x40) != 0;
            set
            {
                _f = value ? (byte)(_f | 0x40) : (byte)(_f & ~0x40);
            }
        }

        /// <summary>
        /// These flag is (rarely) used for the DAA instruction only.
        ///  Indicates carry for lower 4bits of the result, also for DAA, 
        /// the Carry flag must indicate carry for upper 8bits.
        /// </summary>
        public bool BCDHFlag
        {
            get => (_f & 0x20) != 0;
            set
            {
                _f = value ? (byte)(_f | 0x20) : (byte)(_f & ~0x20);
            }
        }

        /// <summary>
        /// Becomes set when the result of an addition became bigger than FFh (8bit) or FFFFh (16bit). 
        /// Or when the result of a subtraction or comparision became less than zero 
        /// (much as for Z80 and 80x86 CPUs, but unlike as for 65XX and ARM CPUs). 
        /// Also the flag becomes set when a rotate/shift operation has shifted-out a "1"-bit
        /// </summary>
        public bool CarryFlag
        {
            get => (_f & 0x10) != 0;
            set
            {
                _f = value ? (byte)(_f | 0x10) : (byte)(_f & ~0x10);
            }
        }

        public Registers() {
            Reset();
        }

        public void Reset() {
            AF = 0x01B0;
            BC = 0x0013;
            DE = 0x00D8;
            HL = 0x014d;
            SP = 0xFFFE;
            PC = 0x100;
        }
    }
}
