using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Permission;
using WebApplication1.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNToastNotifyNoty(new NotyOptions
{
    Layout = "bottomRight",
    ProgressBar = true,
    Timeout = 5000,
    Theme = "metroui"
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=rcu00.db"));
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
    options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied

});
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseNToastNotify();
app.UseHttpsRedirection();
await SeedDatabase();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task SeedDatabase() //can be placed at the very bottom under app.Run()
{
    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    context.Database.EnsureCreated();

    //check for users
    if (context.ApplicationUser.Any())
    {
        return; //if user is not empty, DB has been seed
    }

    //CREATE ROLES
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.PortalAdmin.ToString().Substring(0,2).ToUpper(), AppRoles.PortalAdmin.ToString()));
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAdmin.ToString().Substring(0,2).ToUpper(), AppRoles.ClientAdmin.ToString()));
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorAdmin.ToString().Substring(0,2).ToUpper(), AppRoles.VendorAdmin.ToString()));
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientCreator.ToString().Substring(0,2).ToUpper(), AppRoles.ClientCreator.ToString()));
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAssigner.ToString().Substring(0,2).ToUpper(), AppRoles.ClientAssigner.ToString()));
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAssessor.ToString().Substring(0,2).ToUpper(), AppRoles.ClientAssessor.ToString()));
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorSupervisor.ToString().Substring(0,2).ToUpper(), AppRoles.VendorSupervisor.ToString()));
    await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorAgent.ToString().Substring(0,2).ToUpper(), AppRoles.VendorAgent.ToString()));

    //CREATE RISK CASE DETAILS
    var created = new RiskCaseStatus
    {
        Name = "CREATED",
        Code = "CREATED"
    };
    var currentCaseStatus1 = await context.RiskCaseStatus.AddAsync(created);
    var assigned = new RiskCaseStatus
    {
        Name = "ASSIGNED",
        Code = "ASSIGNED"
    };

    var currentCaseStatus2 = await context.RiskCaseStatus.AddAsync(assigned);
    var rejected = new RiskCaseStatus
    {
        Name = "REJECTED",
        Code = "REJECTED"
    };
    
    var currentCaseStatus3 = await context.RiskCaseStatus.AddAsync(rejected);
    var accepted = new RiskCaseStatus
    {
        Name = "ACCEPTED",
        Code = "ACCEPTED"
    };
    
    var currentCaseStatus4 = await context.RiskCaseStatus.AddAsync(accepted);
    var withdrawn = new RiskCaseStatus
    {
        Name = "WITHDRAWN",
        Code = "WITHDRAWN"
    };
    
    var currentCaseStatus5 = await context.RiskCaseStatus.AddAsync(withdrawn);

    var clientCreatorCreated = new RiskCaseStatus
    {
        Name = "CLIENT_CREATOR_CREATED",
        Code = "CLIENT_CREATOR_CREATED"
    };
    
    var currentCaseStatus6 = await context.RiskCaseStatus.AddAsync(clientCreatorCreated);
    
    var clientAssignerAccepted = new RiskCaseStatus
    {
        Name = "CLIENT_ASSIGNER_ACCEPTED",
        Code = "CLIENT_ASSIGNER_ACCEPTED"
    };
    
    var currentCaseStatus7 = await context.RiskCaseStatus.AddAsync(clientAssignerAccepted);
        
    var vendorSupervisorAccepted = new RiskCaseStatus
    {
        Name = "VENDOR_SUPERVISOR_ACCEPTED",
        Code = "VENDOR_SUPERVISOR_ACCEPTED"
    };
    
    var currentCaseStatus8 = await context.RiskCaseStatus.AddAsync(vendorSupervisorAccepted);
    var vendorAgentAccepted = new RiskCaseStatus
    {
        Name = "VENDOR_AGENT_ACCEPTED",
        Code = "VENDOR_AGENT_ACCEPTED"
    };
    
    var currentCaseStatus9 = await context.RiskCaseStatus.AddAsync(vendorAgentAccepted);        
   var vendorAgentSubmitted = new RiskCaseStatus
    {
        Name = "VENDOR_AGENT_SUBMITTED",
        Code = "VENDOR_AGENT_SUBMITTED"
    };
    
    var currentCaseStatus10 = await context.RiskCaseStatus.AddAsync(vendorAgentSubmitted); 
  
   var vendorSupervisorSubmitted = new RiskCaseStatus
    {
        Name = "VENDOR_SUPERVISOR_SUBMITTED",
        Code = "VENDOR_SUPERVISOR_SUBMITTED"
    };
    
    var currentCaseStatus11 = await context.RiskCaseStatus.AddAsync(vendorSupervisorSubmitted); 

   var clientAsssessorAccepted = new RiskCaseStatus
    {
        Name = "CLIENT_ASSESSOR_ACCEPTED",
        Code = "CLIENT_ASSESSOR_ACCEPTED"
    };
    
    var currentCaseStatus12 = await context.RiskCaseStatus.AddAsync(clientAsssessorAccepted); 

   var clientAsssessorSubmitted = new RiskCaseStatus
    {
        Name = "CLIENT_ASSESSOR_SUBMITTED",
        Code = "CLIENT_ASSESSOR_SUBMITTED"
    };
    
    var currentCaseStatus13 = await context.RiskCaseStatus.AddAsync(clientAsssessorSubmitted);         
    var clientAsssessorRejected = new RiskCaseStatus
    {
        Name = "CLIENT_ASSESSOR_REJECTED",
        Code = "CLIENT_ASSESSOR_REJECTED"
    };
    
    var currentCaseStatus14 = await context.RiskCaseStatus.AddAsync(clientAsssessorSubmitted);
    var clientAsssessorReturnRejected = new RiskCaseStatus
    {
        Name = "CLIENT_ASSESSOR_RETURN_REJECTED",
        Code = "CLIENT_ASSESSOR_RETURN_REJECTED"
    };
    
    var currentCaseStatus15 = await context.RiskCaseStatus.AddAsync(clientAsssessorReturnRejected); 

    var claims = new RiskCaseType
    {
        Name = "CLAIMS",
        Code = "CLAIMS",
    };
    
    var currentCaseType1 = await context.RiskCaseType.AddAsync(claims);
    var underwriting = new RiskCaseType
    {
        Name = "UNDERWRITING",
        Code = "UNDERWRITING",
    };
    
    var currentCaseType = await context.RiskCaseType.AddAsync(underwriting);
    var india = new Country 
    {
        Name = "INDIA",
        Code = "IND",
    };
    var currentCountry = await context.Country.AddAsync(india);
    var australia = new Country 
    {
        Name = "AUSTRALIA",
        Code = "AUS",
    };
    
    var australiaCountry = await context.Country.AddAsync(australia);
    var canada = new Country 
    {
        Name = "CANADA",
        Code = "CAN",
    };
    
    var canadaCountry = await context.Country.AddAsync(canada);    
    var up = new State
    {
        CountryId= currentCountry.Entity.CountryId,
        Name = "UTTAR PRADESH",
        Code = "UP"
    };

    var upState = await context.State.AddAsync(up);
    var ontario = new State
    {
        CountryId= canadaCountry.Entity.CountryId,
        Name = "ONTARIO",
        Code = "ON"
    };

    var ontarioState = await context.State.AddAsync(ontario);   

    var delhi = new State
    {
        CountryId = currentCountry.Entity.CountryId,
        Name = "NEW DELHI",
        Code = "NDL"
    };

    var delhiState = await context.State.AddAsync(delhi);

    var victoria = new State
    {
        CountryId= australiaCountry.Entity.CountryId,
        Name = "VICTORIA",
        Code = "VIC"
    };

    var victoriaState = await context.State.AddAsync(victoria);   

    var tasmania = new State
    {
        CountryId= australiaCountry.Entity.CountryId,
        Name = "TASMANIA",
        Code = "TAS"
    };

    var tasmaniaState = await context.State.AddAsync(tasmania); 

    var newDelhi = new PinCode{
        Name = "NEW DELHI",
        Code = "110001",
        State = delhiState.Entity
    };

    var newDelhiPinCode = await context.PinCode.AddAsync(newDelhi);

    var northDelhi = new PinCode{
        Name = "NORTH DELHI",
        Code = "110002",
        State = delhiState.Entity
    };

    var northDelhiPinCode = await context.PinCode.AddAsync(northDelhi);    

    var indirapuram = new PinCode{
        Name = "INDIRAPURAM",
        Code = "201014",
        State = upState.Entity
    };

    var indiraPuramPinCode = await context.PinCode.AddAsync(indirapuram);

    var bhelupur = new PinCode{
        Name = "BHELUPUR",
        Code = "221001",
        State = upState.Entity
    };

    var bhelupurPinCode = await context.PinCode.AddAsync(bhelupur);

    var forestHill = new PinCode{
        Name = "FOREST HILL",
        Code = "3131",
        State = victoriaState.Entity
    };

    var forestHillPinCode = await context.PinCode.AddAsync(forestHill);   

    var vermont = new PinCode{
        Name = "VERMONT",
        Code = "3133",
        State = victoriaState.Entity
    };
    
    var vermontPinCode = await context.PinCode.AddAsync(vermont);        

    var tasmaniaCity = new PinCode{
        Name = "TASMANIA CITY",
        Code = "7000",
        State = tasmaniaState.Entity
    };
    
    var tasmaniaCityCode = await context.PinCode.AddAsync(tasmaniaCity); 
    
    var torontoCity = new PinCode{
        Name = "TORONTO",
        Code = "9101",
        State = ontarioState.Entity
    };
    
    var torontoCityCode = await context.PinCode.AddAsync(tasmaniaCity);     

    var _case1 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 1",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus1.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };
    var _case2 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 2",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus2.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };
    
    var _case3 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 3",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus3.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };
    var _case4 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 4",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus4.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };    
    var _case5 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 5",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus5.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };   
    var _case6 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 6",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus6.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };     
    var _case7 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 7",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus7.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };        
    var _case8 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 8",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus2.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };   
    var _case9 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 9",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus3.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };         
    var _case10 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 10",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus4.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };    
    var _case11 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 11",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus6.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };      
    var _case12 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 12",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus5.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
    var _case13 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 13",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus3.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };   
   var _case14 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 14",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus1.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };        
    
   var _case15 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 15",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus3.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
   var _case16 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 16",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus7.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
   var _case17 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 17",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus4.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
   var _case18 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 18",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus3.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
   var _case19 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 19",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus4.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
       var _case20 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 20",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus2.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
       var _case21 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 21",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus7.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };     
    var _case22 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 22",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus2.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    }; 
    var _case23 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 23",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus4.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  

    var _case24 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 24",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus5.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  

    var _case25 = new RiskCase
    {
        Name =  "TEST CLAIM CASE 25",
        Description = "TEST CLAIM CASE DESCRIPTION",
        RiskCaseTypeId  =  currentCaseType.Entity.RiskCaseTypeId,
        RiskCaseStatusId = currentCaseStatus2.Entity.RiskCaseStatusId,
        Created = DateTime.Now
    };  
                                                                        
    await context.RiskCase.AddAsync(_case1);
    await context.RiskCase.AddAsync(_case2);
    await context.RiskCase.AddAsync(_case3);
    await context.RiskCase.AddAsync(_case4);
    await context.RiskCase.AddAsync(_case5);
    await context.RiskCase.AddAsync(_case6);
    await context.RiskCase.AddAsync(_case7);
    await context.RiskCase.AddAsync(_case8);
    await context.RiskCase.AddAsync(_case9);
    await context.RiskCase.AddAsync(_case10);
    await context.RiskCase.AddAsync(_case11);
    await context.RiskCase.AddAsync(_case12);
    await context.RiskCase.AddAsync(_case13);
    await context.RiskCase.AddAsync(_case14);
    await context.RiskCase.AddAsync(_case15);
    await context.RiskCase.AddAsync(_case16);
    await context.RiskCase.AddAsync(_case17);
    await context.RiskCase.AddAsync(_case18);
    await context.RiskCase.AddAsync(_case19);
    await context.RiskCase.AddAsync(_case20);
    await context.RiskCase.AddAsync(_case21);
    await context.RiskCase.AddAsync(_case22);
    await context.RiskCase.AddAsync(_case23);
    await context.RiskCase.AddAsync(_case24);
    await context.RiskCase.AddAsync(_case25);

    await context.SaveChangesAsync();

    //Seed portal admin
    var portalAdmin = new ApplicationUser()
    {
        UserName = "portal-admin@admin.com",
        Email = "portal-admin@admin.com",
        FirstName = "Portal",
        LastName = "Admin",
        Password = ApplicationUserOptions.Password,
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        State = upState.Entity,
        Country = currentCountry.Entity,
        PinCode = indiraPuramPinCode.Entity,
        ProfilePictureUrl = "img/superadmin.jpg"
    };
    if (userManager.Users.All(u => u.Id != portalAdmin.Id))
    {
        var user = await userManager.FindByEmailAsync(portalAdmin.Email);
        if (user == null)
        {
            await userManager.CreateAsync(portalAdmin, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.PortalAdmin.ToString());
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientAdmin.ToString());
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientCreator.ToString());
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientAssigner.ToString());
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.ClientAssessor.ToString());
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.VendorAdmin.ToString());
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.VendorSupervisor.ToString());
            await userManager.AddToRoleAsync(portalAdmin, AppRoles.VendorAgent.ToString());
        }
        var adminRole = await roleManager.FindByNameAsync(AppRoles.PortalAdmin.ToString());
        var allClaims = await roleManager.GetClaimsAsync(adminRole);
        var allPermissions = Permissions.GeneratePermissionsForModule("Products");
        foreach (var permission in allPermissions)
        {
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(adminRole, new Claim("Permission", permission));
            }
        }
    }


    //Seed client admin
    var clientAdmin = new ApplicationUser()
    {
        UserName = "client-admin@admin.com",
        Email = "client-admin@admin.com",
        FirstName = "Client",
        LastName = "Admin",
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        Password = ApplicationUserOptions.Password,
        isSuperAdmin = true,
        State = ontarioState.Entity,
        Country = canadaCountry.Entity,
        PinCode = torontoCityCode.Entity,
        ProfilePictureUrl = "img/admin.png"        
    };
    if (userManager.Users.All(u => u.Id != clientAdmin.Id))
    {
        var user = await userManager.FindByEmailAsync(clientAdmin.Email);
        if (user == null)
        {
            await userManager.CreateAsync(clientAdmin, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientAdmin.ToString());
            await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientCreator.ToString());
            await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientAssigner.ToString());
            await userManager.AddToRoleAsync(clientAdmin, AppRoles.ClientAssessor.ToString());
            await userManager.AddToRoleAsync(clientAdmin, AppRoles.VendorAdmin.ToString());
            await userManager.AddToRoleAsync(clientAdmin, AppRoles.VendorSupervisor.ToString());
            await userManager.AddToRoleAsync(clientAdmin, AppRoles.VendorAgent.ToString());
        }
    }

    //Seed client creator
    var clientCreator = new ApplicationUser()
    {
        UserName = "client-creator@admin.com",
        Email = "client-creator@admin.com",
        FirstName = "Client",
        LastName = "Creator",
        EmailConfirmed = true,
        Password = ApplicationUserOptions.Password,
        PhoneNumberConfirmed = true,
        isSuperAdmin = true,
        State = upState.Entity,
        Country = currentCountry.Entity,
        PinCode = indiraPuramPinCode.Entity,
        ProfilePictureUrl = "img/creator.jpg"        
    };
    if (userManager.Users.All(u => u.Id != clientCreator.Id))
    {
        var user = await userManager.FindByEmailAsync(clientCreator.Email);
        if (user == null)
        {
            await userManager.CreateAsync(clientCreator, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(clientCreator, AppRoles.ClientCreator.ToString());
        }
    }


    //Seed client assigner
    var clientAssigner = new ApplicationUser()
    {
        UserName = "client-assigner@admin.com",
        Email = "client-assigner@admin.com",
        FirstName = "Client",
        LastName = "Assigner",
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        Password = ApplicationUserOptions.Password,
        isSuperAdmin = true,
        PinCode = northDelhiPinCode.Entity,
        State = delhiState.Entity,
        Country = currentCountry.Entity,
        ProfilePictureUrl = "img/assigner.png"        
    };
    if (userManager.Users.All(u => u.Id != clientAssigner.Id))
    {
        var user = await userManager.FindByEmailAsync(clientAssigner.Email);
        if (user == null)
        {
            await userManager.CreateAsync(clientAssigner, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(clientAssigner, AppRoles.ClientAssigner.ToString());
        }
    }

    //Seed client assessor
    var clientAssessor = new ApplicationUser()
    {
        UserName = "client-assessor@admin.com",
        Email = "client-assessor@admin.com",
        FirstName = "Client",
        LastName = "Assessor",
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        Password = ApplicationUserOptions.Password,
        isSuperAdmin = true,
        PinCode = northDelhiPinCode.Entity,
        State = delhiState.Entity,
        Country = currentCountry.Entity,
        ProfilePictureUrl = "img/assessor.png"        
    };
    if (userManager.Users.All(u => u.Id != clientAssessor.Id))
    {
        var user = await userManager.FindByEmailAsync(clientAssessor.Email);
        if (user == null)
        {
            await userManager.CreateAsync(clientAssessor, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(clientAssessor, AppRoles.ClientAssessor.ToString());
        }
    }


    //Seed Vendor Admin
    var vendorAdmin = new ApplicationUser()
    {
        UserName = "vendor-admin@admin.com",
        Email = "vendor-admin@admin.com",
        FirstName = "Vendor",
        LastName = "Admin",
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        Password = ApplicationUserOptions.Password,
        isSuperAdmin = true,
        PinCode = indiraPuramPinCode.Entity,
        State = upState.Entity,
        Country = currentCountry.Entity,
        ProfilePictureUrl = "img/vendor-admin.png"        
    };
    if (userManager.Users.All(u => u.Id != vendorAdmin.Id))
    {
        var user = await userManager.FindByEmailAsync(vendorAdmin.Email);
        if (user == null)
        {
            await userManager.CreateAsync(vendorAdmin, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(vendorAdmin, AppRoles.VendorAdmin.ToString());
            await userManager.AddToRoleAsync(vendorAdmin, AppRoles.VendorSupervisor.ToString());
            await userManager.AddToRoleAsync(vendorAdmin, AppRoles.VendorAgent.ToString());
        }
    }



    //Seed Vendor Admin
    var vendorSupervisor = new ApplicationUser()
    {
        UserName = "vendor-supervisor@admin.com",
        Email = "vendor-supervisor@admin.com",
        FirstName = "Vendor",
        LastName = "Supervisor",
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        Password = ApplicationUserOptions.Password,
        isSuperAdmin = true,
        PinCode = indiraPuramPinCode.Entity,
        State = upState.Entity,
        Country = currentCountry.Entity,
        ProfilePictureUrl = "img/supervisor.png"        
    };
    if (userManager.Users.All(u => u.Id != vendorSupervisor.Id))
    {
        var user = await userManager.FindByEmailAsync(vendorSupervisor.Email);
        if (user == null)
        {
            await userManager.CreateAsync(vendorSupervisor, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(vendorSupervisor, AppRoles.VendorSupervisor.ToString());
            await userManager.AddToRoleAsync(vendorSupervisor, AppRoles.VendorAgent.ToString());
        }
    }


    //Seed Vendor Admin
    var vendorAgent = new ApplicationUser()
    {
        UserName = "vendor-agent@admin.com",
        Email = "vendor-agent@admin.com",
        FirstName = "Vendor",
        LastName = "Agent",
        EmailConfirmed = true,
        PhoneNumberConfirmed = true,
        Password = ApplicationUserOptions.Password,
        isSuperAdmin = true,
        PinCode = indiraPuramPinCode.Entity,
        State = upState.Entity,
        Country = currentCountry.Entity,
        ProfilePictureUrl = "img/agent.jpg"        
    };
    if (userManager.Users.All(u => u.Id != vendorAgent.Id))
    {
        var user = await userManager.FindByEmailAsync(vendorAgent.Email);
        if (user == null)
        {
            await userManager.CreateAsync(vendorAgent, ApplicationUserOptions.Password);
            await userManager.AddToRoleAsync(vendorAgent, AppRoles.VendorAgent.ToString());
        }
    }
}
