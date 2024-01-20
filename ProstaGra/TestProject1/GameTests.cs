using TauGame;

namespace TestProject1;

public class GameTests
{
    [Fact]
    public void MoveUp_ValidMove_ReturnsTrue()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result = gameBoard.Move(2, 2, 'U');

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void MoveLeft_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result = gameBoard.Move(2, 0, 'L');

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void MoveDown_ValidMove_ReturnsTrue()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result = gameBoard.Move(2, 2, 'D');

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void MoveMultipleDirections_ValidMoves_ReturnsTrue()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool move1 = gameBoard.Move(2, 2, 'U');
        bool move2 = gameBoard.Move(2, 2, 'R');
        bool move3 = gameBoard.Move(2, 2, 'D');
        bool move4 = gameBoard.Move(2, 2, 'L');

        // Assert
        Assert.True(move1);
        Assert.True(move2);
        Assert.True(move3);
        Assert.True(move4);
    }

    [Fact]
    public void MoveIntoObstacle_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Place an obstacle in the way
        gameBoard.Move(2, 3, 'R'); // Move right to create an obstacle

        // Act
        bool result = gameBoard.Move(2, 2, 'R'); // Try to move into the obstacle

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void MoveOutOfBounds_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result1 = gameBoard.Move(0, 2, 'U'); // Move up from the top edge
        bool result2 = gameBoard.Move(4, 2, 'D'); // Move down from the bottom edge
        bool result3 = gameBoard.Move(2, 0, 'L'); // Move left from the left edge
        bool result4 = gameBoard.Move(2, 4, 'R'); // Move right from the right edge

        // Assert
        Assert.False(result1);
        Assert.False(result2);
        Assert.False(result3);
        Assert.False(result4);
    }
    

    
    [Fact]
    public void MoveThroughOpenPath_ValidMove_ReturnsTrue()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool move1 = gameBoard.Move(2, 2, 'R');
        bool move2 = gameBoard.Move(2, 3, 'R');
        bool move3 = gameBoard.Move(2, 4, 'U');
        bool move4 = gameBoard.Move(1, 4, 'L');
        bool move5 = gameBoard.Move(1, 3, 'D');
        bool move6 = gameBoard.Move(2, 3, 'L');

        // Assert
        Assert.True(move1);
        Assert.True(move2);
        Assert.True(move3);
        Assert.True(move4);
        Assert.True(move5);
        Assert.True(move6);
    }

    [Fact]
    public void MoveIntoWall_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Place a wall in the way
        gameBoard.Move(2, 3, 'R');
        gameBoard.Move(2, 2, 'R');
        gameBoard.Move(2, 1, 'R');

        // Act
        bool result = gameBoard.Move(2, 0, 'R'); // Try to move into the wall

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void MoveAlongEdge_ValidMoves_ReturnsTrue()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool move1 = gameBoard.Move(2, 0, 'D');
        bool move2 = gameBoard.Move(3, 0, 'R');
        bool move3 = gameBoard.Move(3, 1, 'D');
        bool move4 = gameBoard.Move(4, 1, 'R');
        bool move5 = gameBoard.Move(4, 2, 'U');

        // Assert
        Assert.True(move1);
        Assert.True(move2);
        Assert.True(move3);
        Assert.True(move4);
        Assert.True(move5);
    }
    [Fact]
    public void MoveUp_FromTopEdge_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result = gameBoard.Move(0, 2, 'U'); // Try to move up from the top edge

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void MoveLeft_FromLeftEdge_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result = gameBoard.Move(2, 0, 'L'); // Try to move left from the left edge

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void MoveDown_FromBottomEdge_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result = gameBoard.Move(4, 2, 'D'); // Try to move down from the bottom edge

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void MoveRight_FromRightEdge_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = new GameBoard(5, 5);

        // Act
        bool result = gameBoard.Move(2, 4, 'R'); // Try to move right from the right edge

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void MoveWithoutInitialization_InvalidMove_ReturnsFalse()
    {
        // Arrange
        GameBoard gameBoard = null; // Not initializing the game board

        // Act
        bool result = gameBoard.Move(2, 2, 'U'); // Try to move without initializing the game board

        // Assert
        Assert.False(result);
    }

    
}