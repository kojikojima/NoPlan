using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagType
{
    Unit,
    Bullet,
}

public static partial class TagTypeExtensions
{
    /*-------------------------------------------------------------------*/
    /* 変数                                                              */
    /*-------------------------------------------------------------------*/
    private static readonly Dictionary<TagType, string> tagDictionary = new Dictionary<TagType, string>()
    {
        { TagType.Unit, "Unit" },
        { TagType.Bullet, "Bullet"},
    };

    /*-------------------------------------------------------------------*/
    /* 外部メソッド                                                      */
    /*-------------------------------------------------------------------*/
    /// <summary>
    /// タグ名取得
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static string GetTagName(this TagType tag)
    {
        if (!tagDictionary.TryGetValue(tag, out var name))
        {
            Debug.Log("未定義のTagです");
        }
        return name;
    }
}