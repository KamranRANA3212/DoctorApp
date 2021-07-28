using DoctorApp.DTO_s;
using DoctorApp.Interfaces;
using DoctorApp.Modals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DoctorApp.Services
{
    public class AccountService : IAccount
    {
        private ApplicationDbContext _context;

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _configuration;
        public static IWebHostEnvironment _envirnment;
        

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration,IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _envirnment = environment;
            

    }


        public async Task<LoginResponse> Login(SignIn model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (result)
            {
                var claims = new[]
            {
                   new Claim("userStatus",user.IsActive.ToString()),
                   new Claim("role", _userManager.GetRolesAsync(user).Result[0].ToString()),
                   //new Claim("licenceStatus", _context.Doctor.FirstOrDefault(z=>z.User_Id==user.Id)?.IsLicenceVerified.ToString()),
                   new Claim("userId", user.Id),
               };

                var token = GenerateToken(claims);
                string image = "";

                if (_userManager.GetRolesAsync(user).Result[0].ToString() == "Doctor")
                {
                    image = _context.Doctor.FirstOrDefaultAsync(z => z.User_Id == user.Id).Result?.Image;
                }
                else
                {
                    image = _context.Patient.FirstOrDefaultAsync(z => z.User_Id == user.Id).Result?.Image;
                }

                return new LoginResponse()
                {
                    Status = "success",
                    Token = token,
                    Error = "",
                    User = new User() { IsActive = user.IsActive, Role = _userManager.GetRolesAsync(user).Result[0].ToString(), UserId = user.Id, Name = user.Name, Image = image },
                };
            }

            string error = "";

            if (!(user?.Email == model.Email))
            {
                //Email is valid
                error = "Email is not valid";
            }
            else
            {
                error = "Pssword is not valid";
            }

            return new LoginResponse()
            {
                Status = "fail",
                Error = error,
            };
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public ObjectResult Refresh(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name;
            //var savedRefreshToken = GetRefreshToken(username); //retrieve the refresh token from a data store
            //if (savedRefreshToken != refreshToken)
            //    throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = GenerateToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();
            //DeleteRefreshToken(username, refreshToken);
            //SaveRefreshToken(username, newRefreshToken);

            return new ObjectResult(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }

        public async Task<object> Register(SignUp model)
        {
             
            
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.Phone,
                PhoneNumberConfirmed = true,
                Name = model.FirstName + " " + model.LastName,
            };


            var userStatus = await _userManager.CreateAsync(user, model.Password);


            if (userStatus.Succeeded)
            {
                var claimStatus = await _userManager.AddToRoleAsync(user, model.Role);

                if (claimStatus.Succeeded)
                {
                    if (model.Role == "Doctor" || model.Role=="doctor")
                    {
                        // make account for doctor
                        var userDetial = await _userManager.FindByEmailAsync(model.Email);
                       
                        
                        string uniqueFileName = ImageUpload(model);
                        Doctor doctor = new Doctor()
                        {
                            FirstName=model.FirstName,
                            LastName=model.LastName,
                            LicenceNumber = model.LicenceNumber,
                            IsLicenceVerified = false,
                            RegistrationCode = new Random().Next(999, 9999),
                            User_Id = userDetial.Id,
                            Status = 2,
                            AssistantName = model.AssistantName,
                            AssistantNumber = model.AssistantNumber,
                            CreatedDate = DateTime.UtcNow.AddHours(5),
                            //Image=objfile.file.FileName
                            Image = uniqueFileName
                           


                        };                            
                                     
                        doctor.DoctorSpeciality = new List<DoctorSpeciality>();

                        foreach (var id in model.SpecialityIds)
                        {
                            doctor.DoctorSpeciality.Add(new DoctorSpeciality()
                            {
                                SpecialtiesId = id,
                            });
                        }

                        _context.Doctor.Add(doctor);

                        var result = await _context.SaveChangesAsync() >= 1;

                        if (result)
                        {
                            await AddQualificationAndExperience(model, doctor.Id);

                            return new
                            {
                                Status = "success",
                                Message = "Congratulations! Your account has been created",
                                Errors = new List<string>()
                            };
                        }
                    }
                  else if (model.Role=="Patient")
                    {
                        var detail =  _userManager.FindByEmailAsync(model.Email);
                        string userimage = ImageUpload(model);
                        Patient patient = new Patient
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Description = model.Description,
                            Image = userimage,
                            CreatedDate = DateTime.UtcNow.AddHours(5),
                            UpdatedDate=default,
                            Lat=model.lat,
                            Long=model.lang,
                            Location=model.Location                                                    
                           
                        };
                        _context.Patient.Add(patient);
                        var res = _context.SaveChangesAsync();
                        
                            return new
                            {
                                Status = "success",
                                Message = "Congratulations! Your account has been created",
                                Errors = new List<string>()
                            };
                            

                    }
                }
            }
            else
            {
                List<string> errors = new List<string>();

                foreach (var error in userStatus.Errors)
                {
                    errors.Add(error.Description);
                }
                return new
                {
                    Status = "fail",
                    Message = "Your request has been denied for following reasons!",
                    Errors = errors
                };
            }
            return false;
        }

        private string ImageUpload([FromForm] SignUp img)
        {
            string uniqueFileName = null;

            if (img.image != null)
            {
                string uploadsFolder = Path.Combine(_envirnment.WebRootPath, "images");
                uniqueFileName =  Guid.NewGuid().ToString() + "_" + img.image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    img.image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private async Task<bool> AddQualificationAndExperience(SignUp model, int id)
        {
            List<DoctorQualification> doctorQualifications = new List<DoctorQualification>();
            List<DoctorExperience> doctorExperience = new List<DoctorExperience>();

            //add Qulification
            foreach (var qualification in model.Qualification)
            {
                DoctorQualification doctorQualification = new DoctorQualification()
                {
                    DoctorId = id,
                    Degree = qualification.Degree,
                    Institute = qualification.Institute
                };

                doctorQualifications.Add(doctorQualification);
            }

            //add Experienc

            foreach (var experience in model.Experience)
            {
                DoctorExperience doctorexperience = new DoctorExperience()
                {
                    DoctorId = id,
                    Designation = experience.Designation,
                    Hospital = experience.Hospital
                };

                doctorExperience.Add(doctorexperience);
            }
            
            _context.DoctorQualification.AddRange(doctorQualifications);
            _context.DoctorExperience.AddRange(doctorExperience);

            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<object> GetAdmins()
        {
            var users = await _userManager.GetUsersInRoleAsync("Admin");

            return DoctorApp.Utilities.Response<ApplicationUser>.GenerateResponse("success", users, null, new List<string>(), "");
        }

        public async Task<object> BlockUnlockAdmin(string email)
            {
            var user = await _userManager.FindByEmailAsync(email);

            user.IsActive = !user.IsActive;

            await _context.SaveChangesAsync();

            return new { Status = "success", Message = "Status has been changed", User = user };
        }
      
        public async Task<object> DeleteAdmin(string id)
        {
            _context.Users.Remove(await _context.Users.FirstOrDefaultAsync(z => z.Id == id));

            await _context.SaveChangesAsync();

            return new { status = "success", message = "Account has been deleted" };
        }

        public async Task<object> ChangePassword(ChnagePassword chnagePassword)
        {
            List<string> errors = new List<string>();

            var user = await _userManager.FindByEmailAsync(chnagePassword.Email);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, chnagePassword.OldPassword, chnagePassword.NewPassword);

                if (result.Succeeded)
                {
                    return new { status = "success", message = "Password has been changed", errors = errors };
                }

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return new { status = "fail", message = "Request has been denied for following reasons", errors = errors };
            }
            return new { status = "fail", message = "Email is not valid", errors = errors };
        }

        public async Task<ShortResponse> CreateCard(DTO_s.Card card)
        {
            //Store Card info in db

            string accountToken;
            string customerToken;

            StripeService.CreateToken(card, out accountToken, out customerToken);

            if (!(string.IsNullOrEmpty(accountToken) && string.IsNullOrEmpty(customerToken)))
            {
                if (!await _context.BankAccount.AnyAsync(z => z.CardNumber == card.CardNumber))
                {
                    Modals.BankAccount account = new Modals.BankAccount()
                    {
                        CardNumber = card.CardNumber,
                        AccountNumber = card.AccountNumber,
                        AccountTitle = card.Name,
                        CVV = card.CVV,
                        ExpiryMonth = card.ExpMonth,
                        ExpiryYear = card.ExpYear,
                        CreatedDate = DateTime.UtcNow.AddHours(5),
                        User_Id = card.User_Id,
                        Stripe_Token = accountToken, //Card Token
                        Stripe_Customer_Token = customerToken //Customer Token
                    };

                    _context.BankAccount.Add(account);
                    var result = await _context.SaveChangesAsync() >= 1;

                    if (result)
                    {
                        return new ShortResponse()
                        {
                            Status = "success",
                            Message = "Card has been addedd successfully"
                        };
                    }
                }
            }

            return new ShortResponse()
            {
                Status = "fail",
                Error = "Server Error!Please try again"
            };
        }

        public async Task<ShortResponse> ForgotPassword(ForgotPassword forgotPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(z => z.PhoneNumber == forgotPassword.PhoneNumber) as ApplicationUser;

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, forgotPassword.NewPassword);

                if (result.Succeeded)
                {
                    return new ShortResponse()
                    {
                        Status = "success",
                        Error = "Password has been changed"
                    };
                }
                else
                {
                    StringBuilder errors = new StringBuilder();

                    foreach (var error in result.Errors)
                    {
                        errors.AppendLine(error.Description);

                        return new ShortResponse()
                        {
                            Status = "fail",
                            Error = errors.ToString(),
                        };
                    }
                }
            }

            return new ShortResponse()
            {
                Status = "fail",
                Message = " The Phone number  "+forgotPassword.PhoneNumber+" doesn't not exist"
            };
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
        
      /*  public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = Options.SMSAccountIdentification;
            // Your Auth Token from twilio.com/console
            var authToken = Options.SMSAccountPassword;

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
              to: new PhoneNumber(number),
              from: new PhoneNumber(Options.SMSAccountFrom),
              body: message);
        }*/



    }
}