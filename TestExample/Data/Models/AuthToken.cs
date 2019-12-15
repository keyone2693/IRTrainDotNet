using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestExample.Data.Models
{
    public class AuthToken : BaseEntity<short>
    {
        public AuthToken()
        {
            Id = new short();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public string Value { get; set; }
    }
}
