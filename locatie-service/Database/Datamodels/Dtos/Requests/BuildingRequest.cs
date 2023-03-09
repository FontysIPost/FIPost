using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace LocatieService.Database.Datamodels.Dtos
{
    public class BuildingRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Address Address { get; set; }
    }
}
