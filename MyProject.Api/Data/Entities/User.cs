using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace MyProject.Api.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(100)]
       
        public string Address { get; set; }
        [Required,MaxLength(20)]
        public string Salt { get; set; }
        [Required, MaxLength(200)]
        public string Hash { get; set; }
    }
}
