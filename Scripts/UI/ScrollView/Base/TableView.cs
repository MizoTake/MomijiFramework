using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using DG.Tweening;

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
        public static void Init<T>(this TableView<T> tableView, bool snap = false) where T : UnityEngine.Component
        {
            if (snap) tableView.Snap();
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

        public static void Snap<T>(this TableView<T> tableView) where T : UnityEngine.Component
        {
            var cellIndex = 0;
            var scrollRect = tableView.transform.parent.parent.GetComponent<ScrollRect>();
            var cellRectTrans = tableView.CellPrefab.transform.GetComponent<RectTransform>();
            var tableViewHeight = cellRectTrans.sizeDelta.y * (tableView.CellCount() - 1);
            var cellPer = cellRectTrans.sizeDelta.y / tableViewHeight;
            scrollRect.OnEndDragAsObservable()
                .Where(_ => Mathf.Abs(_.delta.y) > 1.0f)
                .Select(_ => Mathf.Sign(_.delta.y))
                .Subscribe(_ =>
                {
                    cellIndex += (int)_;
                    if (cellIndex < 0) cellIndex = 0;
                    if (cellIndex > tableView.CellCount()) cellIndex = tableView.CellCount() - 1;

                    DOTween.To(() => scrollRect.verticalScrollbar.value,
                        value => scrollRect.verticalScrollbar.value = value,
                        cellPer * (float)(tableView.CellCount() - cellIndex - 1), 0.3f)
                        .Play();
                    Debug.Log(cellIndex);
                })
                .AddTo(tableView);
        }
    }
}