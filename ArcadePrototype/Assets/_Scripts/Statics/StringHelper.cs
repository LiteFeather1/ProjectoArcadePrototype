using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class StringHelper
{
    public static string SpaceBeforeCapitalLetters(string stringTo)
    {
        return string.Concat(stringTo.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }
}
