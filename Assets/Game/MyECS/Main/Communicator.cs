using System.Collections;
using System.Collections.Generic;
using System;

public class Communicator
{
    List<Action> callbacks = new List<Action>();

    public void AddListener(Action action)
    {
        if (!callbacks.Contains(action))
        {
            callbacks.Add(action);
        }
    }

    public void RemoveListener(Action action)
    {
        callbacks.Remove(action);
    }

    public void Invoke()
    {
        foreach (var item in callbacks)
        {
            item.Invoke();
        }
    }
}

public class Communicator<T>
{
    List<Action<T>> callbacks = new List<Action<T>>();

    public void AddListener(Action<T> action)
    {
        if (!callbacks.Contains(action))
        {
            callbacks.Add(action);
        }
    }

    public void RemoveListener(Action<T> action)
    {
        callbacks.Remove(action);
    }

    public void Invoke(T arg)
    {
        foreach (var item in callbacks)
        {
            item.Invoke(arg);
        }
    }
}

public class Communicator<T0, T1>
{
    List<Action<T0, T1>> callbacks = new List<Action<T0, T1>>();

    public void AddListener(Action<T0, T1> action)
    {
        if (!callbacks.Contains(action))
        {
            callbacks.Add(action);
        }
    }

    public void RemoveListener(Action<T0, T1> action)
    {
        callbacks.Remove(action);
    }

    public void Invoke(T0 arg0, T1 arg1)
    {
        foreach (var item in callbacks)
        {
            item.Invoke(arg0, arg1);
        }
    }
}

public class Communicator<T0, T1, T2>
{
    List<Action<T0, T1, T2>> callbacks = new List<Action<T0, T1, T2>>();

    public void AddListener(Action<T0, T1, T2> action)
    {
        if (!callbacks.Contains(action))
        {
            callbacks.Add(action);
        }
    }

    public void RemoveListener(Action<T0, T1, T2> action)
    {
        callbacks.Remove(action);
    }

    public void Invoke(T0 arg0, T1 arg1, T2 arg2)
    {
        foreach (var item in callbacks)
        {
            item.Invoke(arg0, arg1, arg2);
        }
    }
}

public class Communicator<T0, T1, T2, T3>
{
    List<Action<T0, T1, T2, T3>> callbacks = new List<Action<T0, T1, T2, T3>>();

    public void AddListener(Action<T0, T1, T2, T3> action)
    {
        if (!callbacks.Contains(action))
        {
            callbacks.Add(action);
        }
    }

    public void RemoveListener(Action<T0, T1, T2, T3> action)
    {
        callbacks.Remove(action);
    }

    public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        foreach (var item in callbacks)
        {
            item.Invoke(arg0, arg1, arg2, arg3);
        }
    }
}
