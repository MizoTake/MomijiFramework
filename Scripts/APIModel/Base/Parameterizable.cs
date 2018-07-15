using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Momiji
{
    public interface IParameterizable { }
    public interface IPathParameterizable : IParameterizable
    {
        string QueryPath ();
    }

    public static class IPathParameterizableExtensions
    {
        public static string CreatePath (this IPathParameterizable param, params string[] path)
        {
            var query = "";
            path.ForEach (_ =>
            {
                query += _ + "&";
            });
            return "?" + query;
        }
    }
}