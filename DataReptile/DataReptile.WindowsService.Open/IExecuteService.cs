using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReptile.WindowsService.Open
{
    public interface IExecuteService
    {
        Task Execute(DateTime dateTime);

        int Interval { get; }
    }
}
