using Logic;
namespace LogicTest
{
    [TestClass]
    public class PoolTableTest
    {

        LogicAbstractAPI poolTable = LogicAbstractAPI.CreateApi();


        [TestMethod]
        public void TestCreateBalls()
        {
            poolTable.CreateBalls(5, 10);
            Assert.AreEqual(5, poolTable.GetAllBalls().Count);
        }

        [TestMethod]
        public void TestWidth()
        {
            poolTable.CreateBalls(5, 10);
            Assert.AreEqual(5, poolTable.GetAllBalls().Count);
        }

        [TestMethod]
        public void TestHeight()
        {
            poolTable.CreateBalls(5, 10);
            Assert.AreEqual(5, poolTable.GetAllBalls().Count);
        }

        [TestMethod]
        public void TestStopGame()
        {
            poolTable.CreateBalls(5, 10);
            poolTable.StartGame();
            poolTable.StopGame();
            Assert.AreEqual(0, poolTable.GetAllBalls().Count);
        }

    }
}