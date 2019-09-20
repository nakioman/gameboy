using System;

namespace GameBoyCore.BankController
{
    /// <summary>
    /// Docs: http://bgb.bircd.org/pandocs.htm#mbc1max2mbyteromandor32kbyteram
    /// </summary>
    public class MBC1 : IMemoryMap
    {
        private readonly byte[] _rom;
        private readonly byte[] _ram;

        private int _selectedRomBank;
        private int _selectedRamBank;
        private bool _ramEnabled;
        private BankingMode _selectedMode;

        public MBC1(byte[] rom, int ramBanks)
        {
            _rom = rom;
            _ram = new byte[0x2000 * ramBanks];
            _selectedRomBank = 1;
            _selectedRamBank = 0;
            _ramEnabled = false;
            _selectedMode = BankingMode.ROM;
        }

        public bool CanRead(ushort address)
        {
            var ROMBank0 = address >= 0x0000 && address < 0x4000;
            var switchableROMBank = address >= 0x4000 && address < 0x8000;
            var switchableRAMBank = address >= 0xA000 && address < 0xC000;

            return ROMBank0 || switchableROMBank || switchableRAMBank;
        }

        public byte GetByte(ushort address)
        {
            return address switch
            {
                ushort _ when address <= 0x3FFF => _rom[address],
                ushort _ when address <= 0x7FFF => _rom[(0x4000 * _selectedRomBank) + address],
                ushort _ when address <= 0xBFFF => _ramEnabled ? _ram[(0x2000 * _selectedRamBank) + address] : (byte)0xFF,
                _ => throw new NotSupportedException($"MBC1 can't read address {address}"),
            };
        }

        public void SetByte(ushort address, byte value)
        {
            switch (address)
            {
                case ushort _ when address <= 0x1FFF: // Enable ram write
                    _ramEnabled = address == 0x0A;
                    break;
                case ushort _ when address <= 0x3FFF: // Enable rom bank write
                    _selectedRomBank = value & 0x1F; // only last 5 bits are checked
                    SanitizeSelectedRomBank();
                    break;
                case ushort _ when address <= 0x5FFF: // RAM Bank/Upper bits of ROM Bank 
                    if (_selectedMode == BankingMode.RAM)
                    {
                        _selectedRamBank = value & 0x3; //Only 2 bits are used
                    }
                    else
                    {
                        _selectedRomBank |= value & 0x3;
                        SanitizeSelectedRomBank();
                    }
                    break;
                case ushort _ when address <= 0x7FFF: //ROM/RAM Mode
                    _selectedMode = (BankingMode)(value & 0x1);
                    break;
                case ushort _ when address >= 0xa000 && address <= 0xc000 && _ramEnabled:
                    var ramAddress = (0x2000 * _selectedRamBank) + address;
                    if (ramAddress < _ram.Length)
                    {
                        _ram[ramAddress] = value;
                    }
                    break;
                default:
                    throw new NotSupportedException($"MBC1 can't write address {address}");
            }
        }

        private void SanitizeSelectedRomBank()
        {
            if (_selectedRomBank == 0x00 || _selectedRomBank == 0x20 || _selectedRomBank == 0x40 || _selectedRomBank == 0x60)
            {
                _selectedRomBank++;
            }
        }
    }
}