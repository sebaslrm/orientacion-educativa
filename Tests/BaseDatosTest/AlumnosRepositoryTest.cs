using BaseDatos;
using BaseDatos.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BaseDatosTest;

public class AlumnosRepositoryTest
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

        services.AddTransient<AlumnosRepository>();

        this.serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        if(this.serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    [Test]
    public async Task ObtieneDatosDeAlumnos()
    {
        var repository = this.serviceProvider.GetRequiredService<AlumnosRepository>();
        var list = await repository.ListarTodos();

        int cuantosAlumnos = list.Count;
        int registrosEsperados = 51;

        Assert.That(cuantosAlumnos > 0, "No contiene alumnos");
        Assert.That(cuantosAlumnos == registrosEsperados, $"Se esperaban {registrosEsperados} alumnos, pero se obtuvieron {cuantosAlumnos}");
    }

    [Test]
    public async Task ObtieneUnAlumnoYValidaSuNombre_200_OK()
    {
        long numControl = 24328051051234;
        string apellido = "Contreras";

        var repository = this.serviceProvider.GetRequiredService<AlumnosRepository>();
        var alumno = await repository.ObtenerAlumnoPorNumeroDeControl(numControl);

        Assert.That(alumno is not null, $"No existe un alumno con el número de control {numControl}");
        if(alumno != null)
        {
            Assert.That(
                string.Equals(alumno.Paterno, apellido, 
                StringComparison.OrdinalIgnoreCase), 
                $"El apellido esperado es {apellido} y el registro obtuvo {alumno.Paterno}");
        }
    }

    [Test]
    public async Task BuscaUnAlumnoYObtiene_404_NotFound()
    {
        long numControl = 24328051059999;
        var repository = this.serviceProvider.GetRequiredService<AlumnosRepository>();
        var alumno = await repository.ObtenerAlumnoPorNumeroDeControl(numControl);

        Assert.That(alumno is null, "El alumno existe y no debería existir");
    }
}