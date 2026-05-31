using GymManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace GymManagement.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; } 
        public DateOnly HireDate { get; set; }

    }
}
