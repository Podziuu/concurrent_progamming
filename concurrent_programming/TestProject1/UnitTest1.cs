namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            concurrent_programming.Class1 klasa = new concurrent_programming.Class1();
            Assert.AreEqual(4, klasa.add(2, 2));
            
        }
    }
}