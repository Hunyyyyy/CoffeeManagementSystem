using Application__CaféManagementSystem.Application_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application__CaféManagementSystem.Application_.Helpers
{
    public static class ResponseFactory
    {
        public static ResponseModel<T> Succes1s<T>(Action<T> data, string message) where T : class
        {
            return new ResponseModel<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static ResponseModel<T> Success<T>(T data, string message) where T : class
        {
            return new ResponseModel<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static ResponseModel<T> Fail<T>(string message, List<string>? errors = null) where T : class
        {
            return new ResponseModel<T>
            {
                Success = false,
                Message = message,
                Erros = errors ?? new List<string>()
            };
        }
        public static ResponseModel<T> NotFound<T>(string message) where T : class
        {
            return new ResponseModel<T>
            {
                Success = false,
                Message = message
            };
        }
        public static ResponseModel<T> Error<T>(Exception ex) where T : class
        {
            return new ResponseModel<T>
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
}
