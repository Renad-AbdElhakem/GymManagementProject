namespace GymManagement.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }


        //Navigation
        public ICollection<BaseUser>? BaseUsers = new List<BaseUser>();


    }
}
