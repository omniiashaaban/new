using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory.BLL.Services
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(string email, string displayName, IList<string> roles);
    }
}
