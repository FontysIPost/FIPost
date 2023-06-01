using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace PakketService
{
    public class logData
    {
        public string personID { get; set; }
        public string PakketName { get; set; }
        public string ReceiverID { get; set; }
        public string Sender { get; set; }
        public string LocationID { get; set; }
        public DateTime Time { get; set; }


        public logData(string personID, string PakketName, string ReceiverID, string Sender, string LocationID, DateTime time)
        {
            this.personID = personID;
            this.PakketName = PakketName;
            this.ReceiverID = ReceiverID;
            this.Sender = Sender;
            this.LocationID = LocationID;
            this.Time = time;

        }

        public static async Task LogToMicroserviceAsync(logData data, string url)
        {
            using (var httpClient = new HttpClient())
            {


                var jsonPayload = JsonConvert.SerializeObject(data);
                var payload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, payload);

                // Check the response status code
                if (!response.IsSuccessStatusCode)
                {
                    // Log an error message if the request failed
                    Console.WriteLine("Failed to log to microservice");
                }
            }
        }

    }
}
