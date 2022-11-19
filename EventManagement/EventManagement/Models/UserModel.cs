using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace EventManagement.Models
{
    //Scaffold-DbContext "Server=DESKTOP-RAKIB;Database=EventManagement;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataDB -force
    public class UserModel
	{
        [Required,DisplayName("First Name")]
        [StringLength(30, MinimumLength = 4)]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int? gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage ="Mobile number should be 10 digit")]
        public string? mobile { get; set; }
        public string? countrycode { get; set; }

        [Required, DisplayName("Email")]
        [EmailAddress]
        public string email { get; set; }

        [Required, DataType(DataType.Password),DisplayName("Password")]
        public string password { get; set; }

        [Required,DataType(DataType.Password), Compare(nameof(password),ErrorMessage ="Password and confirm password should be same."),DisplayName("Confirm Password")]
        public string confirmPassword { get; set; }
    }
    public class LoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
