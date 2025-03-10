var builder = WebApplication.CreateBuilder( args );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors( options => {
  options.AddPolicy( "FundifyClient", policy => {
    var clientUrl = Environment.GetEnvironmentVariable( "CLIENT_URL" );
    if (!string.IsNullOrEmpty( clientUrl )) {
      policy.WithOrigins( clientUrl )
            .AllowAnyHeader()
            .AllowAnyMethod();
    }
  } );
} );

var app = builder.Build();

app.UseCors( "FundifyClient" );

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
