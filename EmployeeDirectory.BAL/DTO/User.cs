using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.BAL.DTO
{
    public class User
    {
        public string email { get; set; } = null!;

        public string password { get; set; } = null!;
    }
}
