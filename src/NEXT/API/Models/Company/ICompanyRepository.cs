using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Models;

namespace NEXT.API.Repositories
{
    public interface ICompanyRepository : IDisposable
    {
        void createCompany(Company Company);
        void deleteCompany(int ID);
        Company getCompanyByID(int ID);
        void updateCompany(Company Company);
        List<Company> companyQuery(CompanyQuery query, int results, int skipPages);
        void save();
    }
}
