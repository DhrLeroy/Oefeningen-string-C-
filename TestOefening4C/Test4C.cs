namespace TestOefening4C
{
    [TestClass]
    public sealed class Test4C
    {
        [TestMethod]
        public void TestAssassinsCreedShadowsSpecialEditionPS57999()
        {
            TestHelper.HelperMethods.AssertOutputs("Titel: Assassin’s Creed Shadows – Special Edition\r\nConsole: PS5\r\nPrijs: 79,99", "Assassin’s Creed Shadows – Special Edition (PS5) – € 79,99");
        }

        [TestMethod]
        public void TestNBA2K25PS41999()
        {
            TestHelper.HelperMethods.AssertOutputs("Titel: NBA 2K25\r\nConsole: PS4\r\nPrijs: 19,99", "NBA 2K25 (PS4) – € 19,99");
        }
    }
}
