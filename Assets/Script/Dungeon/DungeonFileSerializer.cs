using System.Collections.Generic;
using UnityEngine;

public class DungeonFileSerializer{
    public List<CellData> FileSerializer(List<Cell> board){
        
        List<CellData> boardData = new List<CellData>(); 
        
        foreach(Cell c in board){
            CellData cellData = new CellData();
            cellData.roomId=c.roomId;
            cellData.visited =c.visited;
            cellData.generate=c.generate;
            cellData.status= c.status;
            cellData.wallStatus = c.wallStatus;
            cellData.pillarStatus=c.pillarStatus;
            boardData.Add(cellData);
        }

        return boardData;
    }
}
