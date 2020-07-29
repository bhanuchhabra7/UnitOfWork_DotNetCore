using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkExample_WebProject.EF
{
    public interface IUnitOfWork
    {
        UowExampleContext Context { get; }

        IContactRepository ContactRepository { get; }

        Task SaveAsync();
    }
}
