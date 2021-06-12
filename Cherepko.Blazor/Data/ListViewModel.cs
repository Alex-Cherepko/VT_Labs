using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cherepko.Blazor.Data
{
public class ListViewModel
{ 
        [JsonPropertyName("rodId")]
        public int RodId { get; set; } 
        [JsonPropertyName("rodName")]
        public string RodName  { get; set; } 
}
public class DetailsViewModel
{
    [JsonPropertyName("rodName")]
    public string RodName { get; set; } 
    [JsonPropertyName("description")]
    public string Description { get; set; } 
    [JsonPropertyName("price")]
    public float Price { get; set; } 
    [JsonPropertyName("image")]
    public string Image { get; set; } 
}
}
