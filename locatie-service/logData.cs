
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;



namespace LocatieService
{
    public class logData
    {
        public string Name { get; set; }
        public string locatieType { get; set; }
        public DateTime Time { get; set; }




        public logData(string name, string locatieType, DateTime time )
        {
            this.Name = name;
            this.locatieType = locatieType;
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
