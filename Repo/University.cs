using Microsoft.EntityFrameworkCore;
using WebApplication1.Model.DataModels;
using WebApplication1.Model.ViewModels;

namespace WebApplication1.Repo
{
    public class UniversityRepository
    {
        public UniversityRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private DataContext _dataContext { get; }

        public async Task<University[]?> GetUniversitiesByCountry(string countryName)
        {
            var country = await _dataContext.Countries.FirstOrDefaultAsync(x => x.Name.ToLower() == countryName.ToLower());
            if (country != null)
            {
                return await _dataContext.Universities.Where(x => x.Country == country.Name).ToArrayAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task SaveOrUpdate(string countryName, University[] universitiesObject)
        {
            // Check if the country exists in the database
            var country = await _dataContext.Countries.FirstOrDefaultAsync(x => x.Name.ToLower() == countryName.ToLower());

            // If the country doesn't exist, add it to the database
            if (country == null)
            {
                country = new Country { Name = countryName };
                _dataContext.Countries.Add(country);
                await _dataContext.SaveChangesAsync(); // Save changes to get the generated country ID
            }

            foreach (var universityJson in universitiesObject)
            {
                var universityDb = await _dataContext.Universities.FirstOrDefaultAsync(u => u.Name.ToLower() == universityJson.Name.ToLower());

                // If the university doesn't exist, add it to the database
                if (universityDb == null)
                {
                    universityDb = new University
                    {
                        Name = universityJson.Name,
                        Country = universityJson.Country,
                        Domains = universityJson.Domains,
                        Alpha_Two_Code = universityJson.Alpha_Two_Code,
                        Web_Pages = universityJson.Web_Pages,
                        State_Province = universityJson.State_Province
                    };
                    _dataContext.Universities.Add(universityDb);
                }
                else
                {
                    // If the university already exists, update its properties
                    universityDb.Domains = universityJson.Domains;
                    universityDb.Alpha_Two_Code = universityJson.Alpha_Two_Code;
                    universityDb.Web_Pages = universityJson.Web_Pages;
                    universityDb.State_Province = universityJson.State_Province;
                }
            }
            try
            {
                await _dataContext.SaveChangesAsync(); // Save changes to the database
            }
            catch (Exception ex)
            {

            }
        }
    }
}
