using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    public class MainWindowModel
    {
        ImpBL myBL;
        public MainWindowModel()
        {
            myBL = new ImpBL();

        }
        public User getUser(string emailAddressProperty)
        {
            return myBL.getUser(emailAddressProperty);
        }
    }
}
