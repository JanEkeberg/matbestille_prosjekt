using MatBestille.Enums;
using MatBestille.Models;
using Xunit;

namespace MatBestille.Tests.Models;

public class ProductTests
{
    // Tests that a baguette is created with its fixed price and allergen.
    [Fact]
    public void Baguette_ShouldHaveFixedPriceAndAllergen()
    {
        var baguette = new Baguette("Chicken Baguette", "Gluten");

        Assert.Equal("Chicken Baguette", baguette.Name);
        Assert.Equal("Gluten", baguette.Allergen);
        Assert.Equal(60m, baguette.Price);
    }

    // Tests that a wrap is created with its fixed price, allergens, and bread type.
    [Fact]
    public void Wraps_ShouldHaveFixedPriceAllergensAndType()
    {
        var wrap = new Wraps("Caesar Wrap", "Egg", true);

        Assert.Equal("Caesar Wrap", wrap.Name);
        Assert.Equal("Egg", wrap.Allergens);
        Assert.True(wrap.IsGrov);
        Assert.Equal(65m, wrap.Price);
    }

    // Tests that extra product types store their important values correctly.
    [Fact]
    public void OtherProducts_ShouldStoreImportantProductValues()
    {
        var drink = new Drikker("Cola", 35m, BottleSize.Large);
        var fruits = new Fruits("Fruit Basket", 120m, 5);
        var cake = new Kake("Chocolate Cake", 300m, 10);

        Assert.Equal(BottleSize.Large, drink.Size);
        Assert.Equal(35m, drink.Price);
        Assert.Equal(5, fruits.ForHowManyPeople);
        Assert.Equal(10, cake.ForHowManyPeople);
    }

    // Tests that a product cannot be created with zero or negative price.
    [Fact]
    public void Product_ShouldRejectInvalidPrice()
    {
        Assert.Throws<ArgumentException>(() =>
            new Drikker("Cola", 0m, BottleSize.Small));
    }
}
