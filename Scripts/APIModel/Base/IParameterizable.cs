using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Momiji
{
    public interface IParameterizable { }

    public interface IPathParameterizable : IParameterizable
    {
        string QueryPath ();
    }

    public interface IBodyParameterizable : IParameterizable
    {
        List<IMultipartFormSection> Body ();
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

    public static class IBodyParameterizableExtensions
    {
        public static List<IMultipartFormSection> CreateBody (this IBodyParameterizable param, params string[] bodyData)
        {
            var formData = new List<IMultipartFormSection> ();
            bodyData.ForEach (_ =>
            {
                formData.Add (new MultipartFormDataSection (_));
            });
            return formData;
        }
    }
}