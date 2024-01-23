using TaxSystem.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace TaxSystem.Models.User
{
    public class AllUsersQueryModel
    {
        public AllUsersQueryModel()
        {
            Users = new HashSet<UserViewModel>();
        }

        public  int UsersPerPage { get; set; } = 5;

        public int CurrentPage { get; set; } = 1;

        public string SearchTerm { get; set; } = null;

        public string RoleName { get; set; } = null;

        public IEnumerable<UserViewModel> Users { get; set; }

    }
}
