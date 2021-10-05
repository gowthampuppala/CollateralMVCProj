using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollateralMVC.Services
{
    public interface IUserService
    {
        string Login(string userName, string password);
        string EncodePassword(string pass);
    }
}
