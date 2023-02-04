using HotelApi.ModelDtos.hotelDtos;

namespace HotelApi.ModelDtos.countryDtos
{
    public class CountryDto : BaseCountryDto
    {
        public int Id { get; set; }

        public List<HotelDto> Hotels { get; set; }
    }
}