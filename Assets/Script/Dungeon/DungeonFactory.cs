using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DungeonFactory 
{
   private readonly RoomConfiguration _roomConfiguration;
   private RoomSelector _rs;

   public DungeonFactory(RoomConfiguration roomConfiguration){
    _roomConfiguration=roomConfiguration;
    _rs = new RoomSelector();
   }


    public void Create(Vector2 dungeonSize, List<Cell> board,Vector2 offset)
    {
        int totalWidth = Mathf.FloorToInt(dungeonSize.x);
        int totalHeight = Mathf.FloorToInt(dungeonSize.y);
       

        for (int i = 0; i < totalWidth; i++)
        {
            for (int j = 0; j < totalHeight; j++)
            {
                int cellIndex= i+j*totalWidth;
                Cell currentCell = board[cellIndex];
                
                if (currentCell.visited || currentCell.generate)
                {   
                    string id = _rs.GetRoomId(board,cellIndex,_roomConfiguration);
                    RoomBehaviour rb = Object.Instantiate(_roomConfiguration.GetPrefabRoomId(id), new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity);
                    rb.UpdateRoom(currentCell);

                    rb.name += " " + i + "-" + j;
                    board[Mathf.FloorToInt(i + j * dungeonSize.x)].roomId = id;
                }            
            }
        }
    }
}
