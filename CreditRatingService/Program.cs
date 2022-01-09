using AutoMapper;
using AutoMapper.Data;
using CreditRatingService.UseCase;
using CreditRatingService.UseCase.Entities;
using GrpcCredit;
using GrpcCredit.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<ICreditRatingUseCase, CreditRatingUseCase>();

#region Automapper
builder.Services.AddAutoMapper(v =>
{
    v.AddDataReaderMapping();
});
Mapper.Initialize(cfg =>
{
    cfg.CreateMap<InfoCredit, CreditRequest>().ReverseMap();
    cfg.CreateMap<InfoCreditResponse, CreditResponse>().ReverseMap();
    cfg.CreateMap<InfoCreditUserResponse, CreditUserResponse>().ReverseMap();
    cfg.CreateMap<InfoFee, FeeRequest>().ReverseMap();
    cfg.CreateMap<InfoFeeResponse, FeeResponse>().ReverseMap();
});
#endregion Automapper

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CreditService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
