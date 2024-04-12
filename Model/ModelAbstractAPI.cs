namespace Model
{
    public class ModelAbstractAPI
    {
        private Logic.LogicAbstractAPI logicAPI;

        public static ModelAbstractAPI CreateAPI()
        {
            return new BallManager();
        }

    }
}
