using System.Collections.Generic;

namespace SimpleWebApiToDoListDemo.Wrapper
{
    public interface IResultWrapper
    {
        List<string> Messages { get; set; }

        bool IsSuccess { get; set; }
    }

    public interface IResultWrapper<out T> : IResultWrapper
    {
        T Data { get; }
    }
}
