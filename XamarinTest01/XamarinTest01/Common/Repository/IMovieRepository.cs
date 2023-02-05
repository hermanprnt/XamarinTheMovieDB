using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamarinTest01.Common.Models;

namespace XamarinTest01.Common.Repository
{
    public interface IMovieRepository
    {
        Task<ObservableCollection<Movie>> GetMovies(MovieCall movieCall);
    }
}
