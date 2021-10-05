﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Exceptions
{
    public class InvalidElementOperationException : Exception
    {
        public InvalidElementOperationException(string message) : base(message)
        {
        }
    }
}
