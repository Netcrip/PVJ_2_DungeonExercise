using Refactoring;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DungeonGenerator;

public class RoomFactory : MonoBehaviour
{
    [SerializeField] private GameObject[] roms;

   /* public Room Create(int _dungeonX, int _dungeonY)
        {
        for (int i = 0; i < _dungeonX; i++)
        {
            for (int j = 0; j < _dungeonY; j++)
            {
                Cell currentCell = _board[Mathf.FloorToInt(i + j * _dungeonSize.x)];

                if (currentCell.visited)
                {
                    int randomRoom = Random.Range(0, _rooms.Length);

                    GameObject newRoom = Instantiate(_rooms[randomRoom], new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    rb.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }
        return null;
    }*/
}
