namespace TauGame;

public class GameBoard
{
    private readonly char[,] board;
    private readonly int rows;
    private readonly int cols;
    private readonly Random random;

    public GameBoard(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.board = new char[rows, cols];
        this.random = new Random();

        InitializeBoard();
    }

    private void InitializeBoard()
    {
        // Fill the board with empty spaces
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board[i, j] = ' ';
            }
        }

        // Place start and stop randomly on the edges
        PlaceStartAndStop();

        // Place random obstacles
        PlaceObstacles();
    }

    private void PlaceStartAndStop()
    {
        // Place START at the edge
        int startRow = random.Next(1, rows - 1);
        board[startRow, 0] = 'A';

        // Place STOP at a different edge
        int stopRow;
        do
        {
            stopRow = random.Next(1, rows - 1);
        } while (stopRow == startRow);

        board[stopRow, cols - 1] = 'B';
    }

    private void PlaceObstacles()
    {
        // Place random obstacles
        int numObstacles = random.Next(1, (rows * cols) / 4);

        for (int i = 0; i < numObstacles; i++)
        {
            int obstacleRow, obstacleCol;

            do
            {
                obstacleRow = random.Next(1, rows - 1);
                obstacleCol = random.Next(1, cols - 1);
            } while (board[obstacleRow, obstacleCol] != ' ');

            board[obstacleRow, obstacleCol] = 'X';
        }
    }

    public void ExportBoardToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    writer.Write(board[i, j] + " ");
                }
                writer.WriteLine();
            }
        }
    }

    public bool Move(int row, int col, char direction)
    {
        // Check if the move is valid
        switch (direction)
        {
            case 'U':
                if (row > 0 && board[row - 1, col] != 'X')
                {
                    SwapCells(row, col, row - 1, col);
                    return true;
                }
                break;
            case 'D':
                if (row < rows - 1 && board[row + 1, col] != 'X')
                {
                    SwapCells(row, col, row + 1, col);
                    return true;
                }
                break;
            case 'L':
                if (col > 0 && board[row, col - 1] != 'X')
                {
                    SwapCells(row, col, row, col - 1);
                    return true;
                }
                break;
            case 'R':
                if (col < cols - 1 && board[row, col + 1] != 'X')
                {
                    SwapCells(row, col, row, col + 1);
                    return true;
                }
                break;
        }

        return false;
    }

    private void SwapCells(int row1, int col1, int row2, int col2)
    {
        char temp = board[row1, col1];
        board[row1, col1] = board[row2, col2];
        board[row2, col2] = temp;
    }
}