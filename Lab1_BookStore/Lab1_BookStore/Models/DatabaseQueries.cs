using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlServerCe;

namespace Lab1_BookStore.Models
{
    public class DatabaseQueries
    {
        private static SqlCeConnection CreateConnection()
        {
            SqlCeConnection conn = new SqlCeConnection();
            conn.ConnectionString =
                ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;
            conn.Open();
            return conn;
        }

        public static void CreateRecord(BookStoreModel book)
        {
            string cmdText =
                @"INSERT INTO Books (Title, Author, PublishedDate, Cost, InStock, BindingType)
                VALUES (@Title, @Author, @PublishedDate, @Cost, @InStock, @BindingType)";//Cover,  @Cover Add back after figure out how to data type this
           
            using (SqlCeConnection conn = CreateConnection())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@PublishedDate", book.PublishedDate);
                    cmd.Parameters.AddWithValue("@Cost", book.Cost);
                    cmd.Parameters.AddWithValue("@InStock", book.InStock);
                    cmd.Parameters.AddWithValue("@BindingType", book.BindingType);
                    //cmd.Parameters.AddWithValue("@Cover", book.Cover);//Need to figure out where to save pictures and what data type to use

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }      
        }

        public static List<BookStoreModel> ReadAllBooks()
        {
            string cmdText =
                @"SELECT Id, Title, Author, PublishedDate, Cost, InStock, BindingType FROM Books";//Cover Add back after figure out how to data type this

            List<BookStoreModel> list = new List<BookStoreModel>();
            
            using (SqlCeConnection conn = CreateConnection())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    using (SqlCeDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookStoreModel book = new BookStoreModel();
                            
                            book.Id = (int)reader["Id"];
                            book.Title = (string)reader["Title"];
                            book.Author = (string)reader["Author"];
                            book.PublishedDate = (DateTime)reader["PublishedDate"];
                            book.Cost = (double)reader["Cost"];
                            book.InStock = (bool)reader["InStock"];
                            book.BindingType = (string)reader["BindingType"];
                            //book.Cover = (???)reader["Cover"];

                            list.Add(book);
                        }
                    }
                }
            }

            return list;
        }

        public static BookStoreModel ReadBook(int id)
        {
            string cmdText =
                @"SELECT Id, Title, Author, PublishedDate, Cost, InStock, BindingType FROM Books
                WHERE Id = @bookId";// Cover, Add back after figure out how to data type this
            
            BookStoreModel book = null;

            using (SqlCeConnection conn = CreateConnection())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@bookId", id);

                    using (SqlCeDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            book = new BookStoreModel();

                            book.Id = (int)reader["Id"];
                            book.Title = (string)reader["Title"];
                            book.Author = (string)reader["Author"];
                            book.PublishedDate = (DateTime)reader["PublishedDate"];
                            book.Cost = (double)reader["Cost"];
                            book.InStock = (bool)reader["InStock"];
                            book.BindingType = (string)reader["BindingType"];
                            //book.Cover = (???)reader["Cover"] ;
                        }
                    }
                }
            }

            return book;
        }

        public static void UpdateRecord(BookStoreModel book)
        {
            string cmdText =
                @"UPDATE Books 
                  SET
                    Title = @Title,
                    Author = @Author,
                    PublishedDate = @PublishedDate,
                    Cost = @Cost,
                    InStock = @InStock,
                    BindingType = @BindingType
                    
                WHERE Id = @id";//Cover = @Cover  Add back after figure out how to data type this

            using (SqlCeConnection conn = CreateConnection())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@id", book.Id);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@PublishedDate", book.PublishedDate);
                    cmd.Parameters.AddWithValue("@Cost", book.Cost);
                    cmd.Parameters.AddWithValue("@InStock", book.InStock);
                    cmd.Parameters.AddWithValue("@BindingType", book.BindingType);
                    //cmd.Parameters.AddWithValue("@Cover", book.Cover);//Need to figure out where to save pictures and what data type to use

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteBook(int id)
        {
            string cmdText = 
                @"DELETE FROM Books WHERE Id = @bookId";

            using (SqlCeConnection conn = CreateConnection())
            {
                using (SqlCeCommand cmd = new SqlCeCommand(cmdText, conn))
                {
                    cmd.Parameters.AddWithValue("@bookId", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }

    }//end class

}