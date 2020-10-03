using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace Graal.Library.Storage.Common
{
    public interface IStorageSqlDriver
    {
        IDbConnection Connection { get; set; }

        event Action ConnectionStatusChange;

        bool ConnectionStatus { get; }

        bool SchemaExistAndCorrect();

        void CreateNeededTables();

        string DBName { get; }

        string SchemaName { get; }
    }
}
