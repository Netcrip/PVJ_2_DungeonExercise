
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "custom/Room configuration")]
public class RoomConfiguration : ScriptableObject
{
    [SerializeField] private RoomBehaviour[] _rooms;

    private Dictionary<string, RoomBehaviour> _idRoom;

    private void Awake()
    {
        _idRoom = new Dictionary<string, RoomBehaviour>();
        foreach (var room in _rooms)
        {
            _idRoom.Add(room.Id, room);
        }
    }
 

    public RoomBehaviour GetPrefabRoomId(string id)
    {
        if(!_idRoom.TryGetValue(id,out var room))
        {
            throw new Exception($"Room whit id {id} dose not exist");
        }
        return room;
    }
    public int GetCount(){
        return _idRoom.Count;
    }
}

