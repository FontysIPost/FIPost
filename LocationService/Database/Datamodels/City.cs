using Newtonsoft.Json;
using System;

namespace LocatieService.Database.Datamodels
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; } = true;
    }
}
