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
        for (int i = 0; i < dungeonSize.x; i++)
        {
            for (int j = 0; j < dungeonSize.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * dungeonSize.x)];
                int cellNumber = Mathf.FloorToInt(i + j * dungeonSize.x); 
                if (currentCell.visited || currentCell.generate)
                {   
                    string id = _rs.GetRoomId(board,cellNumber,_roomConfiguration);
                    RoomBehaviour rb = Object.Instantiate(_roomConfiguration.GetPrefabRoomId(id), new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity);
                rb.UpdateRoom(currentCell);

                rb.name += " " + i + "-" + j;

                 board[Mathf.FloorToInt(i + j * dungeonSize.x)].roomId = id;
                }            
            }
        }
    }
}
