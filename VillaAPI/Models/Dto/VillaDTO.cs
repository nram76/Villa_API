using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }

        // attributes are passed to variables
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int Sqft { get; set; }

        [Required]
        public int Occupancy { get; set; }

    }
}
