using TestHelper;

namespace TestOefening4F
{
    [TestClass]
    public sealed class TestF
    {
        [TestMethod]
        public void TestMethodbanaan()
        {
            HelperMethods.AssertOutputs("Geef een woord in: B A N A A N", "banaan");
        }
        [TestMethod]
        public void TestMethodCampusImpuls()
        {
            HelperMethods.AssertOutputs("Geef een woord in: C A M P U S   I M P U L S", "campus impuls");
        }
    }
}
