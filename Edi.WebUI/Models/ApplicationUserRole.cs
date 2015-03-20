using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Edi.WebUI.Models
{
    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationRole Role { get; set; }

        public ApplicationUserRole()
            : base()
        {
            
        }
    }
}
