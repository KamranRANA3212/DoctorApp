using AutoMapper;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DoctorApp.Services
{
    public class UnitOfWorkService : IUnitOfWork
    {
        private ApplicationDbContext _context;

        private IMapper _mapper;
        private IConfiguration _config;
        private IWebHostEnvironment _env;

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _user;

        public UnitOfWorkService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config, IUserClaimsPrincipalFactory<ApplicationUser> user, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _user = user;
            _env = env;
        }

        public IAccount Account => new AccountService(_context, _userManager, _signInManager, _config);
        public IDoctor Doctor => new DoctorService(_context, _mapper, _config);

        public IDashBoard DashBoard => new DashBoardService(_context, _mapper);

        public ISpeciality Speciality => new SpecialityService(_context, _mapper);

        public IReviews Reviews => new ReviewService(_context, _mapper);

        public IAppointment Appointment => new AppointmnetService(_context, _mapper, _config, _env);

        public IDoctorTimeSchedule DoctorTimeSchedule => new DoctorTimeScheduleService(_context, _mapper);

        public IPatient Patient => new PatientService(_context, _mapper);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}