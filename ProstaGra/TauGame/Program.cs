namespace TauGame;

abstract class Program
{
    static void Main()
    {
        // Example usage
        GameBoard gameBoard = new GameBoard(5, 5);
        gameBoard.ExportBoardToFile("gameboard.txt");
    }
}