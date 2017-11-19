using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class StringExtensions
{
    /// <summary>
    /// 余分な記号やスペースを消して返す
    /// </summary>
    public static string SymbolReplace(this string name)
    {
        string[] invalidChar = new string[] { " ", "　", "!", "\"", "#", "$", "%", "&", "\'", "(", ")", "-", "=", "^", "~", "¥", "|", "[", "{", "@", "`", "]", "}", ":", "*", ";", "+", "/", "?", ".", ">", ",", "<" };
        invalidChar.ToList().ForEach(s => name = name.Replace(s, string.Empty));
        return name;
    }
}

