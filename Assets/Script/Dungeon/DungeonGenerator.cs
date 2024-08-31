using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    
    [SerializeField] Vector2 _dungeonSize;
    [SerializeField] int _startPos = 0;

    //[SerializeField] GameObject[]  _rooms;
    [SerializeField] Vector2 _offset;

    DungeonFactory _factory;

    [SerializeField] RoomConfiguration _roomConfiguration;
    
    List<Cell> _board;
   

   private void Awake() {
     _factory = new DungeonFactory(Instantiate(_roomConfiguration));
   }
    // Start is called before the first frame update 
    void Start()
    {
        MazeGenerator();
    }


    void GenerateDungeon()
    {
        _factory.Create(_dungeonSize,_board,_offset);        
    }

    public void MazeGenerator()
    {
        _board = CreateBoard();

        GeneratePath();
        
        GenerateDungeon();
      
    }

    private void GeneratePath(){
        //Create Dungeon Maze
        //StarPosition determina el casillero donde el arranca el Dungeon
        int currentCell = _startPos;

        //Generamos la Pila(Stack) donde armaremos el Laberinto
        Stack<int> path = new Stack<int>();  
 
        while (currentCell!= _board.Count -1)
        {

            _board[currentCell].visited = true;

            List<int> neighbors = CheckNeighbors(currentCell,false);
            List<int> neighborsVisited = CheckNeighbors(currentCell, true);

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
                pilarCheck(currentCell, neighborsVisited);
                currentCell =DoorCheck(currentCell,neighbors);
            }
        }
        
        List<int> lastVisited = CheckNeighbors(currentCell, true);
        pilarCheck(currentCell, lastVisited);
        _board[currentCell].visited = true;

        //Cargar lso pilares de las celdas no visitadas.
        for(int i = 0;i < _board.Count; i++)
        {
            if (_board[i].generate)
            {
                lastVisited = CheckNeighbors(i, true);
                pilarCheck(i, lastVisited);
            }  
        }
    }

    private List<Cell> CreateBoard()
    {
        // Create Dungeon board
        _board = new List<Cell>();

        float boardLenght = _dungeonSize.x * _dungeonSize.y;

        for (int i = 0; i < boardLenght; i++)
        {
            _board.Add(new Cell());
        }
        return _board;
    }
   
    private void pilarCheck(int currentCell, List<int> neighbors)
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
            _board[currentCell].pillarStatus[0] = true;
            _board[currentCell].pillarStatus[1] = true;
            _board[currentCell].pillarStatus[2] = true;
            _board[currentCell].pillarStatus[3] = true;
        }
        else
        {
            //  combinaciones de paredes 
            if (wall[0] && !wall[1] && !wall[2] && !wall[3])
            {
                // Pared arriba
                _board[currentCell].pillarStatus[2] = true;
                _board[currentCell].pillarStatus[3] = true;
            }
            else if (!wall[0] && wall[1] && !wall[2] && !wall[3])
            {
                // Pared abajo
                _board[currentCell].pillarStatus[0] = true;
                _board[currentCell].pillarStatus[1] = true;
            }
            else if (!wall[0] && !wall[1] && wall[2] && !wall[3])
            {
                // Pared derecha
                _board[currentCell].pillarStatus[0] = true;
                _board[currentCell].pillarStatus[3] = true;
            }
            else if (!wall[0] && !wall[1] && !wall[2] && wall[3])
            {
                // Pared izquierda
                _board[currentCell].pillarStatus[1] = true;
                _board[currentCell].pillarStatus[2] = true;
            }
            else if (wall[0] && !wall[1] && wall[2] && !wall[3])
            {
                // Pared arriba y derecha
                _board[currentCell].pillarStatus[3] = true;
            }
            else if (wall[0] && !wall[1] && !wall[2] && wall[3])
            {
                // Pared arriba y izquierda
                _board[currentCell].pillarStatus[2] = true;
            }
            else if (!wall[0] && wall[1] && wall[2] && !wall[3])
            {
                // Pared abajo y derecha
                _board[currentCell].pillarStatus[0] = true;
            }
            else if (!wall[0] && wall[1] && !wall[2] && wall[3])
            {
                // Pared abajo e izquierda
                _board[currentCell].pillarStatus[1] = true;
            }
        }
    }

    private int DoorCheck(int currentCell, List<int> neighbors){
            int element = Random.Range(0, neighbors.Count); 
            int newCell = neighbors[element];
          
            int direction = newCell - currentCell;
            int[] directionStatus = CheckDirection(direction);
            _board[currentCell].status[directionStatus[0]] = true;
        //_board[newCell].status[directionStatus[1]] = true;
            _board[newCell].wallStatus[directionStatus[1]] = true;
           

        neighbors.Remove(element);
        if (Random.Range(0, 100)<30 && neighbors.Count>0)
            {
            
            element = Random.Range(0, neighbors.Count);
            int newDoor = neighbors[element];
            direction = newDoor - currentCell;
            directionStatus = CheckDirection(direction);
            _board[currentCell].status[directionStatus[0]] = true;
            //_board[newDoor].status[directionStatus[1]] = true;
            _board[newDoor].wallStatus[directionStatus[1]] = true;

            _board[newDoor].generate = true;
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
    List<int> CheckNeighbors(int cell, bool visited)
    {
        List<int> neighbors = new List<int>();
        
        //check Up
        if(cell - _dungeonSize.x >= 0 && _board[Mathf.FloorToInt(cell - _dungeonSize.x)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - _dungeonSize.x));
        }
        
        //check Down
        if(cell + _dungeonSize.x < _board.Count && _board[Mathf.FloorToInt(cell + _dungeonSize.x)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + _dungeonSize.x));
        }

        //check Right
        if((cell + 1) % _dungeonSize.x != 0 && _board[Mathf.FloorToInt(cell + 1)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check Left
        if(cell % _dungeonSize.x != 0 && _board[Mathf.FloorToInt(cell - 1)].visited == visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }


   
    
}
