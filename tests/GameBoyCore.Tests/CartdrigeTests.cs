using Xunit;

namespace GameBoyCore.Tests
{
    public class CartdrigeTests
    {
        [Fact]
        public void ReadCatridge()
        {
            var tetris = "resources/cpu_instrs/cpu_instrs.gb";
            var cart = new Cartdrige(tetris);

            Assert.Equal("CPU_INSTRS", cart.Title);
            Assert.Equal(CartridgeType.MBC1, cart.Type);
        }
    }
}
