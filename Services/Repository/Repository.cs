using Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repository
{
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        private readonly IMongoDbDataContextProvider _mongoDb;

        public Repository(
            ILogger<Repository> logger,
            IMongoDbDataContextProvider mongoDb
        )
        {
            _logger = logger;
            _mongoDb = mongoDb;
        }
    }
}
