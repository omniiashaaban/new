namespace Chemistry_laboratory_management.Dtos
{
    public class UserDto
    {
        //public string Token { get; set; }
        //public string Email { get; set; }
          
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Role { get; set; }  // طالب - أستاذ - فني
            public int AccessLevel { get; set; } // مستوى الوصول للمختبر
        }
    }


