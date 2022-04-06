using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static List<T> ShuffleList<T>(List<T> _list)
    {
        for (int i = _list.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i);

            T temp = _list[i];
            _list[i] = _list[rand];
            _list[rand] = temp;
        }

        return _list;
    }
}
