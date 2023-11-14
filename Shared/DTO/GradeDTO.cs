using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCTOBER.Shared.DTO
{
    public class GradeDTO
    {
        [Precision(8)]
        public int School_id { get; set; }

        [Precision(8)]
        public int Student_id { get; set; }

        [Precision(8)]
        public int Section_id { get; set; }

        [StringLength(2)]
        public string Grade_type_code { get; set; } = null!;

        [Precision(3)]
        public int Grade_code_occurrence { get; set; }

        public decimal Numeric_grade { get; set; }

        public string Comments { get; set; } = null!;



        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

    }
}
