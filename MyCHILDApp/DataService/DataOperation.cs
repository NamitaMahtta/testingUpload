using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyCHILDApp.Model;
using Newtonsoft.Json;

namespace MyCHILDApp.DataService
{
    public class DataOperation
    {
        HttpClient client = new HttpClient();
        public DataOperation()
        {
        }

        public async Task<IList<VitalForm>> GetVitalFormDataAsync()
        {
            var response = await client.GetStringAsync("http://127.0.0.1/APTProject_Graph/displayVitalFormData.php");
            var vitalFormData = JsonConvert.DeserializeObject<IList<VitalForm>>(response);
            return vitalFormData;
        }

        public async Task<IList<VitalForm>> AddTodoItemAsync(VitalForm itemToAdd)
        {
            var data = JsonConvert.SerializeObject(itemToAdd);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://127.0.0.1/APTProject_Graph/addVitalFormData.php?" +
                                                  "Temp=" + (itemToAdd.temp) + "&HR=" + (itemToAdd.heartRate) + 
                                                  "&RR=" + (itemToAdd.respiratoryRate) + "&OS=" + (itemToAdd.oxygenSat) + 
                                                  "&PS=" + (itemToAdd.painScore)+ "&FS=" + (itemToAdd.feedingStatus)+ 
                                                  "&MS=" + (itemToAdd.mentalStatus)+ "&Sei=" + (itemToAdd.seizure)+ 
                                                  "&CW=" + (itemToAdd.caregiverWorry), content);

            var placesJson = response.Content.ReadAsStringAsync().Result;
            //This line will throw error if there is a null value coming back from the database.
            var result = JsonConvert.DeserializeObject<IList<VitalForm>>(placesJson);
            return result;
        }

        /*public async Task<VitalForm> UpdateTodoItemAsync(int itemIndex, VitalForm itemToUpdate)
        {
            var data = JsonConvert.SerializeObject(itemToUpdate);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(string.Concat("http://localhost:5000/api/todo/", itemIndex), content);
            return JsonConvert.DeserializeObject<VitalForm>(response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Deletes the todo item async.
        /// </summary>
        /// <returns>The todo item async.</returns>
        /// <param name="itemIndex">Item index.</param>
        public async Task DeleteTodoItemAsync(int itemIndex)
        {
            await client.DeleteAsync(string.Concat("http://localhost:5000/api/todo/", itemIndex));
        }*/
    }
}
