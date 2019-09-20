using Xunit;

namespace GameBoyCore.Tests
{
    public class CPUTests
    {

        [Theory]
        [InlineData("resources/cpu_instrs/individual/01-special.gb")]
        public void TestInstructions(string fileName)
        {
            var cart = new Cartdrige(fileName);
            var mmu = new MemoryManager(cart);
            var cpu = new CPU(mmu);

            while (true)
            {
                cpu.Tick();
            }
        }
    }
}