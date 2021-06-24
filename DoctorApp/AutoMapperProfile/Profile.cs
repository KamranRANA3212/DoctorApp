using AutoMapper;
using DoctorApp.DTO_s;
using DoctorApp.Modals;
using System.Collections.Generic;

namespace DoctorApp.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<List<Specialties>, List<SpecialityDTO>>();

            CreateMap<DoctorDTO, Doctor>();
            CreateMap<Doctor, DoctorDTO>();
            CreateMap<DoctorUpdateDTO, Doctor>();

            CreateMap<AppointmentDTO, Appointment>();
            CreateMap<Appointment, AppointmentDTO>();

            CreateMap<PatientDTO, Patient>();
            CreateMap<Patient, PatientDTO>();
        }
    }
}