using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RadioManager.Helpers.Validadores
{
    public class Validator
    {
        public static bool EmailStructureValidator(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool checkPasswordLength(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool CompleteTextbox(string name, string lastName, string personalNumber, string userName)
        {
            if (name.Length > 3 && lastName.Length > 3 && personalNumber.Length > 3 && userName.Length > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}