using AutoMapper;
using DoctorApp.DTO_s;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class SpecialityService : ISpeciality
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public SpecialityService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> AddNewSpeciality(SpecialityDTO specialityDTO)
        {
            Specialties specialties = new Specialties();

            _mapper.Map(specialityDTO, specialties);

            _context.Specialties.Add(specialties);

            await _context.SaveChangesAsync();

            return new { status = "success", message = "Speciality has been added!" };
        }

        //public async Task<object> Delete(int id)
        //{
        //    var speciality = await _context.Specialties.FindAsync(id);

        //    if (speciality != null)
        //    {
        //        _context.Specialties.Remove(speciality);

        //        await _context.SaveChangesAsync();

        //        return new { status = "success", message = "Speciality has been deleted!" };
        //    }
        //    return new { status = "success", message = "No Record Found!" };
        //}

        public async Task<object> GetSpecialities()
        {
            var specialties = await _context.Specialties.AsNoTracking().ToListAsync();

            return Utilities.Response<Specialties>.GenerateResponse("success", specialties, null, new List<string>(), "");
        }

        public async Task<object> SearchSpeciality(string value)
        {
            List<CategoryDTO> specialityDTOs = new List<CategoryDTO>();

            var speciality = await _context.Specialties.Where(z => z.Name.Contains(value)).AsNoTracking().ToListAsync();

            _mapper.Map(speciality, specialityDTOs); //map response to categorydto

            return Utilities.Response<CategoryDTO>.GenerateResponse("success", specialityDTOs, null, new List<string>(), "");
        }
    }
}