using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string _id;

    //0-Up, 1-Down, 2-Right, 3-Left
    public GameObject[] walls;
    public GameObject[] doors;

    public string Id => Id;
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }

    }

}
