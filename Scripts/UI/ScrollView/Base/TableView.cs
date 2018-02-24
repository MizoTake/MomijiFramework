using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using DG.Tweening;

namespace Momiji
{
    public abstract class TableView<T> : UIBehaviour where T : UnityEngine.Component
    {
        [SerializeField]
        private UIBehaviour _cellPrefab;
        public ReactiveProperty<int> SnapIndex = new ReactiveProperty<int>(0);
        public UIBehaviour CellPrefab { get { return _cellPrefab; } }
        public Pool<T> Pool { get; set; }
        public List<T> Cells { get; protected set; } = new List<T>();

        public abstract int CellCount();
        public abstract void TableViewCell(int index, T cell);
    }

    public static class TableViewExtension
    {
        public static void Init<T>(this TableView<T> tableView, ScrollViewType type = ScrollViewType.None) where T : UnityEngine.Component
        {
            switch (type)
            {
                case ScrollViewType.SnapX:
                    tableView.SnapX();
                    break;
                case ScrollViewType.SnapY:
                    tableView.SnapY();
                    break;
            }
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

        public static void SnapX<T>(this TableView<T> tableView) where T : UnityEngine.Component
        {
            tableView.SnapIndex.Value = tableView.CellCount() - 1;
            var scrollRect = tableView.transform.parent.parent.GetComponent<ScrollRect>();
            var cellRectTrans = tableView.CellPrefab.transform.GetComponent<RectTransform>();
            var tableViewWidth = cellRectTrans.sizeDelta.x * (tableView.CellCount() - 1);
            var cellPer = cellRectTrans.sizeDelta.x / tableViewWidth;
            scrollRect.OnEndDragAsObservable()
                .Where(_ => Mathf.Abs(_.delta.x) > 1.0f)
                .Select(_ => Mathf.Sign(_.delta.x))
                .Subscribe(distance =>
                {
                    tableView.SnapIndex.Value = Mathf.Clamp(tableView.SnapIndex.Value + (int)distance, 0, tableView.CellCount() - 1);
                })
                .AddTo(tableView);

            scrollRect.OnEndDragAsObservable()
                .Where(_ => Mathf.Abs(_.delta.x) <= 1.0f)
                .Subscribe(_ =>
                {
                    DOTween.To(() => scrollRect.horizontalScrollbar.value,
                        value => scrollRect.horizontalScrollbar.value = value,
                        cellPer * (float)(tableView.CellCount() - tableView.SnapIndex.Value - 1), 0.3f)
                        .Play();
                })
                .AddTo(tableView);

            tableView.SnapIndex
                .Subscribe(_ =>
                {
                    DOTween.To(() => scrollRect.horizontalScrollbar.value,
                        value => scrollRect.horizontalScrollbar.value = value,
                        cellPer * (float)(tableView.CellCount() - tableView.SnapIndex.Value - 1), 0.3f)
                        .Play();
                })
                .AddTo(tableView);
        }

        public static void SnapY<T>(this TableView<T> tableView) where T : UnityEngine.Component
        {
            tableView.SnapIndex.Value = 0;
            var scrollRect = tableView.transform.parent.parent.GetComponent<ScrollRect>();
            var cellRectTrans = tableView.CellPrefab.transform.GetComponent<RectTransform>();
            var tableViewHeight = cellRectTrans.sizeDelta.y * (tableView.CellCount() - 1);
            var cellPer = cellRectTrans.sizeDelta.y / tableViewHeight;
            scrollRect.OnEndDragAsObservable()
                .Where(_ => Mathf.Abs(_.delta.y) > 1.0f)
                .Select(_ => Mathf.Sign(_.delta.y))
                .Subscribe(distance =>
                {
                    tableView.SnapIndex.Value = Mathf.Clamp(tableView.SnapIndex.Value + (int)distance, 0, tableView.CellCount() - 1);
                })
                .AddTo(tableView);

            scrollRect.OnEndDragAsObservable()
                .Where(_ => Mathf.Abs(_.delta.y) <= 1.0f)
                .Subscribe(_ =>
                {
                    DOTween.To(() => scrollRect.verticalScrollbar.value,
                        value => scrollRect.verticalScrollbar.value = value,
                        cellPer * (float)(tableView.CellCount() - tableView.SnapIndex.Value - 1), 0.3f)
                        .Play();
                })
                .AddTo(tableView);

            tableView.SnapIndex
                .Subscribe(_ =>
                {
                    DOTween.To(() => scrollRect.verticalScrollbar.value,
                        value => scrollRect.verticalScrollbar.value = value,
                        cellPer * (float)(tableView.CellCount() - tableView.SnapIndex.Value - 1), 0.3f)
                        .Play();
                })
                .AddTo(tableView);
        }
    }
}

public enum ScrollViewType
{
    None,
    SnapX,
    SnapY
}