// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public class DictionaryByType
{
    private readonly IDictionary<Type, object> dictionary = new Dictionary<Type, object>();

    public void Add<T>(T value)
        => dictionary.Add(typeof(T), value);
    
    public void Put<T>(T value)
        => dictionary[typeof(T)] = value;
    
    public T Get<T>()
        => (T)dictionary[typeof(T)];

    public bool TryGet<T>(out T value)
    {
        object temp;
        if (dictionary.TryGetValue(typeof(T), out temp))
        {
            value = (T)temp;
            return true;
        }
        value = default(T);
        return false;
    }
    
}