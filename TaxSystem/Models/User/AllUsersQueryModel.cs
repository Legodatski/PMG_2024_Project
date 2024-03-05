using System.ComponentModel.DataAnnotations;
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
        public string SearchTerm { get; set; } = null;

        [Display(Name = "Наменование на роля")]
        public string RoleName { get; set; } = null;

        public IEnumerable<UserViewModel> Users { get; set; }

    }
}
