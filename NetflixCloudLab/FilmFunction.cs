using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serverless.Entity;

namespace NetflixCloudLab
{
    public static class FilmFunction
    {
        private static string connectionString = "Server=tcp:netflix-sql-server.database.windows.net,1433;Initial Catalog=netflix-cloud-sql;Persist Security Info=False;User ID=netflix-admin;Password=pass_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;MultipleActiveResultSets=true;";
        [FunctionName("GetAllFilms")]
        public static async Task<IActionResult> GetAllFilms(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "film")] HttpRequest req, ILogger log)
        {
            var filmList = new List<Film>();
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var queryFilm = @"Select * from Film";
                SqlCommand commandFilm = new SqlCommand(queryFilm, connection);
                var readerFilm = await commandFilm.ExecuteReaderAsync();
                while (readerFilm.Read())
                {
                    var queryReview = @$"Select * from Review where FilmId = {(int)readerFilm["Id"]}";
                    var queryActor = @$"Select * from Actor, FilmActor 
                                        where Actor.Id = FilmActor.ActorId 
                                        and FilmActor.FilmId = {(int)readerFilm["Id"]}";
                    SqlCommand commandReview = new SqlCommand(queryReview, connection);
                    SqlCommand commandActor = new SqlCommand(queryActor, connection);
                    var readerReview = await commandReview.ExecuteReaderAsync();
                    var readerActor = await commandActor.ExecuteReaderAsync();
                    var reviewList = new List<Review>();
                    var actorList = new List<Actor>();
                    while (readerReview.Read())
                    {
                        var queryUser = @$"Select * from [User] where id = {(int)readerReview["UserId"]}";
                        SqlCommand commandUser = new SqlCommand(queryUser, connection);
                        var readerUser = await commandUser.ExecuteReaderAsync();
                        User user = new User();
                        while (readerUser.Read())
                        {
                            user = new User()
                            {
                                Id = (int)readerUser["Id"],
                                Username = readerUser["Username"].ToString(),
                                Name = readerUser["Name"].ToString(),
                                Surname = readerUser["Surname"].ToString(),
                                Age = (int)readerUser["Age"]
                            };
                        }
                        Review review = new Review()
                        {
                            Id = (int)readerReview["Id"],
                            Text = readerReview["Text"].ToString(),
                            User = user
                        };
                        reviewList.Add(review);
                    }
                    while (readerActor.Read())
                    {
                        var i = readerActor["Id"];
                        Actor actor = new Actor()
                        {
                            Id = (int)readerActor["Id"],
                            Name = readerActor["Name"].ToString(),
                            Surname = readerActor["Surname"].ToString(),
                            Rating = (Decimal)readerActor["Rating"]
                        };
                        actorList.Add(actor);
                    }
                    Film film = new Film()
                    {
                        Id = (int)readerFilm["Id"],
                        Title = readerFilm["Title"].ToString(),
                        Description = readerFilm["Description"].ToString(),
                        Rating = (Decimal)readerFilm["Rating"],
                        ReleaseDate = readerFilm["ReleaseDate"].ToString(),
                        Reviews = reviewList,
                        Actors = actorList
                    };
                    filmList.Add(film);
                }
                
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (filmList.Count > 0)
            {
                return new OkObjectResult(filmList);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [FunctionName("CreateFilm")]
        public static async Task<IActionResult> CreateFilm(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "film")] HttpRequest req, ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var input = JsonConvert.DeserializeObject<Film>(requestBody);
                SqlConnection connection = new SqlConnection(connectionString);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var query = @$"INSERT INTO [Film] (Id,Title,Description,Rating,ReleaseDate) 
                               VALUES('{input.Id}', '{input.Title}', '{input.Description}', '{input.Rating}', '{input.ReleaseDate}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }
    }
}
