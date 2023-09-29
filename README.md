Install-Package Microsoft.EntityFrameworkCore -version 5.0.10

Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.10


Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.10



Scaffold-DbContext "Data Source=MSI\SQLEXPRESS;Initial Catalog=QLSP;User ID=sa;Password=123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force



builder.Services.AddDbContext<QlspContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myDB"));
});

