using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearchDomain.ValueObjects
{
    public record Address
    {
        public string City { get; init; }//Şehir
        public string District { get; init; }//Bölge
        public string Street { get; init; }//Sokak
        public string BuildingNumber { get; init; }//Numara
        public string ZipCode { get; init; }//Posta Kodu

        public string FullAddress => $"{Street} {BuildingNumber}, {District}, {City}, {ZipCode}";

        public Address(string street, string city, string district, string buildingNumber, string zipCode)
        {
            Street = street;
            City = city;
            District = district;
            BuildingNumber = buildingNumber;
            ZipCode = zipCode;
        }
    }
}
