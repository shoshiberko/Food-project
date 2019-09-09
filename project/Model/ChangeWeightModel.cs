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
        ImpBL myBl;
        public ChangeWeightModel()
        {
            myBl = new ImpBL();
        }
        public void SaveChanges(string currentWeightProperty, DateTime now,String emailAddress)
        {
            var user = myBl.getUser(emailAddress);
            var newUser = new User() { Age=user.Age, BirthDate=user.BirthDate, CurrentWeight=float.Parse(currentWeightProperty), EmailAddress=emailAddress, FamilyStatus=user.FamilyStatus, Gender=user.Gender, GoalWeight=user.GoalWeight, Height=user.Height, LastUpdateCurrentWeight=now, Password=user.Password };
            myBl.updateUser(newUser);
        }
    }
}
