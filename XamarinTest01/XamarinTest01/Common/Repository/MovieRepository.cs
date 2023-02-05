using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinTest01.Common.Constants;
using XamarinTest01.Common.Models;

namespace XamarinTest01.Common.Repository
{
    class MovieRepository : IMovieRepository
    {
        public async Task<ObservableCollection<Movie>> GetMovies(MovieCall movieCall)
        {
            string url = string.Format(Constant.API_FORMAT, ApiKeys.BASE_URL, movieCall.Type, ApiKeys.API_KEY, movieCall.Page);
            HttpClient clientCabinets = new HttpClient();
            try
            {
                HttpResponseMessage response = await clientCabinets.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    MovieResponse movieResponse = JsonConvert.DeserializeObject<MovieResponse>(result);
                    return movieResponse.results;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}
