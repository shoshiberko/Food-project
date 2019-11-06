using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    public class ChangeWeightModel
    {
        ImpBL myBl;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public ChangeWeightModel()//constructor
        {
            myBl = new ImpBL();
        }
        public void SaveChanges(string currentWeightProperty, DateTime now,String emailAddress)//this function get currentWeight, date and email and save the new weight and LastUpdateDate in the details of the email-user.
        {
            var user = myBl.getUser(emailAddress);
            var newUser = new User() { Age=user.Age, BirthDate=user.BirthDate, CurrentWeight=float.Parse(currentWeightProperty), EmailAddress=emailAddress, FamilyStatus=user.FamilyStatus, Gender=user.Gender, GoalWeight=user.GoalWeight, Height=user.Height, LastUpdateCurrentWeight=now, Password=user.Password };
            myBl.updateUser(newUser);
        }
    }
}
