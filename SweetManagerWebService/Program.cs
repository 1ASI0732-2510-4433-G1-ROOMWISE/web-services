using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using sweetmanager.API.Shared.Domain.Repositories;

using SweetManagerWebService.Shared.Infrastructure.Interfaces.ASP.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerWebService.Shared.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.SupplyManagement.Application.Internal.SuppliesRequest;
using SweetManagerWebService.SupplyManagement.Application.Internal.Supply;
using SweetManagerWebService.SupplyManagement.Domain.Repositories;
using SweetManagerWebService.SupplyManagement.Domain.Services;
using SweetManagerWebService.SupplyManagement.Infrastructure.Persistence.Repositories;
using SweetManagerWebService.Commerce.Application.Internal.CommandServices.Contracts;
using SweetManagerWebService.Commerce.Application.Internal.CommandServices.Payments;
using SweetManagerWebService.Commerce.Application.Internal.CommandServices.Subscriptions;
using SweetManagerWebService.Commerce.Application.Internal.QueryServices.Contracts;
using SweetManagerWebService.Commerce.Application.Internal.QueryServices.Dashboard;
using SweetManagerWebService.Commerce.Application.Internal.QueryServices.Payments;
using SweetManagerWebService.Commerce.Application.Internal.QueryServices.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Repositories.Contracts;
using SweetManagerWebService.Commerce.Domain.Repositories.Payments;
using SweetManagerWebService.Commerce.Domain.Repositories.Subscriptions;
using SweetManagerWebService.Commerce.Domain.Services.Contracts;
using SweetManagerWebService.Commerce.Domain.Services.Payments;
using SweetManagerWebService.Commerce.Domain.Services.Subscriptions;
using SweetManagerWebService.Commerce.Infrastructure.Persistence.Dapper.Dashboard;
using SweetManagerWebService.Commerce.Infrastructure.Persistence.EFC.Repositories.Contracts;
using SweetManagerWebService.Commerce.Infrastructure.Persistence.EFC.Repositories.Payments;
using SweetManagerWebService.Commerce.Infrastructure.Persistence.EFC.Repositories.Subscriptions;
using SweetManagerWebService.Communication.Application.CommandService;
using SweetManagerWebService.Communication.Application.QueryService;
using SweetManagerWebService.Communication.Domain.Repositories;
using SweetManagerWebService.Communication.Domain.Services.Notification;
using SweetManagerWebService.Communication.Domain.Services.TypeNotification;
using SweetManagerWebService.Communication.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.Communication.Infrastructure.Population.TypeNotifications;
using SweetManagerWebService.IAM.Application.Internal.CommandServices.Assignments;
using SweetManagerWebService.IAM.Application.Internal.CommandServices.Credential;
using SweetManagerWebService.IAM.Application.Internal.CommandServices.Roles;
using SweetManagerWebService.IAM.Application.Internal.CommandServices.User;
using SweetManagerWebService.IAM.Application.Internal.OutboundServices;
using SweetManagerWebService.IAM.Application.Internal.OutboundServices.ACL;
using SweetManagerWebService.IAM.Application.Internal.QueryServices.Assignments;
using SweetManagerWebService.IAM.Application.Internal.QueryServices.Credential;
using SweetManagerWebService.IAM.Application.Internal.QueryServices.Roles;
using SweetManagerWebService.IAM.Application.Internal.QueryServices.User;
using SweetManagerWebService.IAM.Domain.Repositories.Assignments;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.IAM.Domain.Repositories.Roles;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.IAM.Domain.Services.Assignments;
using SweetManagerWebService.IAM.Domain.Services.Credential.Admin;
using SweetManagerWebService.IAM.Domain.Services.Credential.Owner;
using SweetManagerWebService.IAM.Domain.Services.Credential.Worker;
using SweetManagerWebService.IAM.Domain.Services.Roles;
using SweetManagerWebService.IAM.Domain.Services.Users.Admin;
using SweetManagerWebService.IAM.Domain.Services.Users.Owner;
using SweetManagerWebService.IAM.Domain.Services.Users.Worker;
using SweetManagerWebService.IAM.Infrastructure.Hashing.Argon2Id.Services;
using SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Assignment;
using SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Credential;
using SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.Roles;
using SweetManagerWebService.IAM.Infrastructure.Persistence.EFC.Repositories.User;
using SweetManagerWebService.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using SweetManagerWebService.IAM.Infrastructure.Population.Roles;
using SweetManagerWebService.IAM.Infrastructure.Tokens.JWT.Configuration;
using SweetManagerWebService.IAM.Infrastructure.Tokens.JWT.Services;
using SweetManagerWebService.Monitoring.Application.Internal.CommandServices;
using SweetManagerWebService.Monitoring.Application.Internal.QueryServices;
using SweetManagerWebService.Monitoring.Domain.Repositories;
using SweetManagerWebService.Monitoring.Domain.Services.Booking;
using SweetManagerWebService.Monitoring.Domain.Services.Room;
using SweetManagerWebService.Monitoring.Domain.Services.TypeRoom;
using SweetManagerWebService.Monitoring.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.Monitoring.Interfaces.ACL;
using SweetManagerWebService.Monitoring.Interfaces.ACL.Services;
using SweetManagerWebService.Profiles.Application.Internal.CommandService;
using SweetManagerWebService.Profiles.Application.Internal.QueryService;
using SweetManagerWebService.Profiles.Domain.Repositories;
using SweetManagerWebService.Profiles.Domain.Services.Customer;
using SweetManagerWebService.Profiles.Domain.Services.Hotel;
using SweetManagerWebService.Profiles.Domain.Services.Provider;
using SweetManagerWebService.Profiles.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.Profiles.Interfaces.ACL;
using SweetManagerWebService.Profiles.Interfaces.ACL.Services;
using SweetManagerWebService.ResourceManagement.Application.CommandService;
using SweetManagerWebService.ResourceManagement.Application.QueryService;
using SweetManagerWebService.ResourceManagement.Domain.Repositories;
using SweetManagerWebService.ResourceManagement.Domain.Services.Report;
using SweetManagerWebService.ResourceManagement.Domain.Services.TypeReport;
using SweetManagerWebService.ResourceManagement.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerWebService.ResourceManagement.Infrastructure.Population.TypeReports;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer(); 

