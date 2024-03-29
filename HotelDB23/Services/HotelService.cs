﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDB23.Models;
using HotelDB23.Interfaces;
using Microsoft.Data.SqlClient;

namespace HotelDB23.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private string insertSql = "insert into Hotel Values(@ID, @Navn, @Adresse)";
        private string deleteSql = "select * from Hotel delete Hotel where Hotel_No =@ID";
        private String updateSql = "update Hotel set Name=@navn, Address=@Addresse where Hotel_No=@ID";
        private string queryStringFromName = "select * from Hotel where Name=@navn";


        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings


        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryString, connection);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
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
            }
            return hoteller;
        }

        public Hotel GetHotelFromId(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(queryStringFromID, connection);
                    commmand.Parameters.AddWithValue("@ID", hotelNr);
                    commmand.Connection.Open();


                    SqlDataReader reader = commmand.ExecuteReader();
                    if (reader.Read())
                    {
                        int hotelNo = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNo, hotelNavn, hotelAdr);
                        return hotel;
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
            }
            return null;
        }


        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Connection.Open();
                    int noOfRows = command.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }
            }
            return false;
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                try
                {
                    SqlCommand commmand = new SqlCommand(updateSql, connection);


                    commmand.Parameters.AddWithValue("@navn", hotel.Navn);
                    commmand.Parameters.AddWithValue("@Addresse", hotel.Adresse);
                    commmand.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    commmand.Connection.Open();

                    int noOfRows = commmand.ExecuteNonQuery();
                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database error " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl " + ex.Message);
                }

                return false;




            }




        }

        public Hotel DeleteHotel(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand commmand = new SqlCommand(deleteSql, connection);
                    commmand.Parameters.AddWithValue("@ID", hotelNr);
                    commmand.Connection.Open();


                    SqlDataReader reader = commmand.ExecuteReader();
                    if (reader.Read())
                    {
                        int hotelNo = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNo, hotelNavn, hotelAdr);
                        return hotel;
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
            }
            return null;
        }

        public List<Hotel> GetHotelsByName(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
                try
                {

                    SqlCommand commmand = new SqlCommand(queryStringFromName, connection);

                    commmand.Parameters.AddWithValue("@navn", name);
                    commmand.Connection.Open();
                    SqlDataReader reader = commmand.ExecuteReader();
                    while (reader.Read())
                    {

                        int hotelNr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
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

            return hoteller;
        }
    }
}
