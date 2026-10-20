namespace TestOefening4E
{
    [TestClass]
    public sealed class Test4E
    {
        [TestMethod]
        public void TestIkeetbanaanineenmotorboot()
        {
            TestHelper.HelperMethods.AssertOutputs("Ik et banan in en motorbot", "Ik eet banaan in een motorboot");
        }
        [TestMethod]
        public void TestEenBanaan()
        {
            TestHelper.HelperMethods.AssertOutputs("En banan", "Een banaan");
        }
    }
}
