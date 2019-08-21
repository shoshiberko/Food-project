using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
namespace project.Model
{
    public class LogInModel
    {
        ImpBL myBL;
        public LogInModel()
        {
            myBL = new ImpBL();
        }

        public User getUser(string emailAddress)
        {
            return myBL.getUser(emailAddress);
        }
    }
}