builder.Services.AddSwaggerGen();


#region Database Configuration
// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("SweetManager");

builder.Services.AddTransient<IDbConnection>(db => new MySqlConnection(connectionString));

var connectionStringFromEnvironment = Environment.GetEnvironmentVariable("SweetManagerDbConnection");

if (connectionStringFromEnvironment != null)
{
    connectionString = connectionStringFromEnvironment;
}

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<SweetManagerContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });

#endregion

#region OPENAPI Configuration
// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Sweet Manager API",
                Version = "v1",
                Description = "Sweet Manager API",
                TermsOfService = new Uri("https://acme-learning.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Sweet Manager Studios",
                    Email = "contact@swm.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer", Type = ReferenceType.SecurityScheme
                    } 
                }, 
                Array.Empty<string>()
            }
        });
    });

#endregion

builder.Services.AddHttpContextAccessor();

#region Dependency Injection

// IAM BOUNDED CONTEXT
builder.Services.AddScoped<IAdminCommandService, AdminCommandService>();
builder.Services.AddScoped<IAdminQueryService, AdminQueryService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddScoped<IOwnerCommandService, OwnerCommandService>();
builder.Services.AddScoped<IOwnerQueryService, OwnerQueryService>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

builder.Services.AddScoped<IWorkerCommandService, WorkerCommandService>();
builder.Services.AddScoped<IWorkerQueryService, WorkerQueryService>();
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();

builder.Services.AddScoped<IAdminCredentialCommandService, AdminCredentialCommandService>();
builder.Services.AddScoped<IAdminCredentialQueryService, AdminCredentialQueryService>();
builder.Services.AddScoped<IAdminCredentialRepository, AdminCredentialRepository>();

builder.Services.AddScoped<IOwnerCredentialCommandService, OwnerCredentialCommandService>();
builder.Services.AddScoped<IOwnerCredentialQueryService, OwnerCredentialQueryService>();
builder.Services.AddScoped<IOwnerCredentialRepository, OwnerCredentialRepository>();

builder.Services.AddScoped<IWorkerCredentialCommandService, WorkerCredentialCommandService>();
builder.Services.AddScoped<IWorkerCredentialQueryService, WorkerCredentialQueryService>();
builder.Services.AddScoped<IWorkerCredentialRepository, WorkerCredentialRepository>();

builder.Services.AddScoped<IRoleCommandService, RoleCommandService>();
builder.Services.AddScoped<IRoleQueryService, RoleQueryService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IWorkerAreaCommandService, WorkerAreaCommandService>();
builder.Services.AddScoped<IWorkerAreaQueryService, WorkerAreaQueryService>();
builder.Services.AddScoped<IWorkerAreaRepository, WorkerAreaRepository>();

builder.Services.AddScoped<IAssignmentWorkerCommandService, AssignmentWorkerCommandService>();
builder.Services.AddScoped<IAssignmentWorkerQueryService, AssignmentWorkerQueryService>();
builder.Services.AddScoped<IAssignmentWorkerRepository, AssignmentWorkerRepository>();

builder.Services.AddScoped<ExternalMonitoringService>();

builder.Services.AddScoped<ExternalProfilesService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Monitoring Bounded Context

