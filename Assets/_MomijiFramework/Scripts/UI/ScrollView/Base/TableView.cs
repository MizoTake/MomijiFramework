using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TableView<T> : MonoBehaviour where T : UnityEngine.Component
{
    [SerializeField]
    protected GameObject _cellPrefab;
    protected Pool<T> _pool;
    protected List<T> _cells = new List<T>();

    public IReadOnlyList<T> Cell { get { return _cells; } }

    public abstract int CellCount();
    public abstract void ReloadData();
    public abstract void TableViewCell(int index, T cell);
}