using Dapper;
using GruppBParkeringshuset.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GruppBParkeringshuset
{
    internal class DatabaseDapper
    {
        //Kolla SQLEXPRESS Namnet om det inte funkar
        static string connString = "data source=.\\SQLEXPRESS05; initial catalog = Parking10; persist security info = True; Integrated Security = True;"; 
        public static List<Models.City> GetAllCities()
        {
            string sql = @"SELECT
                c.Id, c.CityName,
                SUM(CAST(ps.ElectricOutlet AS INT)) AS ElectricOutletCount
                FROM Cities c
                JOIN ParkingHouses ph ON c.Id = ph.CityId
                JOIN ParkingSlots ps ON ph.id = ps.ParkingHouseId
                GROUP BY c.Id, c.CityName
        ";
            
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
                    ph.Id AS Id,
                    COUNT(*) AS PlatserPerHus,
                        ph.HouseName,
                    STRING_AGG(ps.SlotNumber, ', ') AS Slots,
                    SUM(CAST(ps.ElectricOutlet AS INT)) AS ElectricOutletCount 
                FROM ParkingHouses ph
                JOIN ParkingSlots ps ON ph.Id = ps.ParkingHouseId
                GROUP BY ph.HouseName, ph.Id
                

    ";
            string sql = "SELECT * FROM ParkingSlots";
            List<Models.AllSpots> parkingSlots = new List<AllSpots>();
            using (var connection = new SqlConnection(connString))
            {
                parkingSlots = connection.Query<Models.AllSpots>(sql2).ToList();
            }
            return parkingSlots;
        }

        //public static int GetElectricOutlets()
        //{
        //    string sql = @"SELECT ElectricOutlet FROM ParkingSlots WHERE ElectricOutlet = 1";

        //}
    }
}
