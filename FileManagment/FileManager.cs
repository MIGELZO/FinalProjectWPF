﻿using FinalProjectWPF.Enums;
using FinalProjectWPF.UserManagment;
using System.IO;
using System.Text.Json;

namespace FinalProjectWPF.FileManagment
{
    internal class FileManager
    {
        private readonly string usersFilePath = "users.json";
        private readonly string highScoreSnakeFilePath = "highscoreSnake.json";
        private readonly string highScoreCatchTheEggFilePath = "highscoreCatchTheEgg.json";
        private readonly string highScoreBattleShipFilePath = "highscoreBattleShip.json";

        public FileManager()
        {
            InitializeFromDataFile();
        }

        public List<User>? GetAllUsers()
        {
            try
            {
                string jsonContent = File.ReadAllText(usersFilePath);
                return JsonSerializer.Deserialize<List<User>>(jsonContent);
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }


        public User CreateNewUser(string name)
        {
            List<User>? users = GetAllUsers() ?? new List<User>();
            int id = users.Count > 0 ? users.Max(u => u.UserId) + 1 : 1;
            User player = new User(name, id);
            users.Add(player);
            File.WriteAllText(usersFilePath, JsonSerializer.Serialize(users));
            return player;
        }


        public void UpdateUser(int userId, string name)
        {
            List<User> users = GetAllUsers() ?? new List<User>();
            User? user = users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.FullName = name;
                user.LastLogin = DateTime.Now;
                File.WriteAllText(usersFilePath, JsonSerializer.Serialize(users));
            }
        }


        public User CheckLastLoginUser()
        {
            List<User> users = GetAllUsers() ?? new List<User>();
            return users.OrderByDescending(u => u.LastLogin).First();
        }

        public void UpdateLoginUser(int userId)
        {
            List<User> users = GetAllUsers() ?? new List<User>();
            users.Where(u => u.UserId == userId).First().LastLogin = DateTime.Now;
        }


        public List<double> GetUserHighScoresForGame(int userID, GameType game)
        {
            string filePath = GetHighScoreFilePath(game);
            if (!File.Exists(filePath))
            {
                return new List<double>();
            }
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                Dictionary<int, List<double>> usersHighscores = JsonSerializer.Deserialize<Dictionary<int, List<double>>>(jsonContent) ?? new Dictionary<int, List<double>>();
                return usersHighscores.TryGetValue(userID, out List<double>? scores) ? scores : new List<double>();

            }
            catch (Exception)
            {
                return new List<double>();
            }
        }

        public List<(GameType game, double score)> GetUserAllHighScores(int userID)
        {
            List<(GameType game, double score)> allHighScores = new List<(GameType game, double score)>();
            IEnumerable<GameType> gameTypes = Enum.GetValues(typeof(GameType)).Cast<GameType>();

            foreach (GameType game in gameTypes)
            {
                try
                {
                    List<double> scores = GetUserHighScoresForGame(userID, game);
                    allHighScores.Add((game, scores.Max()));
                }
                catch (Exception)
                {
                }
            }

            return allHighScores;
        }



        public List<(string name, double score)> GetAllPlayersHighScores(GameType game)
        {
            string filePath = GetHighScoreFilePath(game);
            if (!File.Exists(filePath))
            {
                return new List<(string name, double score)>();
            }
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                List<(string name, double score)> finalList = JsonSerializer.Deserialize<List<(string name, double score)>>(jsonContent) ?? new List<(string name, double score)>();
                return finalList.OrderByDescending(player => player.score).ThenBy(p => p.name).ToList();
            }
            catch (Exception)
            {
                return new List<(string name, double score)>();
            }
        }


        public void AddNewHighScore(int userID, GameType game, double score)
        {
            string filePath = GetHighScoreFilePath(game);
            Dictionary<int, List<double>> usersHighscores;
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
            {
                string jsonContent = File.ReadAllText(filePath);
                usersHighscores = JsonSerializer.Deserialize<Dictionary<int, List<double>>>(jsonContent) ?? new Dictionary<int, List<double>>();
            }
            else
            {
                usersHighscores = new Dictionary<int, List<double>>();
            }
            if (!usersHighscores.ContainsKey(userID))
            {
                usersHighscores[userID] = new List<double> { score };
            }
            if (!usersHighscores[userID].Contains(score))
            {
                usersHighscores[userID].Add(score);
            }
            string updatedJson = JsonSerializer.Serialize(usersHighscores, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);
        }

        private string GetHighScoreFilePath(GameType game)
        {
            return game switch
            {
                GameType.Snake => highScoreSnakeFilePath,
                GameType.CatchTheEgg => highScoreCatchTheEggFilePath,
                GameType.BattleShip => highScoreBattleShipFilePath,
                _ => throw new ArgumentOutOfRangeException(nameof(game), $"No file path defined for game {game}")
            };
        }
        public void InitializeFromDataFile()
        {

            if (!File.Exists(usersFilePath))
            {
                string InitialFilePath = @"..\..\..\FileManagment\DataInitialization.json";
                if (!File.Exists(InitialFilePath))
                {
                    throw new FileNotFoundException("DataInitialization.json file not found.");
                }

                try
                {
                    string jsonContent = File.ReadAllText(InitialFilePath);
                    JsonDocument? data = JsonSerializer.Deserialize<JsonDocument>(jsonContent);
                    if (data != null)
                    {
                        string users = data.RootElement.GetProperty("users").ToString();
                        string highscoreSnake = data.RootElement.GetProperty("highscoreSnake").ToString();
                        string highscoreCatchTheEgg = data.RootElement.GetProperty("highscoreCatchTheEgg").ToString();
                        string highscoreBattleShip = data.RootElement.GetProperty("highscoreBattleShip").ToString();

                        File.WriteAllText(usersFilePath, users);
                        File.WriteAllText(highScoreSnakeFilePath, highscoreSnake);
                        File.WriteAllText(highScoreCatchTheEggFilePath, highscoreCatchTheEgg);
                        File.WriteAllText(highScoreBattleShipFilePath, highscoreBattleShip);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("DataInitialization.json file was failed to deserialized.");
                }
            }
        }
    }
}