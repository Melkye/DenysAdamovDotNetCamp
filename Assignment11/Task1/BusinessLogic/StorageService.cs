using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class StorageService
    {
        private Logger _logger; // ILogger
        private Storage _storage; // IStorage
        public StorageService(Storage storage, Logger logger)
        {
            _storage = storage;
            _logger = logger; // = new Logger("Logs.txt");
        }
    }
}
