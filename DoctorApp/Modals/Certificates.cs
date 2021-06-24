using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Certificates
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<DoctorCertificates> DoctorCertificates { get; set; }
    }
}