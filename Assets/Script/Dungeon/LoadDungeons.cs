using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LoadDungeons{
     public Dictionary<string,MyDungeons> LoadDungeon(){    

        string filePath = Application.dataPath + "/Files/Dungeons.json";

        DungeonFileLoader dungeonFile= new DungeonFileLoader(filePath);
        DungeonFileDeserializer  dungeonDeserialize= new DungeonFileDeserializer();
                 
        string jsonContent = dungeonFile.LoadDungeonsFile();

          if(jsonContent!=null)
            return dungeonDeserialize.fileDeserializer(jsonContent); 
        return null;
    } 
}