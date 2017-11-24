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

    /// <summary>
    /// 指定した文字列を消して返す
    /// </summary>
    /// <param name="characters">Characters.</param>
    /// <param name="target">消す対象</param>
    /// <returns></returns>
    public static string ReplaceDelete(this string characters, string target)
    {
        return characters.Replace(target, "");
    }
}

