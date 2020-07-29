using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkExample_WebProject.EF
{
    public interface IContactRepository
    {
        void AddAddressWithPerson(PersonToAddress personToAddress);
        void AddPerson(Person person);
        void AddAddress(Address address);
        Task<List<Person>> GetPeople();

        Task<List<Person>> GetPeopleWithAddress();

        Task<Person> GetPersonById(int id);
    }
}
