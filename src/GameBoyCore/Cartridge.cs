using System;
using System.IO;
using System.Text;
using GameBoyCore.BankController;

namespace GameBoyCore
{
    public class Cartdrige : IMemoryMap
    {
        public string Title { get; }
        public CartridgeType Type { get; }

        private readonly IMemoryMap _bankController;

        public Cartdrige(string fileName)
        {
            var rom = File.ReadAllBytes(fileName);
            Title = GetTitle(rom);
            Type = GetType(rom);
            var ramBanks = GetRamBanks(rom);
            _bankController = GetBankController(rom, ramBanks);
        }

        private string GetTitle(byte[] rom)
        {
            var title = new StringBuilder();
            for (var i = 0x0134; i < 0x0143; i++)
            {
                var c = (char)rom[i];
                if (c == 0) break;
                title.Append(c);
            }
            return title.ToString();
        }

        private CartridgeType GetType(byte[] rom)
        {
            return (CartridgeType)rom[0x147];
        }        

        private int GetRamBanks(byte[] rom)
        {
            var banks = rom[0x149];
            switch (banks)
            {
                case 0x00:
                    return 0;
                case 0x01:
                case 0x02:
                    return 1;
                case 0x03:
                    return 4;
                case 0x04:
                    return 16;
                default:
                    throw new NotSupportedException($"The specified RAM Size is unknown, value was: {banks}");
            }
        }

        private IMemoryMap GetBankController(byte[] rom, int ramBanks)
        {
            switch (Type)
            {
                case CartridgeType.MBC1:
                case CartridgeType.MBC1_RAM:
                case CartridgeType.MBC1_RAM_BATTERY:
                    return new MBC1(rom, ramBanks);
                default:
                    throw new NotSupportedException($"The Bank Controller {Type} is not supported");
            }
        }

        public bool CanRead(ushort address)
        {
            return _bankController.CanRead(address);
        }

        public void SetByte(ushort address, byte value)
        {
            _bankController.SetByte(address, value);
        }

        public byte GetByte(ushort address)
        {
            return _bankController.GetByte(address);
        }
    }
}