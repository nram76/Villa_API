using VillaAPI.Models.Dto;
namespace VillaAPI.Data
{
    public static class VillaStore
    {
        // returns contents of request in array format
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO { Id = 1, Name = "Pool View", Sqft=1500, Occupancy=4 },
                new VillaDTO { Id = 2, Name = "Beach View", Sqft=1200, Occupancy=3 }
            };
    }
}
