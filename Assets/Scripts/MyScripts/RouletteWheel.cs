using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteWheel<T> : MonoBehaviour
{
    float luck;
    float count;
    public List<T> options = new List<T>();

    public T ExecuteRoulette()
    {
        count = 1f / (float)options.Count;
        luck = Random.value;
        for (int i = 0; i < options.Count; i++)
        {
            if (luck <= count)
            {
                return options[i];
            }
            count += count;
        }
        return default(T);
    }
}
