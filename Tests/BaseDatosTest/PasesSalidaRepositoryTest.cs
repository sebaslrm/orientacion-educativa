using System.Runtime.CompilerServices;
using BaseDatos;
using BaseDatos.Documents;
using BaseDatos.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace BaseDatosTest;

public class PasesSalidaRepositoryTest
{
    private IServiceProvider serviceProvider;

    [SetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddTransient<PasesSalidaRepository>();

        this.serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        if (this.serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    // 1.Listar pases de salida para un alumno existente 
    [Test]
    public async Task ListarPasesAlumno()
    {
        var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
        var list = await repository.ListarPasesAlumno(24328051053245);
        int cantidadPasesEsperados = 2;

        int cantidadPasesObtenidos = list.Count;
        Assert.That(cantidadPasesEsperados == cantidadPasesObtenidos, $"Se esperaban {cantidadPasesEsperados} registros, pero se obtuvieron {cantidadPasesObtenidos} registros de la base de datos");
    }
}
  