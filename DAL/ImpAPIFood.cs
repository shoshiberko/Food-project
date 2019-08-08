using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class ImpAPIFood : IAPIFood
    {
        public List<FoodItem> getListFoodItems(String food)
        {
            //send request for list of foods that contain the string food parameter
            WebRequest request = WebRequest.Create(" https://api.nal.usda.gov/ndb/search/?format=xml&q=" + food + "&sort=n&max=25&offset=0&api_key=xjkHskmKWXGFzYR2O2czyY5jWhmeaD7puagyHI5L");
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sReader = new StreamReader(dataStream);
            string output = sReader.ReadToEnd();
            //xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(output);
            XmlNodeList itemList = xmlDoc.SelectNodes("/list/item");
            List<FoodItem> resultList = new List<FoodItem>();
            foreach (XmlNode xmlItem in itemList)
            {
                resultList.Add(new FoodItem { Key = xmlItem["ndbno"].InnerText, Name = xmlItem["name"].InnerText });
            }
            return resultList;

        }
        public FoodDetails getFoodDetails(string keyFood)
        {
            //send request for list of foods that it's key is the string keyFood parameter
            WebRequest request = WebRequest.Create(" https://api.nal.usda.gov/ndb/V2/reports?ndbno=" + keyFood + "&type=b&format=xml&api_key=xjkHskmKWXGFzYR2O2czyY5jWhmeaD7puagyHI5L");
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sReader = new StreamReader(dataStream);
            string output = sReader.ReadToEnd();

            //xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(output);
            //make a foodDetails object tat contain the relevant details.
            XmlNodeList nutrientsList = xmlDoc.SelectNodes("/foods/food/nutrients/nutrient");
            FoodDetails foodDetails = new FoodDetails();
            foodDetails.Water = float.Parse(nutrientsList[0]["value"].InnerText);
            foodDetails.Calories = float.Parse(nutrientsList[1]["value"].InnerText);
            foodDetails.Protien = float.Parse(nutrientsList[2]["value"].InnerText);
            foodDetails.Fats = float.Parse(nutrientsList[3]["value"].InnerText);
            foodDetails.Carbohydrate = float.Parse(nutrientsList[4]["value"].InnerText);
            foodDetails.Fiber = float.Parse(nutrientsList[5]["value"].InnerText);
            foodDetails.Sugars = float.Parse(nutrientsList[6]["value"].InnerText);
            foodDetails.Sodium = float.Parse(nutrientsList[12]["value"].InnerText);
            float vitamins = 0;
            for (int i = 14; i < 27; i++)
                vitamins += float.Parse(nutrientsList[i]["value"].InnerText);
            foodDetails.Vitamins = vitamins;
            return foodDetails;
        }
    }
}
