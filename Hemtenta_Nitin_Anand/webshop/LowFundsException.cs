using System;
using System.Runtime.Serialization;

namespace HemtentaTdd2017.webshop
{
    //[Serializable]
    public class LowFundsException : Exception
    {
        public LowFundsException(string message)
        {
        }
    }
}