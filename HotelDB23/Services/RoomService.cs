using HotelDB23.Interfaces;
using HotelDB23.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDB23.Services
{
    public class RoomService : Connection, IRoomService
    {
        private String queryStringFromID = "select * from Room where Hotel_No = @ID";
        public bool CreateRoom(int hotelNr, Room room)
        {
            throw new NotImplementedException();
        }

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAllRoom(int hotelNr)
        {
            List<Room> roomer = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
                try
                {

                    SqlCommand commmand = new SqlCommand(queryStringFromID, connection);

                    commmand.Parameters.AddWithValue("@ID", hotelNr);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {

                        int roomNr = reader.GetInt32(0);
                        string tpes = reader.GetString(2);
                        char ttemp = tpes[0];
                        int hotelNm  = reader.GetInt32(1);
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNr, ttemp, price, hotelNm);
                        roomer.Add(room);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
                finally
                {
                    //her kommer man altid
                }

            return roomer;
        }

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }
    }
}
