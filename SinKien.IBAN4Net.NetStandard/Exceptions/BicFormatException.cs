/*
 * IBAN4Net
 * Copyright 2018 Vaclav Beca [sinkien]
 *
 * Based on Artur Mkrtchyan's project IBAN4j (https://github.com/arturmkrtchyan/iban4j).
 *
 *
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace SinKien.IBAN4Net.Exceptions
{
    /// <summary>
    /// Exception which is thrown, when the application attempts to convert a string to Bic or to validate Bic's string representation, but the
    /// string does not have the appropriate format.
    /// Contains a field for indication which rule of validation is broken.
    /// </summary>
    public class BicFormatException : Exception
    {
        public object ExpectedObject { get; private set; }
        public object ActualObject { get; private set; }
        public BicFormatViolation FormatViolation { get; private set; }

        public BicFormatException() : base()
        { }

        public BicFormatException(string message) : base(message)
        { }

        public BicFormatException(string format, params object[] args) : base(string.Format(format, args))
        { }

        public BicFormatException(string message, Exception innerException) : base(message, innerException)
        { }

        public BicFormatException(string message, BicFormatViolation violation, object actual, object expected) : base(message)
        {
            ActualObject = actual;
            ExpectedObject = expected;
            FormatViolation = violation;
        }

        public BicFormatException(string message, BicFormatViolation violation) : base(message)
        {
            FormatViolation = violation;
        }

        public BicFormatException(string message, BicFormatViolation violation, Exception innerException) : base(message, innerException)
        {
            FormatViolation = violation;
        }

        public BicFormatException(string message, BicFormatViolation violation, object actual) : base(message)
        {
            ActualObject = actual;
            FormatViolation = violation;
        }
    }
}
