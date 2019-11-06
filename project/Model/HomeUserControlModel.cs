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
        ImpBL myBL;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public HomeUserControlModel()//constructor
        {
            myBL = new ImpBL();
        }
        public User getUser(string emailAddress)//get the user that it's email address equal to emailAddress
        {
            return myBL.getUser(emailAddress);
        }
    }
}
