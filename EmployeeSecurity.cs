using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace MyPracAPI
{
    public class EmployeeSecurity
    {
        public static bool LogIn(string Username,string password) 
        {
            using (MyEmployeeDetailsEntities entities = new MyEmployeeDetailsEntities())
            {
                return entities.Users.Any(user => user.UserName.Equals(Username, StringComparison.OrdinalIgnoreCase)
                && user.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}