using System.Collections.Generic;
using UnityEngine;

public class DungeonFileDeserializer{
    public Dictionary<string,MyDungeons> fileDeserializer(string jsonContent){
        List<Cell> board;
        Cell cell;
        Dictionary<string,MyDungeons> myDungeons = new Dictionary<string, MyDungeons>();
            try
            {                
                Dungeons dungeonsJson = JsonUtility.FromJson<Dungeons>(jsonContent);
                foreach(Dungeon d in dungeonsJson.dungeons){
                    board = new List<Cell>(); 
                    foreach(CellData cd in d.datos){
                        cell = new Cell();
                        cell.roomId=cd.roomId;
                        cell.visited =cd.visited;
                        cell.generate=cd.generate;
                        cell.status= cd.status;
                        cell.wallStatus = cd.wallStatus;
                        cell.pillarStatus=cd.pillarStatus;
                        board.Add(cell);
                    }  
                    MyDungeons dungeon = new MyDungeons();
                    dungeon.board=board;
                    dungeon.dungeonSizex=d.dungeonSizex;
                    dungeon.dungeonSizey=d.dungeonSizey;
                    myDungeons.Add(d.nombre,dungeon);
                }
                return myDungeons;

            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error al leer o deserializar el archivo JSON: " + ex.Message);
                return myDungeons;

            }

    }
}