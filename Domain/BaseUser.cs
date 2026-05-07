using System.ComponentModel.DataAnnotations;

namespace GymManagement.Domain
{
    public abstract class BaseUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
        //Navigation
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
}
