using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;



public class SaveDungeon { 
    public void Save(List<Cell> board, string name,Vector2 dungeonSize){

        string filePath = Application.dataPath + "/Files/Dungeons.json";
        DungeonFileLoader dungeonFile = new DungeonFileLoader(filePath);
        DungeonFileSerializer dungeonSerializer = new DungeonFileSerializer();
        bool boardCharge=false;

        foreach(Cell c in board){
            if (c.roomId!=null && !boardCharge){
                boardCharge = true;
            }
        }


        if(boardCharge){
                List<CellData> boardData = dungeonSerializer.FileSerializer(board);

            try
            {
                Dungeons dungeonsJson;
                string jsonContent = dungeonFile.LoadDungeonsFile();
                if(jsonContent!=null)
                    {
                        dungeonsJson = JsonUtility.FromJson<Dungeons>(jsonContent);; 
                    }  
                else
                    {
                        dungeonsJson = new Dungeons();
                        dungeonsJson.dungeons = new Dungeon[0];
                    }
                Dungeon newDungeon =new Dungeon();
                newDungeon.nombre=name;
                newDungeon.dungeonSizex=dungeonSize.x;
                newDungeon.dungeonSizey=dungeonSize.y;
                newDungeon.datos=boardData;
                
                List<Dungeon> tempDungeonsList = new List<Dungeon>(dungeonsJson.dungeons)
                {
                    newDungeon
                };
                dungeonsJson.dungeons = tempDungeonsList.ToArray();
                
                string updatedJson = JsonUtility.ToJson(dungeonsJson);

                
                File.WriteAllText(filePath, updatedJson);

            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error al guardar el archivo JSON: " + ex.Message);

            }

        }
        
    }
}
