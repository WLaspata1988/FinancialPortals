using FinancialPortals.Models;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace FinancialPortals.Helpers
{
    public class Logger
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static void AddException(Exception ex)
        {
            var exception = new Models.Exception
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                InnerException = JsonConvert.SerializeObject(ex.InnerException, Formatting.Indented),
                HelpLink = ex.HelpLink,
                Data = JsonConvert.SerializeObject(ex.Data, Formatting.Indented),
                TargetSite = ex.TargetSite.Name
            };

            db.Exceptions.Add(exception);
            db.SaveChanges();
        }

        public static void LogInformation(string info)
        {
            var exception = new Models.Exception
            {
                Message = info
            };

            db.Exceptions.Add(exception);
            db.SaveChanges();
        }
    }
}