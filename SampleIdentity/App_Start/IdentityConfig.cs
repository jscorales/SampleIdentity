using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2
{
    public class AtomUserManager: UserManager<AtomUser, int>
    {
        public AtomUserManager(IUserStore<AtomUser, int> store)
            : base(store)
        {
        }

        public static AtomUserManager Create(IdentityFactoryOptions<AtomUserManager> options, IOwinContext context)
        {
            // Configure the userstore to use the DbContext to work with database
            //return new AtomUserManager(new AtomUserStore(context.Get<AtomDbContext>()));

            return new AtomUserManager(new AtomUserStore(context.Get<AtomDbContext>()));
        }
    }
}