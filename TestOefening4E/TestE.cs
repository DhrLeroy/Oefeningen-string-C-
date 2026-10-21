namespace TestOefening4E
{
    [TestClass]
    public sealed class TestE
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
        [TestMethod]
        public void TestAanhetmeerziteenbanaanendiedroomtvandezee()
        {
            TestHelper.HelperMethods.AssertOutputs("An het mer zit en banan en die dromt van de ze", "Aan het meer zit een banaan en die droomt van de zee");
        }
    }
}
