using GruppBParkeringshuset.Models;

namespace GruppBParkeringshuset
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;

            while (!quit)
            {
                Console.Clear();

                Console.WriteLine("Press c to add city");
                Console.WriteLine("Press b to add car");
                Console.WriteLine("Press s to show cities");
                Console.WriteLine("Press p to show parking houses");
                Console.WriteLine("Press h to add parking house");
                Console.WriteLine("Press z to show parking slots");
                Console.WriteLine("Press x to add parking slots");
                Console.WriteLine("Press q to quit");

                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case 'c':
                        Console.Write("\nEnter city name: ");
                        string cityName = Console.ReadLine();
                        Models.City newCity = new Models.City
                        {
                            CityName = cityName
                        };
                        int rowsAffected = DatabaseDapper.InsertCity(newCity);
                        Console.WriteLine("cities added: " + rowsAffected);
                        Thread.Sleep(1000);
                        break;

                    case 's':
                        Console.WriteLine("All cities");
                        List<Models.City> cities = DatabaseDapper.GetAllCities();
                        foreach (Models.City city in cities)
                        {
                            Console.Write(city.Id + " ");
                            Console.WriteLine(city.CityName);
                        }
                        Thread.Sleep(2000);
                        break;

                    case 'p':
                        Console.WriteLine("\nAll parking houses");
                        List<Models.ParkingHouse> parkingHouses = DatabaseDapper.GetAllParkingHouses();
                        foreach (Models.ParkingHouse parkingHouse in parkingHouses)
                        {
                            Console.Write(parkingHouse.Id + " ");
                            Console.Write(parkingHouse.HouseName + " city#: ");
                            Console.WriteLine(parkingHouse.CityId);
                        }
                        Thread.Sleep(5000);
                        break;

                    case 'h':
                        Console.Write("Enter parking house name: ");
                        string parkingHouseInput = Console.ReadLine();
                        Console.Write("Enter city id: ");
                        int cityId = int.Parse(Console.ReadLine());

                        Models.ParkingHouse newParkingHouse = new Models.ParkingHouse
                        {
                            HouseName = parkingHouseInput,
                            CityId = cityId
                        };
                        int rowsAffected2 = DatabaseDapper.InsertParkingHouse(newParkingHouse);
                        Console.WriteLine("parking houses added: " + rowsAffected2);
                        Thread.Sleep(1000);
                        break;

                    case 'z':
                        Console.WriteLine("\nAll parking slots");
                        List<Models.AllSpots> parkingSlots = DatabaseDapper.GetParkingSlots();
                        foreach (Models.AllSpots parkingSlot in parkingSlots)
                        {
                            Console.Write("Housename: "+ parkingSlot.HouseName);
                            Console.Write(", Number of slots: " + parkingSlot.Slots);
                            Console.WriteLine(", Spots per house: " + parkingSlot.PlatserPerHus);
                            //Console.WriteLine(", Parking house id: " + parkingSlot.ParkingHouseId);
                        }
                        Thread.Sleep(5000);
                        break;
                    case 'x':
                        Console.WriteLine("Enter parking house Id: ");
                        int parkingHouseName = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter if its an Electric Outlet (True/False: ");
                        string electricOutlet = Console.ReadLine();
                        Console.WriteLine("Enter Slot number: ");
                        int slotNumber = int.Parse(Console.ReadLine());
                        Models.ParkingSlots parkingSlots1 = new ParkingSlots
                        {
                            ParkingHouseId = parkingHouseName,
                            SlotNumber = slotNumber

                        };
                        if (electricOutlet.ToLower() == "true")
                        {
                            parkingSlots1.ElectricOutlet = 1;

                        }
                        else
                        {
                            parkingSlots1.ElectricOutlet = 0;
                        }
                        int rowAffected3 = DatabaseDapper.InsertParkingSlot(parkingSlots1);
                        Console.WriteLine("parking slots added: " + rowAffected3);
                        Thread.Sleep(5000);
                        break;


                    case 'q':
                        quit = true;
                        break;
                }
            }
        }
    }
}