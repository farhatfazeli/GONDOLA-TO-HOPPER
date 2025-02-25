namespace TrainGame.Infrastructure
{
    public class RollingStockViewFactory
    {
        
        
        private static RollingStockViewFactory instance;
        public static RollingStockViewFactory I => instance ??= new RollingStockViewFactory();
        private RollingStockViewFactory()
        {
        }
    }
}