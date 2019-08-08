using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace project.ViewModel
{
    
    class EmailValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (((String)value) == "" || ((String)value) == null)
                return new ValidationResult(true, "Not a valid email");
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool canConvert = regex.IsMatch((String)value); 
            return new ValidationResult(canConvert, "Not a valid float");
        }
    }
}
