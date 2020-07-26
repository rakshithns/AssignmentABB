using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentABB.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageForStatusCode();
        }
        private string GetDefaultMessageForStatusCode()
        {
            return StatusCode switch
            {
                400 => "Bad Request.",
                401 => "You are not Authorized.",
                404 => "Resource was not found.",
                500 => "Internal server error.",
                _ => null
            };
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