builder.Services.AddScoped<IBookingCommandService, BookingCommandService>();
builder.Services.AddScoped<IBookingQueryService, BookingQueryService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<ITypeRoomCommandService, TypeRoomCommandService>();
builder.Services.AddScoped<ITypeRoomQueryService, TypeRoomQueryService>();
builder.Services.AddScoped<ITypeRoomRepository, TypeRoomRepository>();

builder.Services.AddScoped<IMonitoringContextFacade, MonitoringContextFacade>();

builder.Services.AddScoped<RolesInitializer>();

// COMMERCE BOUNDED CONTEXT

builder.Services.AddScoped<IContractOwnerRepository, ContractOwnerRepository>();
builder.Services.AddScoped<IPaymentCustomerRepository, PaymentCustomerRepository>();
builder.Services.AddScoped<IPaymentOwnerRepository, PaymentOwnerRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

builder.Services.AddScoped<IContractOwnerCommandService, ContractOwnerCommandService>();
builder.Services.AddScoped<IContractOwnerQueryService, ContractOwnerQueryService>();
builder.Services.AddScoped<IPaymentCustomerCommandService, PaymentCustomerCommandService>();
builder.Services.AddScoped<IPaymentCustomerQueryService, PaymentCustomerQueryService>();
builder.Services.AddScoped<IPaymentOwnerCommandService, PaymentOwnerCommandService>();
builder.Services.AddScoped<IPaymentOwnerQueryService, PaymentOwnerQueryService>();

builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();

// SUPPLY MANAGEMENT BOUNDED CONTEXT
builder.Services.AddScoped<ISupplyRepository, SupplyRepository>(); 
builder.Services.AddScoped<ISupplyCommandService, SupplyCommandService>();
builder.Services.AddScoped<ISupplyQueryService, SupplyQueryService>();

builder.Services.AddScoped<ISuppliesRequestCommandService, SuppliesRequestCommandService>(); 
builder.Services.AddScoped<ISuppliesRequestQueryService, SuppliesRequestQueryService>();
builder.Services.AddScoped<ISuppliesRequestRepository, SuppliesRequestRepository>();
builder.Services.AddScoped<ISupplyCommandService, SupplyCommandService>();
builder.Services.AddScoped<ISupplyQueryService, SupplyQueryService>();

// RESOURCE MANAGEMENT BOUNDED CONTEXT

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ITypeReportRepository, TypeReportRepository>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();
builder.Services.AddScoped<ITypeReportQueryService, TypeReportQueryService>();
builder.Services.AddScoped<ITypeReportCommandService, TypeReportCommandService>();

builder.Services.AddScoped<TypeReportsInitializer>();

// COMMUNICATION BOUNDED CONTEXT

builder.Services.AddScoped<ITypeNotificationRepository, TypeNotificationRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<ITypeNotificationQueryService, TypeNotificationQueryService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();
builder.Services.AddScoped<ITypeNotificationCommandService, TypeNotificationCommandService>();

builder.Services.AddScoped<TypeNotificationsInitializer>();

// PROFILES BOUNDED CONTEXT

builder.Services.AddScoped<ICustomerCommandService, CustomerCommandService>();
builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();
builder.Services.AddScoped<IHotelCommandService, HotelCommandService>();
builder.Services.AddScoped<IHotelQueryService, HotelQueryService>();
builder.Services.AddScoped<IProviderCommandService, ProviderCommandService>();
builder.Services.AddScoped<IProviderQueryService, ProviderQueryService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();

builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();



#endregion 

#region JWT Configuration

var tokenSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.Configure<TokenSettings>(tokenSettings);

var secretKey = tokenSettings["SecretKey"];

var audience = tokenSettings["Audience"];

var issuer = tokenSettings["Issuer"];

var securityKey = !string.IsNullOrEmpty(secretKey)
    ? new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey))
    : throw new ArgumentException("Secret key is null or empty");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = securityKey,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddTransient<TokenValidationHandler>();

builder.Services.AddAuthorization();

#endregion

var app = builder.Build();

#region Ensure Database Created (COMPILE AppDbContext)
// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SweetManagerContext>();
    context.Database.EnsureCreated();
}

#endregion

#region Run DatabaseInitializer
using (var scope = app.Services.CreateScope())
{
    var roleInitializer = scope.ServiceProvider.GetRequiredService<RolesInitializer>();

    var typeReportInitializer = scope.ServiceProvider.GetRequiredService<TypeReportsInitializer>();

    var notificationInitializer = scope.ServiceProvider.GetRequiredService<TypeNotificationsInitializer>();
    
    roleInitializer.InitializeAsync().Wait();

    typeReportInitializer.InitializeAsync().Wait();

    notificationInitializer.InitializeAsync().Wait();
}
#endregion

// Configuration cors
app.UseCors(
    b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() 
);


app.UseSwagger();

app.UseSwaggerUI();


app.UseRouting();

app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();