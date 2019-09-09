using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    public class HomeUserControlModel
    {
        ImpBL myBL;
        public HomeUserControlModel()
        {
            myBL = new ImpBL();
        }
        public User getUser(string emailAddress)
        {
            return myBL.getUser(emailAddress);
        }
    }
}
