using DALWithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace DALWithEF.Context
{
    public class IMDBContext: Imdb20241Context
    {
        private readonly string _connectionString;

        public IMDBContext(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(_connectionString);
    }
}
