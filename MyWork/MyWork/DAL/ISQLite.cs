using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.DAL
{
    public interface ISQLite
    {
        string GetLocalFilePath(string filename);
    }
}
