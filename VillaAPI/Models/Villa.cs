using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int Sqft { get; set; }

        public int Occupancy { get; set; }
    }
}
