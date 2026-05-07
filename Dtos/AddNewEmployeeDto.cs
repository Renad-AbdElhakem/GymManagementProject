using GymManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace GymManagement.Dtos
{
    public record AddNewEmployeeDto
    {

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateOnly HireDate { get; set; }
      
        public decimal Salary { get; set; }

        public int RoleId { get; set; }

   
   
    }
}
