using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }
    
    [SerializeField] Vector2 _dungeonSize;
    [SerializeField] int _startPos = 0;

    [SerializeField] GameObject[]  _rooms;
    [SerializeField] Vector2 offset;
    
    List<Cell> _board;
   
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }


    void GenerateDungeon()
    {
        for(int i = 0; i < _dungeonSize.x; i++)
        {
            for(int j = 0; j < _dungeonSize.y; j++)
            {
                Cell currentCell = _board[Mathf.FloorToInt(i+j*_dungeonSize.x)];

                if(currentCell.visited)
                {
                    int randomRoom = UnityEngine.Random.Range(0,_rooms.Length) ;

                    GameObject newRoom =   Instantiate(_rooms[randomRoom], new Vector3(i * offset.x, 0f, -j * offset.y),Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    rb.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }

    }

    public void MazeGenerator()
    {
        //Create Dungeon board
        _board = new List<Cell>();

        float boardLenght = _dungeonSize.x * _dungeonSize.y;

        for(int i = 0; i < boardLenght; i++)
        {
            _board.Add(new Cell());
        }
      
       //Create Dungeon Maze
       //StarPosition determina el casillero donde el arranca el Dungeon
        int currentCell = _startPos;

        //Generamos la Pila(Stack) donde armaremos el Laberinto
        Stack<int> path = new Stack<int>();
     
        int k = 0;
        /*for(int i=0;i<_board.Count;i++)*/while(k < 1000)
        {
            k ++;
          
            //marca la celda actual como visitada
            _board[currentCell].visited = true;

            //si se alcanza la celda de salida
            //ser termina el bucle
            if(currentCell == _board.Count -1)
            {
                Debug.Log(k);
                break;
            }

            //Check Neighbors cells
            List<int> neighbors = CheckNeighbors(currentCell);

            if(neighbors.Count == 0)
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
                
                currentCell=DoorCheck(currentCell,neighbors);
            }
        }

       //Instantiate rooms
        GenerateDungeon();
      
    }

    private int DoorCheck(int currentCell, List<int> neighbors){
           int newCell = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
           int[] directionStatus = new int[2];
           int direction = newCell - currentCell;
           /* 
                  1
            ═╬═════════╬═
             ║    0    ║
            2║3       2║3
             ║    1    ║
            ═╬═════════╬═
                  0
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

            _board[currentCell].status[directionStatus[0]] = true;
             currentCell = newCell;
            _board[currentCell].status[directionStatus[1]] = true;


    return currentCell;
    }


    
    //Chequea las celdas vecinas
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        
        //check Up
        if(cell - _dungeonSize.x >= 0 && !_board[Mathf.FloorToInt(cell - _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - _dungeonSize.x));
        }
        
        //check Down
        if(cell + _dungeonSize.x < _board.Count && !_board[Mathf.FloorToInt(cell + _dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + _dungeonSize.x));
        }

        //check Right
        if((cell + 1) % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check Left
        if(cell % _dungeonSize.x != 0 && !_board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }


    private void OnGUI() 
    {
        float w = Screen.width/2;
        float h = Screen.height - 80;
        if(GUI.Button(new Rect(w,h,250,50), "Regenerate Dungeon"))
        {
            RegenerateDungeon();
        }
    }

    void RegenerateDungeon()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
