namespace Application.Dtos.Country
{
    public class GetWholeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetAllProvinces> Children { get; set; }
    }
    public class GetAllProvinces
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<GetAllCities> Children { get; set; }
    }
    public class GetAllCities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public List<GetAllNeighbourhoods> Children { get; set; }
    }
    public class GetAllNeighbourhoods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }

    }
}
