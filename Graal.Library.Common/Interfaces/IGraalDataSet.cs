using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graal.Library.Common.Interfaces
{
    public interface IGraalDataSet
    {
        DataTable GetTable(string name = null);

        void Refresh();

        void Update();
    }
}
