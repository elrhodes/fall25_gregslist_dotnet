using System.ComponentModel.DataAnnotations;

namespace gregslist_api_dotnet.Models;

public class House
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [Range(0, 100000000)] public int? Price { get; set; }
    [Url, MaxLength(500)] public string ImgUrl { get; set; }
    [MaxLength(500)] public string Description { get; set; }
    [Range(1, 20)] public int? Bedrooms { get; set; }
    [Range(1, 20)] public int? Bathrooms { get; set; }
    [Range(100, 100000)] public int? SqFt { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
}