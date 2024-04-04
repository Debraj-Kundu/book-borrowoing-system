using DataLayer.Entity;
using SharedLayer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuisnessLayer.Domain
{
    public class UserDomain : DomainBase
    {
        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Tokens_Available { get; set; }

        public virtual ICollection<BookDomain>? Books_Borrowed { get; set; }

        public virtual ICollection<BookDomain>? Books_Lent { get; set; }
    }
}
