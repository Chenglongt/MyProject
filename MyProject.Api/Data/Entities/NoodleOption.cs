using System.ComponentModel.DataAnnotations;

namespace MyProject.Api.Data.Entities
{
    public class NoodleOption 
    {
        
        public int NoodleId { get; set; }

        [Required,MaxLength(40)]
        public string Flavor { get; set; }
        [Required, MaxLength(40)]
        public string Topping { get; set; }
        public virtual Noodle Noodle { get; set; }
    }
}
