using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoAPI.UseCases
{
    public class GetAdminInformationCommandResult : CommandResultBase
    {
        public AdminInformationContract? AdminInformationContract { get; set; }
    }
}