﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using EmployeeManagement.BLL.Contracts;
using EmployeeManagement.BLL.Manager;
using EmployeeManagement.DatabaseContext.DatabaseContext;
using EmployeeManagement.Repositories.Contracts;
using EmployeeManagement.Repositories.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http;

namespace EmployeeManagement.Configuration
{
   public static class AppConfiguration
    {
        
        
            public static  void ConfigureServices(IServiceCollection services, IConfiguration configuration)
            {
                services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"), b => b.MigrationsAssembly("EmployeeManagement.DatabaseContext")));
       
                services.AddTransient<IEmployeeRepository>(c =>
                {
                    if (1 == 1)
                    {
                        return new EmployeeRepository();
                    }
                   
                });
            services.AddTransient<IDepartmentRepository>(
                c =>
                {
                    if (1 == 1)
                    {
                        return new DepartmentRepository();
                    }
                });
             services.AddTransient<IEmployeeManager, EmployeeManager>();
             services.AddTransient<IDepartmentManager, DepartmentManager>();
            services.AddAutoMapper();
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options => {
                        options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore; 

                          });
            }
        }
    }

