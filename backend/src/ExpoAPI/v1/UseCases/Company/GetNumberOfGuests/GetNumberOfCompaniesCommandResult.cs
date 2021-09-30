using System.Collections.Generic;

namespace ExpoAPI.UseCases.Company
{
    public class GetNumberOfGuestsCommandResult : CommandResultBase
    {
        public int? NumberOfGuests { get; set; }
    }
}