using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using JHilburn.FabricMgr.Models;
using Newtonsoft.Json;
using System.Text;

namespace JHilburn.FabricMgr.Services
{
    public class FabricsService
    {
        private readonly HttpClient _client;

        public FabricsService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:3000/api/fabrics");
            _client.DefaultRequestHeaders.Add("x-api-auth", "F1467AA76C4776F69D165BD598812");

        }


        public async Task<IEnumerable<FabricViewModel>> GetAllAsync()
        {
            var _fabrics = new List<FabricViewModel>();
            var res = await _client.GetAsync(_client.BaseAddress);

            if (res.IsSuccessStatusCode)
            {
                _fabrics = await res.Content.ReadAsAsync<List<FabricViewModel>>();
            }

            return _fabrics;
        }


        public async Task<FabricViewModel> GetFabricAsync(int id)
        {
            FabricViewModel _fabric = null;
            Task<HttpResponseMessage> t = _client.GetAsync(string.Format($@"{_client.BaseAddress}/{id}"));
            var res = t.Result;

            if (res.IsSuccessStatusCode)
            {
                _fabric = await res.Content.ReadAsAsync<FabricViewModel>();
            }

            return _fabric;
        }


        public async Task<Uri> AddFabricAsync(FabricViewModel fabric)
        {
            string obj = JsonConvert.SerializeObject(fabric);
            var content = new StringContent(obj, Encoding.UTF8, "application/json");
            var res = await _client.PostAsync(_client.BaseAddress, content);
            Console.WriteLine(res.ToString());
            res.EnsureSuccessStatusCode();
            return res.Headers.Location;
        }


        public async Task<FabricViewModel> UpdateFabric(FabricViewModel fabric)
        {
            string obj = JsonConvert.SerializeObject(fabric);
            var content = new StringContent(obj, Encoding.UTF8, "application/json");
            var res = await _client.PutAsJsonAsync(string.Format($@"{_client.BaseAddress}/{fabric.id}"), content);
            res.EnsureSuccessStatusCode();
            var updatedfabric = await res.Content.ReadAsAsync<FabricViewModel>();
            return updatedfabric;
        }


        public async Task<HttpStatusCode> Delete(int id)
        {
            var res = await _client.DeleteAsync(string.Format($@"{_client.BaseAddress}/{id}"));
            return res.StatusCode;
        }
    }
}
