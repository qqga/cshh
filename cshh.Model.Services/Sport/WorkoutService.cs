using cshh.Data.Services.Repo;
using cshh.Data.Sport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cshh.Model.Services.Sport
{

    public interface IWorkoutService
    {
        void AddSet(int user, int reps);

        IQueryable<Set> GetSets(int user);

        UserTotalResult GetUserTotalResult(int userId);
        void ClearSets(int userId);
    }

    public class WorkoutService : IWorkoutService
    {
        readonly ISportRepository _Repo;
        public WorkoutService(ISportRepository repo)
        {
            _Repo = repo;
        }

        public void AddSet(int user, int reps)
        {
            var set = new Set()
            {
                Dt = DateTime.UtcNow.AddHours(5),
                Reps = reps,
                UserId = user
            };
            _Repo.Add(set, true);
            _Repo.Save();
        }

        public IQueryable<Set> GetSets(int user) => _Repo.Sets.Where(_ => _.UserId == user);

        public UserTotalResult GetUserTotalResult(int userId)
        {
            IQueryable<Set> sets = GetSets(userId);
            var res = new UserTotalResult(sets);
            return res;
        }
        public void ClearSets(int userId)
        {           
            var ids = GetSets(userId).Select(_ => _.Id).ToList();
            foreach(var id in ids)
            {
                _Repo.Delete<Set>(id, false);
            }
            _Repo.Save();
        }

    }

    public class UserTotalResult
    {
        public TimeSpan Period { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public decimal BurnedFat { get; }

        public UserTotalResult(IEnumerable<Set> sets)
        {
            Set first = sets.OrderBy(_ => _.Dt).FirstOrDefault();
            Set last = sets.OrderBy(_ => _.Dt).LastOrDefault();

            if(first == null)
            {
                return;
            }
            Period = (last?.Dt ?? DateTime.UtcNow.AddDays(5)) - first.Dt;
            Reps = sets.Sum(_ => _.Reps);
            Sets = sets.Count();
            BurnedFat = CalcBurnedFat(Reps);
        }
        public static decimal CalcBurnedFat(int reps)
        {
            //todo drill params, user params
            var energyPerGramm = 3700m;
            var fuelKpd = 0.5m;
            var repitHeightMeter = 0.5m;
            var userWeight = 60m;
            var repsPerGram = energyPerGramm * fuelKpd / repitHeightMeter / userWeight;
            var burned = reps / repsPerGram;
            return burned;
        }
    }
}
