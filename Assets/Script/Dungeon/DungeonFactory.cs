using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonFactory : MonoBehaviour
{
   [SerializeField] private GameObject[] _rooms;

   [SerializeField] private RoomConfiguration _roomConfiguration;


    public void  Create(Vector2 dungeonSize, List<Cell> board,Vector2 offset)
    {
        for (int i = 0; i < dungeonSize.x; i++)
        {
            for (int j = 0; j < dungeonSize.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * dungeonSize.x)];

                if (currentCell.visited || currentCell.generate)
                {
                    int randomRoom = Random.Range(0, _rooms.Length);
                    string id="default";
                    switch (randomRoom)
                    {
                        case 0:
                            id="Vacion";
                            break;
                        case 1:
                            id="Jarron";
                            break; ;
                    }

                    GameObject newRoom = Instantiate(/*_roomConfiguration.GetPrefabRoom(id)*/_rooms[0], new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    rb.UpdateRoom(currentCell);

                    newRoom.name += " " + i + "-" + j;                
                }
            }
        }
    }
}
