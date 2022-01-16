﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExt<T> : UnityEvent<T> { }

public abstract class AbstractField<T> : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    protected T InitialValue;
    public UnityEvent<T> onValueChanged = new UnityEventExt<T>();
    [SerializeField]
    protected T runtimeValue;
    public virtual T Value
    {
        get
        {
            return runtimeValue;
        }
        set
        {
            if (HasValueChanged(value))
            {
                runtimeValue = value;
                ValueHasChanged(value);
            }
        }
    }

    protected void ValueHasChanged(T value)
    {
        onValueChanged.Invoke(value);
    }

    /// <summary>
    /// Reference types implementing IEquatable doesn't use boxing/unboxing in equality comparison
    /// https://stackoverflow.com/a/488301
    /// </summary>
    /// <param name="newValue"></param>
    /// <returns></returns>
    protected virtual bool HasValueChanged(T newValue)
    {
        return !EqualityComparer<T>.Default.Equals(runtimeValue, newValue);
    }

    public virtual void OnBeforeSerialize()
    { }

    public virtual void OnAfterDeserialize()
    {
        runtimeValue = InitialValue;
    }
}
