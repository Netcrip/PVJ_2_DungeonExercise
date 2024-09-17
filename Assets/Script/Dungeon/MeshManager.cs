using UnityEngine;
using Unity.AI.Navigation;
using System;

public class MeshManager{




    private void GenerateDungeonObjet(){
        GameObject roomContainer = new GameObject("RoomContainer");
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");

        foreach(GameObject room in rooms){
            room.transform.SetParent(roomContainer.transform);
        }
    }

    public void GenerateNavMesh(){
        GenerateDungeonObjet();
        GameObject roomContainer = GameObject.Find("RoomContainer");
        NavMeshSurface navMeshSurface = roomContainer.AddComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }

     
}