namespace Context.ViewModels;

public class UrlStorageResponse
{
    public Guid Id { get; set; }
    public string UrlReal { get; set; }
    public string UrlShortest { get; set; }
    public bool? WithPass { get; set; }
}