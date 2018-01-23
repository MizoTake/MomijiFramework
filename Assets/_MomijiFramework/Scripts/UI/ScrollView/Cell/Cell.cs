using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell<T> : MonoBehaviour where T : CellParameter
{
    public T Param;

    public abstract void Config(T param);
}

public abstract class CellParameter { }