using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCore.Data.ViewModels
{
    public class GetUserViewModel
    {
        public string DisplayName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime JoinedDateTime { get; set; } = DateTime.Now;
    }
}
