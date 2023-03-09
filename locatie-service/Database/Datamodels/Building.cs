using System;

namespace LocatieService.Database.Datamodels
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
