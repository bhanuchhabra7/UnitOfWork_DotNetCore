using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkExample_WebProject.EF
{
    public sealed class ContactRepository : IContactRepository, IDisposable
    {
        private readonly UowExampleContext context;

        public void Dispose()
        {
            this.context?.Dispose();
        }

        public ContactRepository(UowExampleContext context)
        {
            this.context = context;
        }

        public void AddAddressWithPerson(PersonToAddress personToAddress)
        {
            context.PeoplAddresses.Add(personToAddress);
        }

        public Task<List<Person>> GetPeople()
        {
            return context.People.ToListAsync();
        }

        public Task<Person> GetPersonById(int id)
        {
            return context.People.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void AddPerson(Person person)
        {
            context.People.Add(person);
        }

        public void AddAddress(Address address)
        {
            context.Addresses.Add(address);
        }

        public Task<List<Person>> GetPeopleWithAddress()
        {
            return context.People.Include(x => x.PeoplesAddress).ThenInclude(x => x.Address).ToListAsync();
        }
    }
}
