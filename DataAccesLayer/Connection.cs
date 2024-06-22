using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data.OleDb;
using System.Data;

namespace DataAccesLayer
{
    public class Connection  
    {
        public static OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\GolfDernegiVeriTabani.mdb");
    }
}
