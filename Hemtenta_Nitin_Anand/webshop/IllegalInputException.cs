using System;
using System.Runtime.Serialization;

namespace HemtentaTdd2017.webshop
{
    //[Serializable]
    public class IllegalInputException : Exception
    {

            public IllegalInputException(string message)
            {

            }
    }
}