using AutoMapper;
using DALWithEF.Models;
using DTO;

namespace DALWithEF.Profiles
{
    public class MovieProfile: Profile
    {
        public MovieProfile() 
        {
            CreateMap<TblMovie, Movie>();
            CreateMap<Movie, TblMovie>();
        }
    }
}
