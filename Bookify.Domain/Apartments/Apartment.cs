using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartments
{
    public sealed class Apartment : Entity
    {
        public Apartment(Guid id) : base(id)
        {
        }

        public Name Name { get; set; }
        public Description Description { get; set; }
        public Address Address { get; set; }
        public Money Price { get; set; }
        //public string PriceCurrecncy { get; set; }
        public Money CleaningFeeAmount { get; set; }
        //public decimal CleaningFeeAmount { get; set; }
        //public string CleaningFeeCurrency { get; set; }
        public DateTime? LastBookedOnUtc { get; set; }
        public List<Amenity> Amenities { get; set; } = new();
    }
}
