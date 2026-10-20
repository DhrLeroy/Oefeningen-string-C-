namespace TestOefening4A
{
    [TestClass]
    public sealed class Test4A
    {
        [TestMethod]
        public void TestJohnDoe()
        {
            TestHelper.HelperMethods.AssertOutputs("Voornaam: Naam: J. D.", "john", "doe");
        }
        [TestMethod]
        public void TestEmmaStone()
        {
            TestHelper.HelperMethods.AssertOutputs("Voornaam: Naam: E. S.", "EMMA", "stone");
        }
    }
}
