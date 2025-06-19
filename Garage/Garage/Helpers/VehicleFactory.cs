using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Helpers
{
    internal class VehicleFactory
    {
        private readonly InputHelper inputHelper;

        public VehicleFactory(InputHelper inputHelper)
        {
            this.inputHelper = inputHelper;
        }

        public Vehicle CreateVehicle(string type)
        {
            switch (type.ToLower())
            {
                case "car":
                    return CreateCar();
                case "motorcycle":
                    return CreateMotorcycle();
                case "bus":
                    return CreateBus();
                case "airplane":
                    return CreateAirplane();
                case "boat":
                    return CreateBoat();
                default:
                    throw new ArgumentException("Ogiltig fordonstyp.");
            }
        }

        private Vehicle CreateCar()
        {
            int wheels = inputHelper.GetPositiveInt("Ange antal hjul:");
            string color = inputHelper.GetText("Ange färg:");
            string license = inputHelper.GetValidLicense();
            string fuel = inputHelper.GetText("Ange bränsletyp:");
            return new Car(wheels, color, license, fuel);
        }

        private Vehicle CreateMotorcycle()
        {
            int wheels = inputHelper.GetPositiveInt("Ange antal hjul:");
            string color = inputHelper.GetText("Ange färg:");
            string license = inputHelper.GetValidLicense();
            int engineVolume = inputHelper.GetPositiveInt("Ange enginevolym:");
            return new Motorcycle(wheels, color, license, engineVolume);
        }

        private Vehicle CreateBus()
        {
            int wheels = inputHelper.GetPositiveInt("Ange antal hjul:");
            string color = inputHelper.GetText("Ange färg:");
            string license = inputHelper.GetValidLicense();
            int seats = inputHelper.GetPositiveInt("Ange antal säten:");
            return new Bus(wheels, color, license, seats);
        }

        private Vehicle CreateAirplane()
        {
            int wheels = inputHelper.GetPositiveInt("Ange antal hjul:");
            string color = inputHelper.GetText("Ange färg:");
            string license = inputHelper.GetValidLicense();
            int engines = inputHelper.GetPositiveInt("Ange antal motorer:");
            return new Airplane(wheels, color, license, engines);
        }

        private Vehicle CreateBoat()
        {
            string color = inputHelper.GetText("Ange färg:");
            string license = inputHelper.GetValidLicense();
            int length = inputHelper.GetPositiveInt("Ange längd på båten:");
            return new Boat(color, license, length);
        }
    }

}
