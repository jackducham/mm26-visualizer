namespace MM26.IO.Models
{
    public static class BoardExtension
    {
        public static int GetHeight(this Board board)
        {
            return board.Columns;
        }

        public static int GetWidth(this Board board)
        {
            return board.Rows;
        }
    }
}
