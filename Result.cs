using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCombiner
{
    class Result<Type>
    {
        public Result()
        {
            Ok = false;
            Value = default(Type);
            Status = "Uninitialized";
        }
        private Result(bool ok, Type value, string status)
        {
            Ok = ok;
            Value = value;
            Status = status;
        }
        public static Result<Type> Success(Type value)
        {
            return new Result<Type>(true, value, "Ok");
        }
        public static Result<Type> Error(string status)
        {
            return new Result<Type>(false, default(Type), status);
        }

        public bool Ok { get; set; }
        public Type Value { get; set; }
        public string Status { get; set; }
    }
}
