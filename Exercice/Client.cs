using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice
{
    [Table(nameof(Client))]
    public record Client
    {
        [Key]
        public int ClientID { get; init; }

        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();

        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }

}
