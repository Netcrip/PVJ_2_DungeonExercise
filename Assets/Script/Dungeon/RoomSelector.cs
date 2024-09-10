using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class RoomSelector 
{
  
  public string GetRoomId(List<Cell> board, int cellNumber,RoomConfiguration roomConfiguration ){
    string id="Vacio";
    int randomRoom = Random.Range(0,roomConfiguration.GetCount());
    if(board[cellNumber].roomId!=null){
        return board[cellNumber].roomId;
    }
    else if(board[cellNumber].roomId!=null){
        id= board[cellNumber].roomId;
    }
    else if(cellNumber==board.Count-1){
        id="Boss";
    }
    else if(cellNumber==0)
        id="Vacio";
    else if(Random.Range(0, 100)>20){
        switch(Random.Range(0,3)){
            case 0:
            id="Vacio";
            break;
            case 1:
            id="Jarrones";
            break;
            case 3:
            id="Bones";
            break;
            }   
        }
        else{
            switch(Random.Range(0,2)){
                case 0:
                id="Enemy";
                break;
                case 1:
                id="Enemy2";
                break;
            }
        }  
    return id;    
   }
}
