using System.ComponentModel.DataAnnotations;

namespace Chemistry_laboratory_management.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }  // إضافة الدور
    }

}
