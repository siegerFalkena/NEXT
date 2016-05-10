using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.API.Repositories;

namespace NEXT.API.Models
{
    public class CompanyRepository : ICompanyRepository
    {
        private NEXTContext context;

        public CompanyRepository(NEXTContext context) {
            this.context = context;
        }
        
        void ICompanyRepository.createCompany(Company company)
        {   
            context.Company.Add(company);

        }

        void ICompanyRepository.deleteCompany(int ID)
        {
            Company tempCompany =  context.Company.Where<Company>( Company=> Company.ID == ID).Single();
            context.Company.Remove(tempCompany);
        }

        Company ICompanyRepository.getCompanyByID(int ID)
        {
            return context.Company.Where(Company => Company.ID == ID).Single();
        }

        void ICompanyRepository.updateCompany(Company Company)
        {
            context.Company.Update(Company);
        }

        List<Company> ICompanyRepository.companyQuery(CompanyQuery query, int results, int skipPages)
        {
            IQueryable<Company> queryObject = context.Company.Where(query.asExpression());
            if ( skipPages > 0) {
                queryObject = queryObject.Skip(skipPages * results);
            }
            return queryObject.Take(results).ToList();
        }

        void ICompanyRepository.save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
