using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaAPI.Models
{
    public class Villa

        // model defines format of the body as well as lining up columns for database
    {   
        // setup primary key and DB options
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Details { get; set; }

        public double Rate { get; set; }
        
        public string ImageUrl { get; set; }

        public string Amenity { get; set; } 

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int Sqft { get; set; }

        public int Occupancy { get; set; }
    }
}
