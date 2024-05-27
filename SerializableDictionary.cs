using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    private Dictionary<TKey, int> keyIndexMap = new Dictionary<TKey, int>();
    private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    public TValue this[TKey key]
    {
        get => dictionary[key];
        set
        {
            if (dictionary.ContainsKey(key))
            {
                values[keyIndexMap[key]] = value;
            }
            else
            {
                Add(key, value);
            }
        }
    }

    public TValue this[int index]
    {
        get => values[index];
        set => values[index] = value;
    }

    public Dictionary<TKey, TValue> ToDictionary() => dictionary;

    public void Add(TKey key, TValue value)
    {
        if (!dictionary.ContainsKey(key))
        {
            keys.Add(key);
            values.Add(value);
            keyIndexMap.Add(key, keys.Count - 1);
            dictionary.Add(key, value);
        }
        else
        {
            int index = keyIndexMap[key];
            values[index] = value;
            dictionary[key] = value;
        }
    }

    public void OnBeforeSerialize()
    {
        // No need to clear lists, as they will be rebuilt each time
    }

    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<TKey, TValue>();
        keyIndexMap.Clear();

        if (keys.Count != values.Count)
        {
            throw new Exception($"The number of keys ({keys.Count}) and values ({values.Count}) does not match");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (!dictionary.ContainsKey(keys[i]))
            {
                dictionary.Add(keys[i], values[i]);
                keyIndexMap.Add(keys[i], i);
            }
        }
    }
}
