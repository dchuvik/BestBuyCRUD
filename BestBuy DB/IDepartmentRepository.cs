using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuy_DB
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAllDepartments(); //Stubbed out method
    }
}
