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
    [SerializeField] RoomConfiguration _roomConfiguration;

    DungeonFactory _dungeonFactory;
    BoardManager _boardManager;
    MazeManager _mazeManager;
    SaveDungeon _saveDungeon;

    LoadDungeons _loadDungeons;

    //List<Cell> _board;
   

   private void Awake() {
     _dungeonFactory = new DungeonFactory(Instantiate(_roomConfiguration));
     _boardManager = new BoardManager(_dungeonSize);
     _mazeManager = new MazeManager(_boardManager,_dungeonSize);
     _saveDungeon = new SaveDungeon();
     _loadDungeons = new LoadDungeons();
   }
    // Start is called before the first frame update 
    void Start()
    {
        GenerateDungeon();
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
