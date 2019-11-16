namespace MM26.IO
{
    public interface IDataProvider
    {
        VisualizerChange GetChange(int changeIndex);
        VisualizerTurn GetTurn(int turnIndex);
    }

    public class DataProvider
    {
        public static IDataProvider ForDesktop() { return null; }
        public static IDataProvider ForBrowser() { return null; }
        public static IDataProvider Default() { return null; }
    }
}