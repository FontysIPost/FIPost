using System.ComponentModel.DataAnnotations;

namespace LocatieService.Database.Datamodels.Dtos
{
    public class CityRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
