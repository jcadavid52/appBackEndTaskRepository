using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCli_Models;

namespace TaskCli_Services
{
    public interface IManagementFiles
    {
        HandlerResponse initializeFile();
    }
}
