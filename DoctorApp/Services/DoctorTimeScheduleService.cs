using AutoMapper;
using DoctorApp.DTO_s;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class DoctorTimeScheduleService : IDoctorTimeSchedule
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public DoctorTimeScheduleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetTimeSchedule(string doctorId)
        {
            var data = await _context.DoctorTimeSchedule.Where(z => z.ApplicationUser.User_Id == doctorId).Select(z => new DoctorTimeScheduleDTO()
            {
                Id = z.Id,
                Doctor_Id = z.Doctor_Id,
                DayName = z.Day.Name,
                ShiftType = z.ShiftType == 1 ? "Morning" : "Evening",
                StartTime = z.StartTime,
                EndTime = z.EndTime,
            }).AsNoTracking().ToListAsync();

            return new { status = "success", data = data };
        }

        public async Task<object> NewTimeSchedule(DoctorTimeSchedule model)
        {
            await _context.DoctorTimeSchedule.AddRangeAsync(model);

            await _context.SaveChangesAsync();

            return new { status = "succeess", message = "New Schedule has been added!" };
        }
    }
}