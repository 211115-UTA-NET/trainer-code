﻿using System.Data.SqlClient;
using RockPaperScissorsApp.Logic;

namespace RockPaperScissorsApp.DataInfrastructure
{
    public class SqlRepository : IRepository
    {
        private readonly string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IEnumerable<Round> GetAllRoundsOfPlayer(string name)
        {
            List<Round> result = new();

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand cmd = new(
                //       0          1        2        3         4
                @"SELECT Timestamp, P1.Name, P2.Name, P1M.Name, P2M.Name
                  FROM Rps.Round
                      INNER JOIN Rps.Player AS P1 ON Player1 = P1.Id
                      LEFT JOIN Rps.Player AS P2 ON Player2 = P2.Id
                      INNER JOIN Rps.Move AS P1M ON Player1Move = P1M.Id
                      INNER JOIN Rps.Move AS P2M ON Player2Move = P2M.Id
                  WHERE P1.Name = @playername",
                connection);

            cmd.Parameters.AddWithValue("@playername", name);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTimeOffset timestamp = reader.GetDateTimeOffset(0);
                var move1 = (Move)Enum.Parse(typeof(Move), reader.GetString(3));
                var move2 = (Move)Enum.Parse(typeof(Move), reader.GetString(4));
                result.Add(new(timestamp, move1, move2));
            }

            connection.Close();

            return result;
        }

        public void AddNewRound(string? player1, string? player2, Round round)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            // handle it if the player doesn't exist yet
            // (would maybe make more sense for the player creation to be done
            // in advance of calling this method, and this method gets to assume
            // it's already been done)
            int? player1Id = player1 is null ? null : EnsurePlayerExistsAndGetId(connection, player1);
            int? player2Id = player2 is null ? null : EnsurePlayerExistsAndGetId(connection, player2);

            // assume the move exist already in the DB
            string cmdText = @"INSERT INTO Rps.Round (Timestamp, Player1, Player2, Player1Move, Player2Move)
                               VALUES (@timestamp, @p1, @p2,
                                   (SELECT Id FROM Rps.Move WHERE Name = @p1move),
                                   (SELECT Id FROM Rps.Move WHERE Name = @p2move));";
            using SqlCommand cmd = new(cmdText, connection);

            // ado.net requires you to use DBNull instead of null when you mean a SQL NULL value
            cmd.Parameters.AddWithValue("@timestamp", round.Date);
            cmd.Parameters.AddWithValue("@p1", player1Id ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@p2", player2Id ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@p1move", round.Player1.ToString());
            cmd.Parameters.AddWithValue("@p2move", round.Player2.ToString());

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">An open connection</param>
        /// <param name="name">The player's name</param>
        /// <returns>ID of player</returns>
        public int EnsurePlayerExistsAndGetId(SqlConnection connection, string name)
        {
            string cmdText = @"SELECT Id
                               FROM Rps.Player
                               WHERE Name = @playername";
            using (SqlCommand cmd = new(cmdText, connection))
            {
                cmd.Parameters.AddWithValue("@playername", name);

                using SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            // instead of getting the ID in here, i could just rely on a subquery
            // inside the INSERT that inserts the round
            string cmd2Text = @"INSERT INTO Rps.Player (Name) VALUES (@playername);
                                SELECT Id FROM Rps.Player WHERE Name = @playername;";
            using SqlCommand cmd2 = new(cmd2Text, connection);
            cmd2.Parameters.AddWithValue("@playername", name);

            using SqlDataReader reader2 = cmd2.ExecuteReader();

            reader2.Read();
            return reader2.GetInt32(0);
        }
    }
}
