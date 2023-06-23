using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        // DTO Data Transfer Object attributes based on model of body being sent i.e. matches Villa.cs
        // this is the FE data that the user will see do NOT expose protected data here
        public int Id { get; set; }

        // attributes are passed to variables
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int Sqft { get; set; }

        [Required]
        public int Occupancy { get; set; }

        public string Details { get; set; }

        [Required]
        public double Rate { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }

    }
}
