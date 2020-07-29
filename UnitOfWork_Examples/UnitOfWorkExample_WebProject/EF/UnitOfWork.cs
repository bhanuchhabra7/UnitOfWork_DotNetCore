using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkExample_WebProject.EF
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UowExampleContext Context { get; private set; }

        public IContactRepository ContactRepository { get; private set; }

        public UnitOfWork(UowExampleContext context, IContactRepository contactRepository)
        {
            this.Context = context;
            this.ContactRepository = contactRepository;
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }

        public Task SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}
