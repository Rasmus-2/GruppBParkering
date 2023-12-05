using Dapper;
using GruppBParkeringshuset.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppBParkeringshuset
{
    internal class DatabaseDapper
    {
        //Kolla SQLEXPRESS Namnet om det inte funkar
        static string connString = "data source=.\\SQLEXPRESS05; initial catalog = Parking10; persist security info = True; Integrated Security = True;"; 
        public static List<Models.City> GetAllCities()
        {
            string sql = "SELECT * FROM CITIES";
            List<Models.City> cities = new List<Models.City>();
            using (var connection = new SqlConnection(connString))
            {
                cities = connection.Query<Models.City>(sql).ToList();
            }
            return cities;
        }

        public static int InsertCity(Models.City city)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO Cities(CityName) VALUES('{city.CityName}')";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }

        public static List<Models.ParkingHouse> GetAllParkingHouses()
        {
            string sql = "SELECT * FROM ParkingHouses";
            List<Models.ParkingHouse> parkingHouses = new List<Models.ParkingHouse>();
            using (var connection = new SqlConnection(connString))
            {
                parkingHouses = connection.Query<Models.ParkingHouse>(sql).ToList();
            }
            return parkingHouses;
        }

        public static int InsertParkingHouse(Models.ParkingHouse parkingHouse)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO ParkingHouses(HouseName, CityId) VALUES('{parkingHouse.HouseName}', '{parkingHouse.CityId}')";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }

        public static int InsertParkingSlot(Models.ParkingSlots parkingSlots)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO ParkingSlots(SlotNumber, ElectricOutlet, ParkingHouseId) VALUES ('{parkingSlots.SlotNumber}', '{parkingSlots.ElectricOutlet}', '{parkingSlots.ParkingHouseId}')";
            using ( var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }
        public static List<Models.AllSpots> GetParkingSlots()
        {
            string sql2 = @"
                SELECT                     
                    COUNT(*) AS PlatserPerHus,
                        ph.HouseName,
                    STRING_AGG(ps.SlotNumber, ', ') AS Slots
                FROM ParkingHouses ph
                JOIN ParkingSlots ps ON ph.Id = ps.ParkingHouseId
                GROUP BY ph.HouseName
                

    ";
            string sql = "SELECT * FROM ParkingSlots";
            List<Models.AllSpots> parkingSlots = new List<AllSpots>();
            using (var connection = new SqlConnection(connString))
            {
                parkingSlots = connection.Query<Models.AllSpots>(sql2).ToList();
            }
            return parkingSlots;
        }
    }
}
