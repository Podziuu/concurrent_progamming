using Logic;
using Data;
namespace LogicTest
{
    internal class FakeDataApi : DataAbstractAPI
    {
        public override int Height => throw new NotImplementedException();
        public override int Width => throw new NotImplementedException();
        public override List<IBall> CreateBalls(int ballsQuantity, int radius)
        {
           throw new NotImplementedException();
        }
        public override List<IBall> GetAllBalls()
        {
            throw new NotImplementedException();
        }
        public override void RemoveBalls()
        {
            throw new NotImplementedException();
        }
        public override List<List<float>> getBallsPosition()
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class PoolTableTest
    {
        private DataAbstractAPI data;
        private LogicAbstractAPI poolTable;

        [TestInitialize]
        public void Initialize()
        {
            data = FakeDataApi.CreateApi();
            poolTable = LogicAbstractAPI.CreateApi(data);
        }


        [TestMethod]
        public void TestCreateBalls()
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