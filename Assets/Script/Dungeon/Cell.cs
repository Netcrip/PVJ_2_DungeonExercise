public class Cell
{
    public bool visited { get; set;}
    
    public bool generate { get; set; }

    public bool[] status { get; set; }

    public bool[] wallStatus { get; set; }

    public bool[] pillarStatus { get; set; }

    public string roomId { get; set; }

    public Cell()
    {
        visited=false;
        generate=false;
        status=new bool[4];
        wallStatus=new bool[4];
        pillarStatus=new bool[4];
    }

}
