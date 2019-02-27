using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadComic.Common.Enum
{
    public class ConstantsEnum
    {
        public enum CodeResponse
        {
            OK = 200,
            ServerError = 500,
            NotFound = 404,
            NotAccess = 403,
            NotValidate = 201
        }
    }
}