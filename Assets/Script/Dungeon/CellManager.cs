using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager 
{
    // Start is called before the first frame update
    private BoardManager _boardManager;
    private Vector2 _dungeonSize;
    public CellManager (BoardManager boardManager, Vector2  dungeonSize){
        _boardManager=boardManager;
        _dungeonSize= dungeonSize;
    }

    public void PilarCheck(int currentCell, List<int> neighbors)
    {
        /* 
          Diagrama de posiciones:
            0             1
             ═╬═════════╬═
              ║    0    ║
              ║3       2║
              ║    1    ║
             ═╬═════════╬═
            3            2
        */

        // Inicializar el estado de las paredes alrededor de la celda actual
        bool[] wall = new bool[4]; // [0] Arriba, [1] Abajo, [2] Derecha, [3] Izquierda

        // Determinar las paredes alrededor de la celda actual
        for (int i = 0; i < neighbors.Count; i++)
        {
            int neighbor = neighbors[i];
            int direction = neighbor - currentCell; // Corregir la dirección para que sea relativa a la celda actual

            switch (direction)
            {
                case 1: // Derecha
                    wall[2] = true;
                    break;
                case -1: // Izquierda
                    wall[3] = true;
                    break;
                default:
                    if (direction > 0) // Abajo
                    {
                        wall[1] = true;
                    }
                    else // Arriba
                    {
                        wall[0] = true;
                    }
                    break;
            }
        }

        // Primer elemento sin vecinos
        if (!wall[0] && !wall[1] && !wall[2] && !wall[3])
        {
            // No hay paredes alrededor, activar todos los pilares
            _boardManager.Board[currentCell].pillarStatus[0] = true;
            _boardManager.Board[currentCell].pillarStatus[1] = true;
            _boardManager.Board[currentCell].pillarStatus[2] = true;
            _boardManager.Board[currentCell].pillarStatus[3] = true;
        }
        else
        {
            //  combinaciones de paredes 
            if (wall[0] && !wall[1] && !wall[2] && !wall[3])
            {
                // Pared arriba
                _boardManager.Board[currentCell].pillarStatus[2] = true;
                _boardManager.Board[currentCell].pillarStatus[3] = true;
            }
            else if (!wall[0] && wall[1] && !wall[2] && !wall[3])
            {
                // Pared abajo
                _boardManager.Board[currentCell].pillarStatus[0] = true;
                _boardManager.Board[currentCell].pillarStatus[1] = true;
            }
            else if (!wall[0] && !wall[1] && wall[2] && !wall[3])
            {
                // Pared derecha
                _boardManager.Board[currentCell].pillarStatus[0] = true;
                _boardManager.Board[currentCell].pillarStatus[3] = true;
            }
            else if (!wall[0] && !wall[1] && !wall[2] && wall[3])
            {
                // Pared izquierda
                _boardManager.Board[currentCell].pillarStatus[1] = true;
                _boardManager.Board[currentCell].pillarStatus[2] = true;
            }
            else if (wall[0] && !wall[1] && wall[2] && !wall[3])
            {
                // Pared arriba y derecha
                _boardManager.Board[currentCell].pillarStatus[3] = true;
            }
            else if (wall[0] && !wall[1] && !wall[2] && wall[3])
            {
                // Pared arriba y izquierda
                _boardManager.Board[currentCell].pillarStatus[2] = true;
            }
            else if (!wall[0] && wall[1] && wall[2] && !wall[3])
            {
                // Pared abajo y derecha
                _boardManager.Board[currentCell].pillarStatus[0] = true;
            }
            else if (!wall[0] && wall[1] && !wall[2] && wall[3])
            {
                // Pared abajo e izquierda
                _boardManager.Board[currentCell].pillarStatus[1] = true;
            }
        }
    }

    public int DoorCheck(int currentCell, List<int> neighbors){
            int element = Random.Range(0, neighbors.Count); 
            int newCell = neighbors[element];
          
            int direction = newCell - currentCell;
            int[] directionStatus = CheckDirection(direction);
            _boardManager.Board[currentCell].status[directionStatus[0]] = true;
        //_board[newCell].status[directionStatus[1]] = true;
            _boardManager.Board[newCell].wallStatus[directionStatus[1]] = true;
           

        neighbors.Remove(element);
        if (Random.Range(0, 100)<30 && neighbors.Count>0)
            {
            
            element = Random.Range(0, neighbors.Count);
            int newDoor = neighbors[element];
            direction = newDoor - currentCell;
            directionStatus = CheckDirection(direction);
            _boardManager.Board[currentCell].status[directionStatus[0]] = true;
            //_board[newDoor].status[directionStatus[1]] = true;
            _boardManager.Board[newDoor].wallStatus[directionStatus[1]] = true;

            _boardManager.Board[newDoor].generate = true;
        }

            currentCell = newCell;

        return currentCell;
    }


    private int[] CheckDirection(int direction)
    {
        int[] directionStatus = new int[6]; ;
        /* 
           0      1      1
            ═╬═════════╬═
             ║    0    ║
            2║3       2║3
             ║    1    ║
            ═╬═════════╬═
           3      0      2
            */
        if (direction == 1)        // Derecha
        {
            directionStatus[0] = 2;
            directionStatus[1] = 3;

        }
        else if (direction == -1)  // Izquierda
        {
            directionStatus[0] = 3;
            directionStatus[1] = 2;

        }
        else if (direction > 0)    // Abajo
        {
            directionStatus[0] = 1;
            directionStatus[1] = 0;

        }
        else                       // Arriba
        {
            directionStatus[0] = 0;
            directionStatus[1] = 1;

        }

        return directionStatus;
    }
    
    //Chequea las celdas vecinas
    public List<int> CheckNeighbors(int cell, bool visited)
    {
        List<int> neighbors = new List<int>();
        
        //check Up
        if(cell - _dungeonSize.x >= 0 && _boardManager.Board[Mathf.FloorToInt(cell - _dungeonSize.x)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - _dungeonSize.x));
        }
        
        //check Down
        if(cell + _dungeonSize.x < _boardManager.Board.Count && _boardManager.Board[Mathf.FloorToInt(cell + _dungeonSize.x)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + _dungeonSize.x));
        }

        //check Right
        if((cell + 1) % _dungeonSize.x != 0 && _boardManager.Board[Mathf.FloorToInt(cell + 1)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check Left
        if(cell % _dungeonSize.x != 0 && _boardManager.Board[Mathf.FloorToInt(cell - 1)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }
}
