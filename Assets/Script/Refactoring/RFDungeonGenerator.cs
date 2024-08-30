using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RFDungeonGenerator : MonoBehaviour
{
    
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
                    int randomRoom = Random.Range(0,_rooms.Length) ;

                    GameObject newRoom =   Instantiate(_rooms[randomRoom], new Vector3(i * offset.x, 0f, -j * offset.y),Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    //rb.UpdateRoom(currentCell.status,currentCell.wallStatus);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }

    }



    public void MazeGenerator()
    {

        _board = new List<Cell>();

        _board = BoardGenerator();

        PathGenerator(_board);


       //Instantiate rooms
        GenerateDungeon();
      
    }

    private List<Cell> BoardGenerator()
    {
        _board = new List<Cell>();

        float boardLenght = _dungeonSize.x * _dungeonSize.y;

        for (int i = 0; i < boardLenght; i++)
        {
            _board.Add(new Cell());
        }

        return _board;
    }


    private void PathGenerator(List<Cell> _board)
    {
        //Create Dungeon Maze
        //StarPosition determina el casillero donde el arranca el Dungeon
        int currentCell = _startPos;
        //marca la celda actual como visitada
        _board[currentCell].visited = true;

        //Generamos la Pila(Stack) donde armaremos el Laberinto
        Stack<int> path = new Stack<int>();

        if (_board.Count>1)
        {
            for(int i=0; i < _board.Count; i++)
            {
                List<int> neighbors = CheckNeighbors(currentCell);
                if (neighbors.Count==0 || i==_board.Count )
                {
                    /*if (path.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        currentCell = path.Pop();
                    }*/
                    break;
                }
                else
                {
                    path.Push(currentCell);
                    //RemoveWall(currentCell, neighbors);
                    currentCell = DoorCheck(currentCell, neighbors);
                }

            }
        }
        else if(_board.Count == 1)
        {
            currentCell = path.Pop();
        }
        else 
        {
            Debug.Log("El dungeon debe de tener un elemento");
        }
   
    }

    private void RemoveWall(int currentCell, List<int> neighbors){
        for(int i=0;i<neighbors.Count;i++){
           int newCell = neighbors[i];
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

            if(!_board[currentCell].wallStatus[directionStatus[0]] && !_board[newCell].wallStatus[directionStatus[1]]){
                _board[currentCell].wallStatus[directionStatus[0]] = true;
            }
            /*else{}
            _board[currentCell].wallStatus[directionStatus[0]] = true;
            _board[newCell].wallStatus[directionStatus[1]] = true;*/
        }

    }


    private int DoorCheck(int currentCell, List<int> neighbors)
    {
        
        int random = Random.Range(1, neighbors.Count);
        int newCell=0;        

        for (int i = 0; i < random; i++)
        {
            int element = Random.Range(0, neighbors.Count);
            newCell = neighbors[element];
            // 0 - Up, 1 - Down, 2 - Right, 3 - Left
            int[] directionStatus = new int[2];
            // Calcula la dirección según la diferencia entre newCell y currentCell
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
            
            // Aplica el estado de la celda actual y la nueva celda
            _board[currentCell].status[directionStatus[0]] = true;
            _board[newCell].status[directionStatus[1]] = true;
            //_board[newCell].Create = true; //marca la celda actual como visitada
            neighbors.RemoveAt(element); // elimina al vecino por si hay una segunda puerta para que no se repita
            //Debug.Log(newCell);

        }
        currentCell = newCell; 
         _board[newCell].visited = true;

        return currentCell;
    }



    
    //Chequea las celdas vecinas
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
/*
            ═╬═══════╬════════╬════════╬
             ║       ║        ║        ║
             ║   1   ║   2    ║   3    ║
            ═╬═══════╬════════╬════════╬═
             ║       ║        ║        ║
             ║   4   ║   5    ║   6    ║
            ═╬═══════╬════════╬════════╬═

            EJ: cell=1 

            1 (0 - 3 > -2 ) && !(_board(-5).visited) => F && F  =F
              (0 + 3 < 6 ) && !(_board(2).visited) => V && V  = V
              (0+1 % 6  !=0 ) && !(_board(2).visited) => V && V  =V
              (0 % 6  !=0 ) && !(_board(0).visited) => F && V  =F
*/
        
        
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
