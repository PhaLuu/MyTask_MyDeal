using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyTask.WebAPI.Models;
using System.IO;
namespace MyTask.WebAPI.Controllers
{
    [RoutePrefix("api/pnl")]
    public class MyControlController : ApiController
    {

        [HttpGet]
        [Route("getList")]
        public HttpResponseMessage GetList()
        {
            MyDataControl data = new MyDataControl();
            bool isDone = true;
            var physicalPath = System.Web.Hosting.HostingEnvironment.MapPath("/App_Data/data.csv");
            try
            {
                StreamReader sr = new StreamReader(physicalPath);
                string currentLine=null;
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (currentLine.StartsWith("1"))
                    {
                        string[] arrStr = currentLine.Substring(1).Split(new string[] { ".L/" }, StringSplitOptions.None);
                        if (arrStr.Count() >= 2)
                        {
                            if (arrStr[0].Length >= 3)
                            {
                                //for data: BALL/LINDAANNMRS-E2 
                                if (arrStr[0].ElementAt(arrStr[0].Length - 4) == '-')
                                {
                                    arrStr[0] = arrStr[0].Substring(0, arrStr[0].Length - 4);
                                }
                            }
                            Locator locator = new Locator();
                            locator.Name = arrStr[1];
                            Passenger passenger = new Passenger();
                            passenger.Name = arrStr[0];


                            Locator temp = CheckDuplicateName(data.locators, locator);
                            if (temp == null)
                            {
                                locator.Passengers.Add(passenger);
                                data.locators.Add(locator);
                            }
                            else
                            {
                                temp.Passengers.Add(passenger);
                            }
                        }
                        
                        
                    }
                }
                sr.Close();
            }
            catch(Exception ex)
            {
                isDone = false;   
            }
            if(isDone)
                return Request.CreateResponse(HttpStatusCode.OK, (List<Locator>)data.locators);
            else
                return Request.CreateResponse(HttpStatusCode.OK, 0);
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put(LocatorPassenger data)
        {
            if(data==null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, 0);
            }
            var physicalPath = System.Web.Hosting.HostingEnvironment.MapPath("/App_Data/data.csv");
            string strResult = "1" + data.PassengerName +" .L/" + data.LocatorName;
            try
            {
                StreamWriter sw = new StreamWriter(physicalPath, true);
                sw.WriteLine(strResult);
                sw.Close();
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, 0);
            }
            return Request.CreateResponse(HttpStatusCode.OK, 1);
        }
        private Locator CheckDuplicateName(List<Locator> list, Locator locator)
        {
            foreach(var temp in list)
            {
                if(temp.Name == locator.Name)
                {
                    return temp;
                }
            }
            return null;
        }
    }
}