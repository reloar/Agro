using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace Domain.Models
{
    public abstract class Model : IValidatableObject
    {
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Throws an Error if the Object is not Valid
        /// </summary>
        public virtual void Validate()
        {
            Validator.ValidateObject(this, new ValidationContext(this, serviceProvider: null, items: null));
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new ValidationResult[0];
    }
    public static class CommonExtension
    {
        public static ApiResponse<object> ToResponse(this object data, string message = "", HttpStatusCode status = HttpStatusCode.OK)
        {
            if (data is string)
            {
                message = data.ToString();
            }
            return new ApiResponse<object>() { Message = message, StatusCode = status, Data = data };
        }

    }
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> errors { get; set; }
    }
}
