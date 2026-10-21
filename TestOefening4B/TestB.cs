namespace TestOefening4B
{
    [TestClass]
    public sealed class TestB
    {
        [TestMethod]
        public void TestDitiseenZin()
        {
            TestHelper.HelperMethods.AssertOutputs("DIT is een zin.", "dit is een ZIN.");
        }
        [TestMethod]
        public void Testenookeenskijkenofditwerkt()
        {
            TestHelper.HelperMethods.AssertOutputs("EN ook eens kijken of dit werkt?", "en ook eens kijken of dit werkt?");
        }
    }
}
