using Xunit;
using Garage;

public class GarageTests
{
    [Fact]
    public void AddVehicle_WhenGarageHasSpace_ReturnsTrue()
    {
        // Arrange
        var garage = new Garage<Car>(2); 
        var car = new Car(4, "Röd", "ABC123", "Bensin");

        // Act
        bool result = garage.Add(car);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AddVehicle_WhenGarageIsFull_ReturnsFalse()
    {
        // Arrange
        var garage = new Garage<Car>(2);
        garage.Add(new Car(4, "Blå", "DEF456", "Diesel"));
        garage.Add(new Car(4, "Grön", "GHI789", "El"));

        var extraCar = new Car(4, "Svart", "JKL012", "Bensin");

        // Act
        bool result = garage.Add(extraCar);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RemoveVehicle_WhenVehicleExists_ReturnsTrue()
    {
        // Arrange
        var garage = new Garage<Car>(2);
        var car = new Car(4, "Vit", "MNO345", "Diesel");
        garage.Add(car);

        // Act
        bool result = garage.Remove(car);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void RemoveVehicle_WhenVehicleDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var garage = new Garage<Car>(2);
        var car = new Car(4, "Vit", "MNO345", "Diesel");

        // Act
        bool result = garage.Remove(car);

        // Assert
        Assert.False(result);
    }
}
