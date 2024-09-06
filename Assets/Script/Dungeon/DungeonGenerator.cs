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

    //List<Cell> _board;
   

   private void Awake() {
     _dungeonFactory = new DungeonFactory(Instantiate(_roomConfiguration));
     _boardManager = new BoardManager(_dungeonSize);
     _mazeManager = new MazeManager(_boardManager,_dungeonSize);
     _saveDungeon = new SaveDungeon();
   }
    // Start is called before the first frame update 
    void Start()
    {
        GenerateDungeon();
    }


    public void GenerateDungeon()
    {
        _mazeManager.GeneratePath(_startPos);
        _dungeonFactory.Create(_dungeonSize,_boardManager.Board,_offset);      

    }
    
    public void SaveDungeon()
    {
        _saveDungeon.Save(_boardManager.Board);
    }
    
}
