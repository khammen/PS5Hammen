using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCTOBER.Shared.DTO
{
    public class EnrollmentDTO
    {
        [Precision(8)]
        public int Student_id { get; set; }

        [Precision(8)]
        public int Section_id { get; set; }

        public DateTime Enroll_date { get; set; }

        [Precision(3)]
        public int Final_grade { get; set; }

        [StringLength(30)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
        [Precision(8)]
        public int School_id { get; set; }
    }
}
