using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class Utils
    {
        public static bool ValidateEmail(string email)
        {
            var regexForEmailValidation = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
            Regex re = new Regex(regexForEmailValidation);
            if (re.IsMatch(email))
            {
                return true;
            }
            return false;
        }
    }
}