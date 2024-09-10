using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dungeon{
    public string nombre;// {get;set;}
    public float dungeonSizex; 
    public float dungeonSizey;
    public List<CellData> datos;// {set;get;}
}