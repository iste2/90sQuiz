namespace Quiz90s.GameData;

public class Player
{
    public string PlayerId => Name + ImgUrl;
    public string Name { get; set; }
    public string ImgUrl { get; set; }
}