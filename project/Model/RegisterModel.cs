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
        ImpBL myBL;
        public RegisterModel()
        {
            myBL = new ImpBL();
        }
        float calculateBMI(float weightKG, float heightMeter )
        {
            return weightKG / (heightMeter * heightMeter);
        }
        public bool addUser(String emailAddress, String password, GENDER userGender, DateTime birthDate, FAMILYSTATUS familyStatus,
                int height,float currentWeight,DateTime LastUpdateCurrentWeight, float goalWeight)
        {
            float goalBmi=calculateBMI(goalWeight, height / 100);
            float currentBmi = calculateBMI(currentWeight, height / 100);
            if(currentBmi<18.5 && goalBmi<18.5 && goalBmi<currentBmi)
                Console.WriteLine("Cannot add you, your BMI is too low and your goal's BMI is lower:(");
            if (currentBmi > 25 && goalBmi > 25 && goalBmi > currentBmi)
                Console.WriteLine("Cannot add you, your BMI is too high and your goal's BMI is higher:(");
            User user = new User(){EmailAddress=emailAddress,Password=password,Gender=userGender,BirthDate=birthDate,FamilyStatus=familyStatus,
            Height=height,CurrentWeight=currentWeight,GoalWeight=goalWeight};
           return myBL.addUser(user);
        }
    }
}
