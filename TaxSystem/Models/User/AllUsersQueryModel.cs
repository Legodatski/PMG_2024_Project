using TaxSystem.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace TaxSystem.Models.Users
{
    public class AllUsersQueryModel
    {
        public AllUsersQueryModel()
        {
            Users = new HashSet<ApplicationUser>();
        }

        public  int UsersPerPage { get; set; } = 5;

        public int CurrentPage { get; set; } = 1;

        public string SearchTerm { get; set; } = null;

        public string RoleName { get; set; } = null;

        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}
