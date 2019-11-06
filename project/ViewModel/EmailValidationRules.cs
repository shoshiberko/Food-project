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
    
    class EmailValidationRules : ValidationRule//this class define the email Address validation
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)//this function match the input string to the email validation rules. the connection to the email address text box is from xml-when the binding done(Binding.ValidationRules)
            //this function make an red sign when the input is illegal.
        {
            if (((String)value) == "" || ((String)value) == null)
                return new ValidationResult(true, "Not a valid email");
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            bool canConvert = regex.IsMatch((String)value); 
            return new ValidationResult(canConvert, "Not a valid email");
        }
    }
}
