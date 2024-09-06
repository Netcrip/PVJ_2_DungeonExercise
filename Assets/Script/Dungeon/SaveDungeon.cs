using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDungeon { 
    
    public void Save(List<Cell> board)
    {
        foreach (var cell in board)
        {
            Debug.Log(cell.roomId);
        }
    } 
    

   
}
