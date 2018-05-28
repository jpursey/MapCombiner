// Copyright 2018 John Pursey
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
