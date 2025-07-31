using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Bookings
{
    public static class PricingService
    {
        public static PricingDetails CalculatePrice(Apartment apartment,DateRange period)
        {
            var currency = apartment.Price.Currency;

            var priceForPeriod = new Money(
                apartment.Price.Amount * period.LengthInDays,
                currency);

            decimal percentageUpCharge = 0;
            foreach (var amenity in apartment.Amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.AirCondition => 0.01m,
                    Amenity.Parking => 0.01m,
                    _ => 0
                };
            }

            var amenitiesUpCharge = Money.Zero(currency);
            if (percentageUpCharge > 0)
            {
                amenitiesUpCharge = new Money(
                    priceForPeriod.Amount * percentageUpCharge,
                    currency);
            }

            var totalPrice = Money.Zero(currency);

            totalPrice += priceForPeriod;

            if (!apartment.CleaningFeeAmount.IsZero())
            {
                totalPrice += apartment.CleaningFeeAmount;
            }

            totalPrice += amenitiesUpCharge;

            return new PricingDetails(priceForPeriod, apartment.CleaningFeeAmount, amenitiesUpCharge, totalPrice);
        }
    }
}
