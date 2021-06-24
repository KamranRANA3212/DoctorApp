using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorApp.Modals
{
    public class Days
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}