using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.DataBaseOperations
{
    public class MovieDB
    {
        public string path = @"Data Source=NEUDESI-SSN74TE\SQLEXPRESS;Initial Catalog=MovieTicketBooking;Integrated Security=SSPI";

        public List<MovieInfo> GetMovies()
        {
            List<MovieInfo> MovieList = new List<MovieInfo>();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_MovieList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    MovieInfo MovieObj = new MovieInfo();
                    MovieObj.MovieID = Convert.ToInt32(dr["movie_id"]);
                    MovieObj.MovieName = Convert.ToString(dr["movie_name"]);
                    MovieObj.Pic = Convert.ToString(dr["pic"]);

                    MovieList.Add(MovieObj);
                }
            }
            return MovieList;
        }


        public List<MovieInfo> shows(int id)
        {
            List<MovieInfo> Shows = new List<MovieInfo>();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_show", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    MovieInfo ShowObj = new MovieInfo();
                    ShowObj.MovieID = Convert.ToInt32(dr["movie_id"]);
                    ShowObj.MovieName = Convert.ToString(dr["movie_name"]);
                    ShowObj.TimeID = Convert.ToInt32(dr["show_id"]);
                    ShowObj.Time = Convert.ToString(dr["time"]);
                    ShowObj.AvailableSeats = Convert.ToInt32(dr["available_seats"]);
                    ShowObj.Date = Convert.ToDateTime(dr["date"]);
                    Shows.Add(ShowObj);
                }
            }
            return Shows;
        }


        public void Booking(int id, MovieTicket NewBook)
        {
            SqlConnection con = new SqlConnection(path);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Booking", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", NewBook.TimeID);
            cmd.Parameters.AddWithValue("@seat", NewBook.NumberSeats);
            cmd.Parameters.AddWithValue("@userID", NewBook.UserID);
            int RowCount = cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateSeat(int id,int seat)
        {
            SqlConnection con = new SqlConnection(path);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateSeat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@seat",seat);

            int RowCount = cmd.ExecuteNonQuery();
            con.Close();
        }
        public MovieInfo GetMovie(int id)
        {
            MovieInfo Movie = new MovieInfo();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_MovieDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Movie.MovieID = Convert.ToInt32(dr["movie_id"]);
                    Movie.MovieName = Convert.ToString(dr["movie_name"]);
                    Movie.Pic = Convert.ToString(dr["pic"]);
                    Movie.Director = Convert.ToString(dr["director"]);
                    Movie.Actor = Convert.ToString(dr["actor"]);
                    Movie.Release = Convert.ToDateTime( dr["relese"]);
                    Movie.Language = Convert.ToString(dr["language"]);
                }
            }
            return Movie;
        }
        public List<MovieInfo> MovieDate(string date)
        {
            List<MovieInfo> OnDateList = new List<MovieInfo>();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_SearchDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    MovieInfo OnDate = new MovieInfo();
                    OnDate.MovieID = Convert.ToInt32(dr["movie_id"]);
                    OnDate.MovieName = Convert.ToString(dr["movie_name"]);
                    OnDate.Pic = Convert.ToString(dr["pic"]);
                    OnDate.Date = Convert.ToDateTime(dr["date"]);
                    OnDateList.Add(OnDate);
                }
            }
            return OnDateList;
        }
        public List<MovieInfo> showsMovieDate(int MovieId, string date)
        {
            List<MovieInfo> MovieDateList = new List<MovieInfo>();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_SearchMovieDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", MovieId);//data not fetched
                cmd.Parameters.AddWithValue("@date", date);//data not fetched
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    MovieInfo OnDate = new MovieInfo();
                    OnDate.MovieID = Convert.ToInt32(dr["movie_id"]);
                    OnDate.MovieName = Convert.ToString(dr["movie_name"]);
                    OnDate.Pic = Convert.ToString(dr["pic"]);
                    OnDate.TimeID = Convert.ToInt32(dr["show_id"]);
                    OnDate.Time = Convert.ToString(dr["time"]);
                    OnDate.AvailableSeats = Convert.ToInt32(dr["available_seats"]);
                    OnDate.Date = Convert.ToDateTime(dr["date"]);
                    MovieDateList.Add(OnDate);
                }
            }
            return MovieDateList;
        }


        public List<MovieInfo> BookedList(int userid)
        {
            List<MovieInfo> Shows = new List<MovieInfo>();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_BookedList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    MovieInfo ShowObj = new MovieInfo();
                    ShowObj.MovieName = Convert.ToString(dr["movie_name"]);
                    ShowObj.Time = Convert.ToString(dr["time"]);
                    ShowObj.Seat = Convert.ToInt32(dr["no_seats"]);
                    ShowObj.Date = Convert.ToDateTime(dr["date"]);
                    ShowObj.BookID = Convert.ToInt32(dr["book_id"]);

                    Shows.Add(ShowObj);
                }
            }
            return Shows;
        }
        public void Cancel(int bookid,int seat)
        {
            SqlConnection con = new SqlConnection(path);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_Cancel", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@book_id", bookid);
            cmd.Parameters.AddWithValue("@seat", seat);
            int RowCount = cmd.ExecuteNonQuery();
            con.Close();
        }
        public MovieInfo BookedMovie(int BookID)
        {
            MovieInfo ShowObj = new MovieInfo();
            using (SqlConnection con = new SqlConnection(path))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"sp_BookedMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookid", BookID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    ShowObj.MovieName = Convert.ToString(dr["movie_name"]);
                    ShowObj.Time = Convert.ToString(dr["time"]);
                    ShowObj.Seat = Convert.ToInt32(dr["no_seats"]);
                    ShowObj.Date = Convert.ToDateTime(dr["date"]);
                    ShowObj.BookID = BookID;
                }
            }
            return ShowObj;
        }
    }
}
