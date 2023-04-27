using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Seeds
{
    public static class DatabaseSeed
    {
        public static async Task SeedDatabase(WebApplication app) //can be placed at the very bottom under app.Run()
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
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.PortalAdmin.ToString().Substring(0, 2).ToUpper(), AppRoles.PortalAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAdmin.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorAdmin.ToString().Substring(0, 2).ToUpper(), AppRoles.VendorAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientCreator.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientCreator.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAssigner.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientAssigner.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.ClientAssessor.ToString().Substring(0, 2).ToUpper(), AppRoles.ClientAssessor.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorSupervisor.ToString().Substring(0, 2).ToUpper(), AppRoles.VendorSupervisor.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(AppRoles.VendorAgent.ToString().Substring(0, 2).ToUpper(), AppRoles.VendorAgent.ToString()));

            //CREATE RISK CASE DETAILS
            var created = new InvestigationCaseStatus
            {
                Name = "CREATED",
                Code = "CREATED"
            };
            var currentCaseStatus1 = await context.InvestigationCaseStatus.AddAsync(created);
            var assigned = new InvestigationCaseStatus
            {
                Name = "ASSIGNED",
                Code = "ASSIGNED"
            };

            var currentCaseStatus2 = await context.InvestigationCaseStatus.AddAsync(assigned);
            var rejected = new InvestigationCaseStatus
            {
                Name = "REJECTED",
                Code = "REJECTED"
            };

            var currentCaseStatus3 = await context.InvestigationCaseStatus.AddAsync(rejected);
            var accepted = new InvestigationCaseStatus
            {
                Name = "ACCEPTED",
                Code = "ACCEPTED"
            };

            var currentCaseStatus4 = await context.InvestigationCaseStatus.AddAsync(accepted);
            var withdrawn = new InvestigationCaseStatus
            {
                Name = "WITHDRAWN",
                Code = "WITHDRAWN"
            };

            var currentCaseStatus5 = await context.InvestigationCaseStatus.AddAsync(withdrawn);

            var clientCreatorCreated = new InvestigationCaseStatus
            {
                Name = "CLIENT_CREATOR_CREATED",
                Code = "CLIENT_CREATOR_CREATED"
            };

            var currentCaseStatus6 = await context.InvestigationCaseStatus.AddAsync(clientCreatorCreated);

            var clientAssignerAccepted = new InvestigationCaseStatus
            {
                Name = "CLIENT_ASSIGNER_ACCEPTED",
                Code = "CLIENT_ASSIGNER_ACCEPTED"
            };

            var currentCaseStatus7 = await context.InvestigationCaseStatus.AddAsync(clientAssignerAccepted);

            var vendorSupervisorAccepted = new InvestigationCaseStatus
            {
                Name = "VENDOR_SUPERVISOR_ACCEPTED",
                Code = "VENDOR_SUPERVISOR_ACCEPTED"
            };

            var currentCaseStatus8 = await context.InvestigationCaseStatus.AddAsync(vendorSupervisorAccepted);
            var vendorAgentAccepted = new InvestigationCaseStatus
            {
                Name = "VENDOR_AGENT_ACCEPTED",
                Code = "VENDOR_AGENT_ACCEPTED"
            };

            var currentCaseStatus9 = await context.InvestigationCaseStatus.AddAsync(vendorAgentAccepted);
            var vendorAgentSubmitted = new InvestigationCaseStatus
            {
                Name = "VENDOR_AGENT_SUBMITTED",
                Code = "VENDOR_AGENT_SUBMITTED"
            };

            var currentCaseStatus10 = await context.InvestigationCaseStatus.AddAsync(vendorAgentSubmitted);

            var vendorSupervisorSubmitted = new InvestigationCaseStatus
            {
                Name = "VENDOR_SUPERVISOR_SUBMITTED",
                Code = "VENDOR_SUPERVISOR_SUBMITTED"
            };

            var currentCaseStatus11 = await context.InvestigationCaseStatus.AddAsync(vendorSupervisorSubmitted);

            var clientAsssessorAccepted = new InvestigationCaseStatus
            {
                Name = "CLIENT_ASSESSOR_ACCEPTED",
                Code = "CLIENT_ASSESSOR_ACCEPTED"
            };

            var currentCaseStatus12 = await context.InvestigationCaseStatus.AddAsync(clientAsssessorAccepted);

            var clientAsssessorSubmitted = new InvestigationCaseStatus
            {
                Name = "CLIENT_ASSESSOR_SUBMITTED",
                Code = "CLIENT_ASSESSOR_SUBMITTED"
            };

            var currentCaseStatus13 = await context.InvestigationCaseStatus.AddAsync(clientAsssessorSubmitted);
            var clientAsssessorRejected = new InvestigationCaseStatus
            {
                Name = "CLIENT_ASSESSOR_REJECTED",
                Code = "CLIENT_ASSESSOR_REJECTED"
            };

            var currentCaseStatus14 = await context.InvestigationCaseStatus.AddAsync(clientAsssessorSubmitted);
            var clientAsssessorReturnRejected = new InvestigationCaseStatus
            {
                Name = "CLIENT_ASSESSOR_RETURN_REJECTED",
                Code = "CLIENT_ASSESSOR_RETURN_REJECTED"
            };

            var currentCaseStatus15 = await context.InvestigationCaseStatus.AddAsync(clientAsssessorReturnRejected);

            var claims = new LineOfBusiness
            {
                Name = "CLAIMS",
                Code = "CLAIMS",
            };

            var currentCaseType1 = await context.LineOfBusiness.AddAsync(claims);
            var underwriting = new LineOfBusiness
            {
                Name = "UNDERWRITING",
                Code = "UNDERWRITING",
            };

            var currentCaseType = await context.LineOfBusiness.AddAsync(underwriting);
            var india = new Country
            {
                Name = "INDIA",
                Code = "IND",
            };
            var indiaCountry = await context.Country.AddAsync(india);
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
                CountryId = indiaCountry.Entity.CountryId,
                Name = "UTTAR PRADESH",
                Code = "UP"
            };

            var upState = await context.State.AddAsync(up);
            var ontario = new State
            {
                CountryId = canadaCountry.Entity.CountryId,
                Name = "ONTARIO",
                Code = "ON"
            };

            var ontarioState = await context.State.AddAsync(ontario);

            var delhi = new State
            {
                CountryId = indiaCountry.Entity.CountryId,
                Name = "NEW DELHI",
                Code = "NDL"
            };

            var delhiState = await context.State.AddAsync(delhi);

            var victoria = new State
            {
                CountryId = australiaCountry.Entity.CountryId,
                Name = "VICTORIA",
                Code = "VIC"
            };

            var victoriaState = await context.State.AddAsync(victoria);

            var tasmania = new State
            {
                CountryId = australiaCountry.Entity.CountryId,
                Name = "TASMANIA",
                Code = "TAS"
            };

            var tasmaniaState = await context.State.AddAsync(tasmania);

            var newDelhi = new PinCode
            {
                Name = "NEW DELHI",
                Code = "110001",
                State = delhiState.Entity,
                Country = indiaCountry.Entity
            };

            var newDelhiPinCode = await context.PinCode.AddAsync(newDelhi);

            var northDelhi = new PinCode
            {
                Name = "NORTH DELHI",
                Code = "110002",
                State = delhiState.Entity,
                Country = indiaCountry.Entity
            };

            var northDelhiPinCode = await context.PinCode.AddAsync(northDelhi);

            var indirapuram = new PinCode
            {
                Name = "INDIRAPURAM",
                Code = "201014",
                State = upState.Entity,
                Country = indiaCountry.Entity
            };

            var indiraPuramPinCode = await context.PinCode.AddAsync(indirapuram);

            var bhelupur = new PinCode
            {
                Name = "BHELUPUR",
                Code = "221001",
                State = upState.Entity,
                Country = indiaCountry.Entity
            };

            var bhelupurPinCode = await context.PinCode.AddAsync(bhelupur);

            var forestHill = new PinCode
            {
                Name = "FOREST HILL",
                Code = "3131",
                State = victoriaState.Entity,
                Country = australiaCountry.Entity
            };

            var forestHillPinCode = await context.PinCode.AddAsync(forestHill);

            var vermont = new PinCode
            {
                Name = "VERMONT",
                Code = "3133",
                State = victoriaState.Entity,
                Country = australiaCountry.Entity
            };

            var vermontPinCode = await context.PinCode.AddAsync(vermont);

            var tasmaniaCity = new PinCode
            {
                Name = "TASMANIA CITY",
                Code = "7000",
                State = tasmaniaState.Entity,
                Country = australiaCountry.Entity
            };

            var tasmaniaCityCode = await context.PinCode.AddAsync(tasmaniaCity);

            var torontoCity = new PinCode
            {
                Name = "TORONTO",
                Code = "9101",
                State = ontarioState.Entity,
                Country = canadaCountry.Entity
            };

            var torontoCityCode = await context.PinCode.AddAsync(tasmaniaCity);

            var _case1 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 1",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus1.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case2 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 2",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus2.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };

            var _case3 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 3",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus3.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case4 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 4",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus4.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case5 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 5",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus5.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case6 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 6",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus6.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case7 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 7",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus7.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case8 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 8",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus2.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case9 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 9",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus3.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case10 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 10",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus4.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case11 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 11",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus6.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case12 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 12",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus5.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case13 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 13",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus3.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case14 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 14",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus1.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };

            var _case15 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 15",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus3.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case16 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 16",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus7.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case17 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 17",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus4.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case18 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 18",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus3.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case19 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 19",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus4.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case20 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 20",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus2.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case21 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 21",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus7.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case22 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 22",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus2.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };
            var _case23 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 23",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus4.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };

            var _case24 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 24",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus5.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };

            var _case25 = new InvestigationCase
            {
                Name = "TEST CLAIM CASE 25",
                Description = "TEST CLAIM CASE DESCRIPTION",
                LineOfBusinessId = currentCaseType.Entity.LineOfBusinessId,
                InvestigationCaseStatusId = currentCaseStatus2.Entity.InvestigationCaseStatusId,
                Created = DateTime.Now
            };

            await context.InvestigationCase.AddAsync(_case1);
            await context.InvestigationCase.AddAsync(_case2);
            await context.InvestigationCase.AddAsync(_case3);
            await context.InvestigationCase.AddAsync(_case4);
            await context.InvestigationCase.AddAsync(_case5);
            await context.InvestigationCase.AddAsync(_case6);
            await context.InvestigationCase.AddAsync(_case7);
            await context.InvestigationCase.AddAsync(_case8);
            await context.InvestigationCase.AddAsync(_case9);
            await context.InvestigationCase.AddAsync(_case10);
            await context.InvestigationCase.AddAsync(_case11);
            await context.InvestigationCase.AddAsync(_case12);
            await context.InvestigationCase.AddAsync(_case13);
            await context.InvestigationCase.AddAsync(_case14);
            await context.InvestigationCase.AddAsync(_case15);
            await context.InvestigationCase.AddAsync(_case16);
            await context.InvestigationCase.AddAsync(_case17);
            await context.InvestigationCase.AddAsync(_case18);
            await context.InvestigationCase.AddAsync(_case19);
            await context.InvestigationCase.AddAsync(_case20);
            await context.InvestigationCase.AddAsync(_case21);
            await context.InvestigationCase.AddAsync(_case22);
            await context.InvestigationCase.AddAsync(_case23);
            await context.InvestigationCase.AddAsync(_case24);
            await context.InvestigationCase.AddAsync(_case25);

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
                Country = indiaCountry.Entity,
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
                Country = indiaCountry.Entity,
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
                Country = indiaCountry.Entity,
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
                Country = indiaCountry.Entity,
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
                Country = indiaCountry.Entity,
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
                Country = indiaCountry.Entity,
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
                Country = indiaCountry.Entity,
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
    }
}
