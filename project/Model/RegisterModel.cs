using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BE;
using BL;

namespace project.Model
{
    public class RegisterModel
    {
        ImpBL myBL;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public RegisterModel()//constructor
        {
            myBL = new ImpBL();
        }
       
        public bool addUser(String emailAddress, String password, GENDER userGender, DateTime birthDate, FAMILYSTATUS familyStatus,
                int height,float currentWeight,DateTime lastUpdateCurrentWeight, float goalWeight)//this function create new user and save his details in db.
        {
            User user = new User(){EmailAddress=emailAddress,Password=password,Gender=userGender,BirthDate=birthDate,FamilyStatus=familyStatus,
            Height=height,CurrentWeight=currentWeight,GoalWeight=goalWeight, LastUpdateCurrentWeight=lastUpdateCurrentWeight};
           return myBL.addUser(user);
        }
    }
}
