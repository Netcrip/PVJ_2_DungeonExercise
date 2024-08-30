using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [SerializeField] private string id;
    //0-Up, 1-Down, 2-Right, 3-Left
    [SerializeField] private GameObject[] wallsEntrance;
    
    [SerializeField] private GameObject[] doors;

    [SerializeField] private GameObject[] walls;

    [SerializeField] private GameObject[] pilars;
    public void UpdateRoom(Cell currentCell)
    {
        for (int i = 0; i < currentCell.status.Length; i++)
        {

            if (currentCell.pillarStatus[i])
            {
                pilars[i].SetActive(true);
            }
            if (currentCell.wallStatus[i])
            {
                walls[i].SetActive(false);
            }
            else
            {
                doors[i].SetActive(currentCell.status[i]);
                wallsEntrance[i].SetActive(!currentCell.status[i]);
            }
            

        }

    }

}
