using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace project.ViewModel
{
    public class IntValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(((String)value)=="" || ((String)value) == null)
                return new ValidationResult(true, "Not a valid int");
            int result = 0;
            bool canConvert = int.TryParse(value as string, out result);
            if (result <= 0)
                canConvert = false;
            return new ValidationResult(canConvert, "Not a valid int");
        }
    }
}
