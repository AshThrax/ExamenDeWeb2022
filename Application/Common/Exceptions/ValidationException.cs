using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException
            ():base("one or more validation failures have occured")
            {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failuresGroupe => failuresGroupe.Key,failuresGroupe =>failuresGroupe.ToArray());
                
        }

        public IDictionary<string, string[]>Errors { get; }
    }
}
