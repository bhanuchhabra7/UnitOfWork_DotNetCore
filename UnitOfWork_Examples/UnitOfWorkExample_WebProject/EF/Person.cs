using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UnitOfWorkExample_WebProject.EF
{
    public class Person
    {
        public Person()
        {
            PeoplesAddress = new HashSet<PersonToAddress>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<PersonToAddress> PeoplesAddress { get; set; }
    }
}
