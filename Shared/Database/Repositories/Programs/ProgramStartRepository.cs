using static ClientServer.Shared.Models.ProgramWithTime;

namespace ClientServer.Shared.Database.Repositories.Programs
{
    public class ProgramStartRepository
    {
        private readonly DatabaseContext _context;

        public ProgramStartRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Insert(DbProgramWithExecutionTime dbProgramWithExecutionTime)
        {
            _context.ExecutionTimes.Add(dbProgramWithExecutionTime);
            _context.SaveChanges();
        }
    }
}
