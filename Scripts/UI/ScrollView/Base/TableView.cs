using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace Momiji
{
    public abstract class TableView<T> : MonoBehaviour where T : UnityEngine.Component
    {
        [SerializeField]
        private GameObject _cellPrefab;
        public GameObject CellPrefab { get { return _cellPrefab; } }
        public Pool<T> Pool { get; set; }
        public List<T> Cells { get; protected set; } = new List<T>();

        public abstract int CellCount();
        public abstract void TableViewCell(int index, T cell);
    }

    public static class TableViewExtension
    {
        public static void Init<T>(this TableView<T> tableView) where T : UnityEngine.Component
        {
            tableView.Cells.Clear();
            tableView.Pool = new Pool<T>(tableView.CellPrefab.GetComponent<T>(), tableView.transform);
            tableView.OnDestroyAsObservable().Subscribe(_ => tableView.Pool.Dispose());
        }

        public static void ReloadData<T>(this TableView<T> tableView) where T : UnityEngine.Component
        {
            tableView.Cells.ForEach((_) =>
            {
                tableView.Pool.Return(_);
            });
            tableView.Cells.Clear();
            for (int i = 0; i < tableView.CellCount(); i++)
            {
                var item = tableView.Pool.Rent();
                var cell = item.GetComponent<T>();
                tableView.TableViewCell(i, cell);
                tableView.Cells.Add(cell);
            }
        }
    }
}