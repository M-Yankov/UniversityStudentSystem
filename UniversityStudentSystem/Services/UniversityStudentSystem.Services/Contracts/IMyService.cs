using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityStudentSystem.Data.Models;

namespace UniversityStudentSystem.Services.Contracts
{
    public interface IMyService
    {
        User Random();
        Course Co();
    }
}
