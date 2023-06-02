using APIMySqlСoursework.DBMySql;
using APIMySqlСoursework.Model;
using System.Data.Common;
using System.Diagnostics.Metrics;

namespace APIMySqlСoursework.Query
{
    public class CoachRaitingQuery
    {
        public DBConnection Db { get; }

        public CoachRaitingQuery(DBConnection db)
        {
            Db = db;
        }

        public async Task<List<CoachRaitingFullInfo>> FindOneByCoachIdFullInfoAsync(int idCoach)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT cr.id_CoachRaiting, cr.Teacher_id, cr.User_id, cr.Raiting, cr.Review, us.FullName, u.FullName FROM CoachRaiting cr JOIN Users us ON cr.Teacher_id = us.id_User JOIN Users u ON cr.User_id = u.id_User WHERE cr.Teacher_id = {idCoach}";
            var result = await ReadAllFullInfoAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }
        public async Task<double> TotalCoachRaiting(int idCoach)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT cr.Raiting FROM CoachRaiting cr  WHERE cr.Teacher_id = {idCoach}";
            var result = await SummRaitingsAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        public async Task<List<CoachRaitingFullInfo>> FindAllFullInfoAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = $"SELECT cr.id_CoachRaiting, cr.Teacher_id, cr.User_id, cr.Raiting, cr.Review, us.FullName, u.FullName FROM CoachRaiting cr JOIN Users us ON cr.Teacher_id = us.id_User JOIN Users u ON cr.User_id = u.id_User;";
            var result = await ReadAllFullInfoAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }

        private async Task<double> SummRaitingsAllAsync(DbDataReader reader)
        {
            double raitingsCount = 0;
            double totalRaiting = 0;
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    totalRaiting += reader.GetDouble(0);
                    raitingsCount++;
                }
            }
            return totalRaiting / raitingsCount;
        }

        private async Task<List<CoachRaitingFullInfo>> ReadAllFullInfoAsync(DbDataReader reader)
        {
            var coachRaitingFullInfos = new List<CoachRaitingFullInfo>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var coachRaiting = new CoachRaitingFullInfo(Db)
                    {
                        id_CoachRaiting = reader.GetInt32(0),
                        Teacher_id = reader.GetInt32(1),
                        User_id = reader.GetInt32(2),
                        Raiting = reader.GetDouble(3),
                        Review = reader.GetString(4),
                        Teacher_Name = reader.GetString(5),
                        User_Name = reader.GetString(6),
                    };
                    coachRaitingFullInfos.Add(coachRaiting);
                }
            }
            return coachRaitingFullInfos;
        }
    }
}
