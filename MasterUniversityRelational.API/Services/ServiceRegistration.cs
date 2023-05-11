using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterUniversityRelational.API.Interfaces;
using MasterUniversityRelational.API.Services.Databases;

namespace MasterUniversityRelational.API.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataService, SqlDataService>();

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ILecturerService, LecturerService>();

            return services;
        }
    }
}
