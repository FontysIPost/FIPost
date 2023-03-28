using System;
using System.ComponentModel.DataAnnotations;

namespace LocatieService.Database.Datamodels
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BuildingId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
