using System.Collections.Generic;
using UnityEngine;

public class BoardManager 
{
    public List<Cell> Board {get;private set;}
    private Vector2 _dungeonSize;

    public BoardManager (Vector2 dungeonSize){
        _dungeonSize=dungeonSize;
        Board=CreateBoard();
    }
  private List<Cell> CreateBoard()
    {
        // Create Dungeon board
        Board = new List<Cell>();

        float boardLenght = _dungeonSize.x * _dungeonSize.y;

        for (int i = 0; i < boardLenght; i++)
        {
            Board.Add(new Cell());
        }
        return Board;
    }
}
