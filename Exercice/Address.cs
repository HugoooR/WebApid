using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice
{
    [Table(nameof(Address))]
    public record Address
    {
        [Key]
        public int AddressID { get; init; }

        [Required]
        [StringLength(100)]
        public required string Street { get; set; }

        [Required]
        [StringLength(10)]
        public required string ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        public required string City { get; set; }

        [Required]
        [StringLength(50)]
        public required string Country { get; set; }

        public int ClientID { get; set; }

        [ForeignKey(nameof(ClientID))]
        public Client? Client { get; set; }
    }
}
