using DataAccess.DBInitializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.DataAccess.Data;
using TBGC.Utility;


public class DbInitializer : IDBInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDBContext _db;

    public DbInitializer(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDBContext db)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
    }

    public void Initalize()
    {
        throw new NotImplementedException();
    }

    public void Initialize()
    {


        //migrations if they are not applied
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception ex) { }



        //create roles if they are not created
        if (!_roleManager.RoleExistsAsync(SD.Role_Member).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Member)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_NonMember)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_AdminAssist)).GetAwaiter().GetResult();


            //if roles are not created, then we will create admin user as well
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "willffdunn",
                Email = "willffdunn@gmail.com",
                Name = "Bill Dunn",
                PhoneNumber = "4848857000",
                StreetAddress = "921 Gardenia Dr 571",
                State = "FL",
                PostalCode = "33483",
                City = "Delray Beach"
            }, "Admin123*").GetAwaiter().GetResult();


            ApplicationUser user = _db.ApplicationUsers.FirstOrDefault
                (u => u.Email == "willffdunn@gmail.com");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).
                GetAwaiter().GetResult();

        }

        return;
    }
}
