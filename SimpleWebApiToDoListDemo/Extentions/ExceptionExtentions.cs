using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebApiToDoListDemo.Extentions
{
    public static class ExceptionExtentions
    {
        public static List<Exception> GetInnerExceptions(this Exception e)
        {
            List<Exception> eList = new List<Exception>();

            if (e is AggregateException)
            {
                eList.AddRange((e as AggregateException).InnerExceptions);
            }
            else
            {
                eList.Add(e);
            }

            List<Exception> ieList = eList
                .Where(i => i.InnerException != null)
                .SelectMany(i => i.InnerException.GetInnerExceptions())
                .ToList();

            eList.AddRange(ieList);

            return eList;
        }
    }
}
