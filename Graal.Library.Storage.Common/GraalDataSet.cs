using Graal.Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Storage.Common
{
    public class GraalDataSet : IGraalDataSet
    {
        private readonly DataSet set;

        private readonly DbDataAdapter adapter;

        public GraalDataSet(DbDataAdapter _adapter)
        {
            set = new DataSet();
            adapter = _adapter;
            Refresh();
        }

        public DataTable GetTable(string name = null)
        {
            if (name == null)
                return set.Tables[0];

            return set.Tables[name];
        }

        public void Refresh()
        {
            adapter.Fill(set);
        }

        public void Update()
        {
            adapter.Update(set);
        }
    }
}
