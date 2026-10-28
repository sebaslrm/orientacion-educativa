namespace BaseDatosTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PruebaCorrecta()
    {
        Assert.That(1 + 1 == 2, "Este mensaje se muestra si la prueba falla");
    }

    [Test]
    public void PruebaIncorrecta()
    {
        Assert.That(1 + 1 == 3, "Esta prueba es incorrecta");
    }
}
