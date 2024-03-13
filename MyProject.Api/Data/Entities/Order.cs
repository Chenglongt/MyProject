using System.ComponentModel.DataAnnotations;

namespace MyProject.Api.Data.Entities
{
    public class Order 
    {
        [Key]
        public long Id { get; set; }

        public DateTime OrderAt { get; set; } = DateTime.Now;
        public string CustomerId { get; set; }
        [Required, MaxLength(30)]
        public string CustomerName { get; set; }
        [Required, MaxLength(50)]
        public string CustomerEmail { get; set; }
        [Required, MaxLength(100)]
        public string CustomerIdAddress { get; set; }
        [Range(0.1, double.MaxValue)]
        public double TotalPrice { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }

    }
}
