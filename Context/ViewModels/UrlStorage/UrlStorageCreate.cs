namespace Context.ViewModels;

public class UrlStorageCreate
{
    public string UrlReal { get; set; }
    public Guid? UserId { get; set; }
    public bool? WithPass { get; set; }
    public string? SitePass { get; set; }
}