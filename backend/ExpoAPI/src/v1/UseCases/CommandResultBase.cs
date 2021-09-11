using ExpoAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpoAPI.UseCases
{
    public class CommandResultBase
    {
        public ValidationState ValidateState { get; set; }
        public IEnumerable<MessageContract>? Messages { get; set; }
        public string? ReturnPath { get; set; }
        public bool Success => ValidateState == ValidationState.Valid;
    }
}
