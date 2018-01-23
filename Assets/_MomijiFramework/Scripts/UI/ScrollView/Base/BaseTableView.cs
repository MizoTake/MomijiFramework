using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class BaseTableView<T> : TableView<T> where T : UnityEngine.Component
{

    public void Start()
    {
        _cells.Clear();
        _pool = new Pool<T>(_cellPrefab.GetComponent<T>(), transform);
        this.OnDestroyAsObservable().Subscribe(_ => _pool.Dispose());
    }

    public override void ReloadData()
    {
        _cells.ForEach((_) =>
        {
            _pool.Return(_);
        });
        _cells.Clear();
        for (int i = 0; i < CellCount(); i++)
        {
            var item = _pool.Rent();
            var cell = item.GetComponent<T>();
            TableViewCell(i, cell);
            _cells.Add(cell);
        }
    }

    public override int CellCount()
    {
        return 0;
    }

    public override void TableViewCell(int index, T cell) { }
}