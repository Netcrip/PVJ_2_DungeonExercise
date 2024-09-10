[System.Serializable]
public class CellData{
    public bool visited;
    
    public bool generate; 

    public bool[] status; 

    public bool[] wallStatus; 

    public bool[] pillarStatus; 

    public string roomId; 
}