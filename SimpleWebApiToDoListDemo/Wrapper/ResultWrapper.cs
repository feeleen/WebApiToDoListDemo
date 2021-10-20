using System.Collections.Generic;
using System.Net;

namespace SimpleWebApiToDoListDemo.Wrapper
{

    public class ResultWrapper<T> : IResultWrapper<T>
    {
        public List<string> Messages { get; set; } = new();
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

        public virtual int ResultCode { get; set; } = (int) HttpStatusCode.OK;
    }

    public class FailResultWrapper<T> : ResultWrapper<T>
    {
        public override int ResultCode { get; set; } = (int) HttpStatusCode.BadRequest;
    }

    public class ResultWrapper
    {
        public static ResultWrapper<object> Success()
        {
            return new ResultWrapper<object>() { IsSuccess = true };
        }

        public static ResultWrapper<T> Success<T>(T data = null) where T: class 
        {
            return new ResultWrapper<T>() { IsSuccess = true, Data = data };
        }

        public static ResultWrapper<T> Success<T>(T data, List<string> messages)
        {
            return new ResultWrapper<T>() { IsSuccess = true, Data = data, Messages = messages };
        }
        
        public static ResultWrapper<object> Fail()
        {
            return new FailResultWrapper<object>() { IsSuccess = false };
        }

        public static ResultWrapper<T> Fail<T>(T data = null) where T: class
        {
            return new FailResultWrapper<T>() { IsSuccess = false, Data = data };
        }

        public static ResultWrapper<T> Fail<T>(T data, List<string> messages)
        {
            return new FailResultWrapper<T>() { IsSuccess = false, Data = data, Messages = messages };
        }

        public static ResultWrapper<T> Fail<T>(T data, List<string> messages, HttpStatusCode statusCode)
        {
            return new FailResultWrapper<T>() { IsSuccess = false, Data = data, Messages = messages, ResultCode = (int)statusCode };
        }

        public static ResultWrapper<object> NotFound()
        {
            return new FailResultWrapper<object>() { IsSuccess = false, ResultCode = (int)HttpStatusCode.NotFound };
        }

        public static ResultWrapper<T> NotFound<T>(T data = null) where T : class
        {
            return new FailResultWrapper<T>() { IsSuccess = false, ResultCode = (int)HttpStatusCode.NotFound };
        }
    }
}
