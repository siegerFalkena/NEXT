using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NEXT.DB.Models;
using AutoMapper;
using NEXT.API.Resource;
using NEXT.API.Query;

namespace NEXT.API.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private NEXTContext context;
        private IMapper mapper;
        public LanguageRepository(NEXTContext context, IMappingConfigProvider mapperConfigProvider)
        {
            this.context = context;
            mapper = mapperConfigProvider.getConfig().CreateMapper();

        }

        void IDisposable.Dispose()
        {
        }

        public void createLanguage(Resource.Language language)
        {
        }

        public ICollection<Resource.Language> query(LanguageQuery query)
        { 
            IQueryable<DB.Models.Language> languages =  context.Language.Where(query.asExpression());
            languages = query.getOrdering(languages).Skip(query.page * query.results).Take(query.results);
            return mapper.Map<ICollection<DB.Models.Language>, ICollection<API.Resource.Language>>(languages.ToList());
        }

        public Resource.Language getByID(int LanguageID) {
            DB.Models.Language lang =  context.Language.Where(l => l.ID == LanguageID).SingleOrDefault();
            return mapper.Map<Resource.Language>(lang);
        }
    }
}
