using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    //0-Up, 1-Down, 2-Right, 3-Left
    [SerializeField] private GameObject[] wallsEntrance;
    [SerializeField] private GameObject[] doors;

    [SerializeField] private GameObject[] walls;

    //public bool[] testStatus;

   /* void Start()
    {
        UpdateRoom(testStatus);
    }*/
    
    /*public void UpdateRoom(bool[] status, bool[] WallStatus)
    {
        for (int i = 0; i < status.Length; i++)
        {
            if(!WallStatus[i])
            {
            doors[i].SetActive(status[i]);
            wallsEntrance[i].SetActive(!status[i]);
            }
            else
                walls[i].SetActive(false);
            
        }

    }*/
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            
            doors[i].SetActive(status[i]);
            wallsEntrance[i].SetActive(!status[i]);
          
            
        }

    }

}
