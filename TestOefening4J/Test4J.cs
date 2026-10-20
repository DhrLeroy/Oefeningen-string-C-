namespace TestOefening4J
{
    [TestClass]
    public sealed class Test4J
    {
        [TestMethod]
        public void TestDit_is_een_zin()
        {
            TestHelper.HelperMethods.AssertOutputs("Zin: Dit  is  een  zin.", "Dit is een zin.");
        }
        [TestMethod]
        public void TestDit_is__een_zin()
        {
            TestHelper.HelperMethods.AssertOutputs("Zin: Dit  is    een  zin.", "Dit is  een zin.");
        }
        [TestMethod]
        public void TestDitiseenzin()
        {
            TestHelper.HelperMethods.AssertOutputs("Zin: Ditiseenzin.", "Ditiseenzin.");
        }
    }
}
