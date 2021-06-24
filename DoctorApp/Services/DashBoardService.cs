using AutoMapper;
using DoctorApp.DTO_s;
using DoctorApp.enums;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class DashBoardService : IDashBoard
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public DashBoardService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<DashBoardDTO>> GetDashBoardData()
        {
            

            var doctors = await _context.Doctor.ToListAsync();

            DashBoardDTO dashBoardDTO = new DashBoardDTO()
            {
                Doctors = doctors.Count(),
                Patients = _context.Patient.Count(),
                Bookings = _context.Appointment.Count(),
                NewRequest = doctors.Where(z => z.Status == (int)Status.InProcess).Count(),
                VerifiedRequest = doctors.Where(z => z.Status == (int)Status.Approved).Count(),
                CancelledRequest = doctors.Where(z => z.Status == (int)Status.Cancel).Count(),
            };

            return DoctorApp.Utilities.Response<DashBoardDTO>.GenerateResponse("success", null, dashBoardDTO, null, "");
        }
    }
}