using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice
{
    [Table(nameof(Order))]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
