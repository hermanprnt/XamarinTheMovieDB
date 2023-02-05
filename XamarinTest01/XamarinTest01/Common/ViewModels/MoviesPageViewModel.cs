using XamarinTest01.Common.Models;
using XamarinTest01.Common.Repository;
using XamarinTest01.Common.Constants;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinTest01.Common.Views;

namespace XamarinTest01.Common.Viewmodels
{
    class MoviesPageViewModel : BindableBase
    {
        private int page = 1;
        private INavigationService _navigationService;
        private ObservableCollection<Movie> listMovies = new ObservableCollection<Movie>();
        IMovieRepository repository = DependencyService.Get<IMovieRepository>();
        public Command RefreshMoviesCommand { get; set; }
        public Command MovieTresholdReachedCommand { get; set; }

        private int _movieTreshold;
        public int MovieTreshold
        {
            get => _movieTreshold;
            set => SetProperty(ref _movieTreshold, value);
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public ObservableCollection<Movie> allMovies { get; set; }

        private bool isLoadingData;


        public bool IsLoadingData
        {
            get => isLoadingData;
            set => SetProperty(ref isLoadingData, value);
        }

        public MoviesPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            allMovies = new ObservableCollection<Movie>();
            MovieTreshold = 1;
            FetchMoviesAsync();
            MovieTresholdReachedCommand = new Command(async () => await MoviesTresholdReached());
            RefreshMoviesCommand = new Command(async () =>
            {
                await FetchMoviesAsync();
                IsRefreshing = false;
            });
        }

        public async Task FetchMoviesAsync()
        {
            if (IsLoadingData)
            {
                return;
            }
            IsLoadingData = true;
            try
            {
                MovieTreshold = 1;
                page = 1;
                listMovies.Clear();
                allMovies.Clear();
                listMovies = await repository.GetMovies(new MovieCall(ApiKeys.NOW_PLAYING, page));
                if (listMovies != null)
                {
                    foreach (Movie movie in listMovies)
                    {
                        if (movie != null && !allMovies.Contains(movie))
                        {
                            allMovies.Add(movie);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                IsLoadingData = false;
            }
        }

        private async Task MoviesTresholdReached()
        {
            if (IsLoadingData)
            {
                return;
            }
            page++;
            IsLoadingData = true;
            try
            {
                listMovies = await repository.GetMovies(new MovieCall(ApiKeys.NOW_PLAYING, page));
                foreach (Movie movie in listMovies)
                {
                    if (movie != null && !allMovies.Contains(movie))
                    {
                        allMovies.Add(movie);
                    }
                }
                if (listMovies.Count() == 0)
                {
                    MovieTreshold = -1;
                    return;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                IsLoadingData = false;
            }
        }

        public Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                OpenMovieDetail();
            }
        }

        public void OpenMovieDetail()
        {
            var par = new NavigationParameters();
            par.Add(Constant.MOVIE, _selectedMovie);
            _navigationService.NavigateAsync(nameof(MovieDetailsPage), par);
        }
    }
}
