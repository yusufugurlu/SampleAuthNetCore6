using Sample.Business.GenericRepository;
using Sample.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Abstract
{
    public interface IUnitOfWorks
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : class, new();

        ServiceResult SaveChanges();
    }
}
