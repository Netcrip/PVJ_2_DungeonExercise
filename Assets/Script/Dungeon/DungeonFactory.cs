using System.Collections.Generic;
using UnityEngine;

public class DungeonFactory 
{
   //[SerializeField] private GameObject[] _rooms;

   private readonly RoomConfiguration _roomConfiguration;

   public DungeonFactory(RoomConfiguration roomConfiguration){
    _roomConfiguration=roomConfiguration;
   }


    public void Create(Vector2 dungeonSize, List<Cell> board,Vector2 offset)
    {
        for (int i = 0; i < dungeonSize.x; i++)
        {
            for (int j = 0; j < dungeonSize.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * dungeonSize.x)];

                if (currentCell.visited || currentCell.generate)
                {
                    int randomRoom = Random.Range(0, _roomConfiguration.GetCount());
                    string id="Vacio";
                    if(Mathf.FloorToInt(i + j * dungeonSize.x)==board.Count-1){
                        id="Boss";
                    }
                    else if(Mathf.FloorToInt(i + j * dungeonSize.x)==0)
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

                    /*GameObject newRoom = Instantiate(_roomConfiguration.GetPrefabRoomId(id), new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();*/
                    RoomBehaviour rb = Object.Instantiate(_roomConfiguration.GetPrefabRoomId(id), new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity);
                    rb.UpdateRoom(currentCell);

                    rb.name += " " + i + "-" + j;                
                }
            }
        }
    }
}
