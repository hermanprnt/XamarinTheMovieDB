using System.Collections.ObjectModel;

namespace XamarinTest01.Common.Models
{
    class MovieResponse
    {
        public int page { get; set; }
        public ObservableCollection<Movie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
        public string status_message { get; set; }
    }
}
