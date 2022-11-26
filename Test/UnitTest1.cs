using API.Recipes;
using Moq;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Test1()
    {
        var repository = new Mock<IRecipeRepository>();
        Assert.Ignore();
    }
    
}