namespace Tests;

public static class AssertHelpers
{
    public static void EqualAssert<T>(T first, T second)
    {
        Assert.That(first, Is.EqualTo(second));
    }
}