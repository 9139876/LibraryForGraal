using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

namespace Graal.Library.Storage.Common
{
    public interface IStorageSqlDriver
    {
        IDbConnection Connection { get; set; }

        event Action ConnectionStatusChange;

        bool ConnectionStatus { get; }

        bool SchemaExist();

        bool SchemaExistAndCorrect();

        void CreateGraalSchema();

        string DBName { get; }

        DbDataAdapter QuotesParserAdapter { get; }
    }
}
