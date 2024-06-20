using System.ComponentModel.DataAnnotations;

namespace Context.Models;

public class UrlStorage
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UrlReal { get; set; }
    public string UrlShortest { get; set; }
}