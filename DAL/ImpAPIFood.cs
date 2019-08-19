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
            XmlNodeList stamList;
            FoodDetails foodDetails = new FoodDetails();
            /*foodDetails.Water = float.Parse(nutrientsList[0]["value"].InnerText);
                foodDetails.Calories = float.Parse(nutrientsList[1]["value"].InnerText);
                foodDetails.Protien = float.Parse(nutrientsList[2]["value"].InnerText);
                foodDetails.Fats = float.Parse(nutrientsList[3]["value"].InnerText);
                foodDetails.Carbohydrate = float.Parse(nutrientsList[4]["value"].InnerText);
                foodDetails.Fiber = float.Parse(nutrientsList[5]["value"].InnerText);
                foodDetails.Sugars = float.Parse(nutrientsList[6]["value"].InnerText);
                foodDetails.Sodium = float.Parse(nutrientsList[12]["value"].InnerText);
                float vitamins = 0;
                for (int i = 14; i < 27; i++)
                    vitamins += float.Parse(nutrientsList[i]["value"].InnerText);*/
           float vitamins = 0;
            string name="";
            string value="";
            for (int i = 0; i < nutrientsList.Count; i++)
            {


                foreach (var item in (nutrientsList[i]).Attributes)
                {
                    if ((((XmlAttribute)item).Name).Equals("name"))
                    {
                        name = (((XmlAttribute)item).Value);

                    }
                    else if ((((XmlAttribute)item).Name).Equals("value"))
                    {
                        value = (((XmlAttribute)item).Value);
                    }
                    else if ((((XmlAttribute)item).Name).Equals("group") && (((XmlAttribute)item).Value).Equals("Vitamins"))
                    {
                        name = "Vitamins";
                    }
                }
                switch (name)
                {
                    case "Energy":
                        foodDetails.Calories = float.Parse(value);
                        break;
                    case "Water":
                        foodDetails.Water = float.Parse(value);
                        break;
                    case "Sodium, Na":
                        foodDetails.Sodium = float.Parse(value);
                        break;
                    case "Protein":
                        foodDetails.Protien = float.Parse(value);
                        break;
                    case "Total lipid (fat)":
                        foodDetails.Fats = float.Parse(value);
                        break;
                    case "Fiber, total dietary":
                        foodDetails.Fiber = float.Parse(value);
                        break;
                    case "Carbohydrate, by difference":
                        foodDetails.Carbohydrate = float.Parse(value);
                        break;
                    case "Sugars, total":
                        foodDetails.Sugars = float.Parse(value);
                        break;
                    case "Vitamins":
                        vitamins += float.Parse(value);
                        break;
                    default:
                        break;
                }
            }


                //}
               /* if ((nutrientsList[i]["name"].InnerText).Equals("Energy"))
                {
                    
                }
                else if ((nutrientsList[i]["name"].InnerText).Equals("Protien"))
                {
                    
                }
                else if ((nutrientsList[i]["name"].InnerText).Equals("Total lipid (fat)"))
                {
                    
                }
                else if ((nutrientsList[i]["name"].InnerText).Equals("Carbohydrate, by difference"))
                {
                    
                }
                else if ((nutrientsList[i]["name"].InnerText).Equals("Fiber, total dietary"))
                {
                    
                }
                else if ((nutrientsList[i]["name"].InnerText).Equals("Sugars, total"))
                {
                    
                }
                else if ((nutrientsList[i]["name"].InnerText).Equals("Sodium, Na"))
                {
                }
                else
                {
                    if(nutrientsList[i]["name"].InnerText.Contains("Vitamin"))
                    {
                        vitamins += float.Parse(nutrientsList[i]["value"].InnerText);
                    }
                }
            }*/
            foodDetails.Vitamins = vitamins;
            return foodDetails;
        }
    }
}
