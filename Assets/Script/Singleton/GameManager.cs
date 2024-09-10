
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance {get; private set;}
    public List<Cell> _board = new List<Cell>();
    public Vector2 _dungeonSize {get; private set;}
    private void Awake(){
        if(Instance != null && Instance!= this){
            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Reload(){
        _board = new List<Cell>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChargeDungeon( MyDungeons myDungeons){
        
        _board = myDungeons.board;
        _dungeonSize=new Vector2(myDungeons.dungeonSizex,myDungeons.dungeonSizey);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}