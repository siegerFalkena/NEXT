using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NEXT.API.Query;
using NEXT.API.Resource;

namespace NEXT.API.Repositories
{
    public interface ILanguageRepository : IDisposable
    {
        ICollection<Resource.Language> query(LanguageQuery query);
    }
}
