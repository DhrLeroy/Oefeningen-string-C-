using TestHelper;

namespace TestOefening4F
{
    [TestClass]
    public sealed class Test4F
    {
        [TestMethod]
        public void TestMethodbanaan()
        {
            HelperMethods.AssertOutputs("Geef een woord in: B A N A A N", "banaan");
        }
        [TestMethod]
        public void TestMethodviso()
        {
            HelperMethods.AssertOutputs("Geef een woord in: V I S O", "viso");
        }
    }
}
