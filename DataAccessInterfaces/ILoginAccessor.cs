using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface ILoginAccessor
    {
        int InsertLogin(Login login);
        int UpdateLogin(Login login);
        int DeactivateLogin(string username);
        int ReactivateLogin(string username);
        string[] AuthenticateClientForSecurityQuestions(string username, byte[] passwordHash);
        Client_VM AuthenticateClientWithSecurityResponses(
            string username,
            byte[] passwordHash,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);
        string[] AuthenticateEmployeeForSecurityQuestions(string username, byte[] passwordHash);
        Employee_VM AuthenticateEmployeeWithSecurityResponses(
            string username,
            byte[] passwordHash,
            string securityResponse1,
            string securityResponse2,
            string securityResponse3);
    }
}
