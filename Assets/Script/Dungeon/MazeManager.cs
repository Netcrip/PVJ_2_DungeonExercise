using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager 
{
    private BoardManager _boardManager;
    private CellManager _cellManager;
    
    public MazeManager( BoardManager boardManager, Vector2 dungeonSize){
        _boardManager=boardManager;
        _cellManager = new CellManager(boardManager, dungeonSize);
    }

    public void GeneratePath(int starPosition){
        //Create Dungeon Maze
        //StarPosition determina el casillero donde el arranca el Dungeon
        int currentCell = starPosition;

        //Generamos la Pila(Stack) donde armaremos el Laberinto
        Stack<int> path = new Stack<int>();  
 
        while (currentCell!= _boardManager.Board.Count -1)
        {

            _boardManager.Board[currentCell].visited = true;

            List<int> neighbors = _cellManager.CheckNeighbors(currentCell,false);
            List<int> neighborsVisited = _cellManager.CheckNeighbors(currentCell, true);

            if (neighbors.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);
                _cellManager.PilarCheck(currentCell, neighborsVisited);
                currentCell =_cellManager.DoorCheck(currentCell,neighbors);
            }
        }
        
        List<int> lastVisited = _cellManager.CheckNeighbors(currentCell, true);
        _cellManager.PilarCheck(currentCell, lastVisited);
        _boardManager.Board[currentCell].visited = true;

        //Cargar lso pilares de las celdas no visitadas.
        for(int i = 0;i < _boardManager.Board.Count; i++)
        {
            if (_boardManager.Board[i].generate)
            {
                lastVisited = _cellManager.CheckNeighbors(i, true);
                _cellManager.PilarCheck(i, lastVisited);
            }  
        }
    }
    
}
