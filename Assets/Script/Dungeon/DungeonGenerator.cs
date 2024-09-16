using System;

using System.Collections.Generic;


using UnityEngine;
using Object = UnityEngine.Object;

public class DungeonGenerator : MonoBehaviour
{
    
    [SerializeField] Vector2 _dungeonSize;
    [SerializeField] int _startPos = 0;

    [SerializeField] GameObject  _Player;
    [SerializeField] Vector2 _offset;
    [SerializeField] RoomConfiguration _roomConfiguration;

 

    DungeonFactory _dungeonFactory;
    BoardManager _boardManager;
    MazeManager _mazeManager;
    SaveDungeon _saveDungeon;

    LoadDungeons _loadDungeons;

    MeshManager _mesh;

    //List<Cell> _board;
   private void Awake() {
     _dungeonFactory = new DungeonFactory(Instantiate(_roomConfiguration));
     _boardManager = new BoardManager(_dungeonSize);
     _mazeManager = new MazeManager(_boardManager,_dungeonSize);
     _saveDungeon = new SaveDungeon();
     _loadDungeons = new LoadDungeons();
     _mesh= new MeshManager();
   }
    // Start is called before the first frame update 
    void Start()
    {
        GenerateDungeon();
        _mesh.GenerateNavMesh();
       
        Instantiate(_Player, new Vector3(0f, 0.1f, 0f), Quaternion.identity);
    }


    public void GenerateDungeon()
    {
        if(GameManager.Instance._board.Count<1){
         _mazeManager.GeneratePath(_startPos);
        _dungeonFactory.Create(_dungeonSize,_boardManager.Board,_offset);  
        }
        else
            _dungeonFactory.Create(GameManager.Instance._dungeonSize,GameManager.Instance._board,_offset);  
            

    }
    
    public void SaveDungeon()
    {
        _saveDungeon.Save(_boardManager.Board,_dungeonSize.x +" X "+_dungeonSize.y+" "+System.DateTime.Now,_dungeonSize);
    }
    public Dictionary<string, MyDungeons> DungeonLoad(){
        return _loadDungeons.LoadDungeon();
    }
    
}
