using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Database
{
    public class QueryDB
    {
        public string Request { get; }
        public bool IsProcedure { get; }
        public Dictionary<string, object> Parametres { get; }

        public QueryDB(string request, bool isProcedure = false)
        {
            this.Request = request;
            this.IsProcedure = isProcedure;
            this.Parametres = new Dictionary<string, object>();
        }

        public void AddParametre(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentOutOfRangeException();

            this.Parametres.Add(key, value);
        }
    }
}
