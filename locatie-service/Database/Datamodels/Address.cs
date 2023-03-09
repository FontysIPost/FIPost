using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LocatieService.Database.Datamodels
{
    public class Address : ValueObject
    {
        public Guid CityId { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Addition { get; set; }

        public Address() { }

        public Address(Guid cityId, string postalCode, string street, int number, string addition)
        {
            CityId = cityId;
            PostalCode = postalCode;
            Street = street;
            Number = number;
            Addition = addition;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CityId;
            yield return PostalCode;
            yield return Street;
            yield return Number;
            yield return Addition;
        }
    }
}
