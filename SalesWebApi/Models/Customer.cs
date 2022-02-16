using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebApi.Models {

    public class Customer {
        // Properties
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Name { get; set; }
        public bool Active { get; set; }
        [Required, StringLength(2)]
        public string StateCode { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Sales { get; set; }

        public Customer() { } // Default Constructor
    }
}
