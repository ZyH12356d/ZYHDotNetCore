namespace ZYHDotNetCore.BirdAPI.Models.BirdModel;

public class BirdResponseModel
{
    public Tbl_Bird[] Tbl_Bird { get; set; }
}

public class Tbl_Bird
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}    
