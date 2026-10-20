namespace TestOefening4H
{
    [TestClass]
    public sealed class Test4H
    {
        [TestMethod]
        public void TestDit_is_een_zin()
        {
            TestHelper.HelperMethods.AssertOutputs("zin een is Dit", "Dit is een zin");
        }
        [TestMethod]
        public void TestAppel()
        {
            TestHelper.HelperMethods.AssertOutputs("Appel", "Appel");
        }
        [TestMethod]
        public void TestHallo_wereld()
        {
            TestHelper.HelperMethods.AssertOutputs("wereld Hallo", "Hallo wereld");
        }
        [TestMethod]
        public void TestExtra__spatie()
        {
            TestHelper.HelperMethods.AssertOutputs("spatie  Extra", "Extra  spatie");
        }
    }
}
