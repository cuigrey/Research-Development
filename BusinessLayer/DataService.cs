using AOPLifetimeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DataService : IService007
    {
        public DataService(string connectionString, ILogger logger)
        {

        }

        ILogger Logger { get; set; }

        public void Initialize(string connectionString, IService007 dataService)
        {

        }
    }
}
