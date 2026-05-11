using System.ComponentModel.DataAnnotations;

namespace GymManagement.Domain
{
    public  abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
