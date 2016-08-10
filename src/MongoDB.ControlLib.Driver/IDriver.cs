using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.ControlLib.Driver
{
    public interface ICollection<TDocument>
    {
        string ConnectionString { get; set; }

        string DataBase { get; set; }

        ICollection<TDocument> Collections { get; }






    }
}
