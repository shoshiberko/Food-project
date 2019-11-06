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
        ImpBL myBL;//all the function use the bl that has the access to DAL function and from there to the db. 
        public MainWindowModel()//constructor
        {
            myBL = new ImpBL();

        }
        public User getUser(string emailAddressProperty)//get the user that emailAddressProperty it's mail
        {
            return myBL.getUser(emailAddressProperty);
        }
    }
}
