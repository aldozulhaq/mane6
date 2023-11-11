using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject
{
    readonly HashSet<EventListener<T>> listeners = new();

    public void Invoke(T value)
    {
        foreach (var listener in listeners)
        {
            listener.Raise(value);
        }
    }

    public void Invoke() => Invoke(default);

    public void Register(EventListener<T> listener) => listeners.Add(listener);
    public void Deregister(EventListener<T> listener) => listeners.Remove(listener);
}
public readonly struct Empty { }

[CreateAssetMenu(menuName = "Event Channel/EmptyEventChannel")]
public class EventChannel : EventChannel<Empty> { }
