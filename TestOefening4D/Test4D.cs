namespace TestOefening4D
{
    [TestClass]
    public sealed class Test4D
    {
        [TestMethod]
        public void TestHallo()
        {
            TestHelper.HelperMethods.AssertOutputs("Woord: Gemaskeerd: H***O", "Hallo");
        }
        [TestMethod]
        public void TestEn()
        {
            TestHelper.HelperMethods.AssertOutputs("Woord: Gemaskeerd: EN", "En");
        }
        [TestMethod]
        public void Testtest()
        {
            TestHelper.HelperMethods.AssertOutputs("Woord: Gemaskeerd: T**T", "test");
        }
    }
}
