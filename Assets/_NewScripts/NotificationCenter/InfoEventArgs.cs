using System;
using UnityEngine;
using System.Collections;

public class InfoEventArgs<T> : EventArgs
{
    public T info;

    public InfoEventArgs()
    {
        this.info = default(T);
    }

    public InfoEventArgs(T info)
    {
        this.info = info;
    }
}
