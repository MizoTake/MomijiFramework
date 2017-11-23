using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class TableView : MonoBehaviour
{

    private const int PAGING_VALUE = 30;

    [SerializeField]
    private GameObject _item;
    [SerializeField]
    private Text _headerText;
    [SerializeField]
    private Text _rankText;
    [SerializeField]
    private IndicatorView _indicator;
    private RankingItemPool _pool;
    private List<RankingItem> _listItem = new List<RankingItem>();
    private int _myRank = 0;
    private int _allRank = 0;
    private int _nowCells = 0;
    private int beforeScore = -1;
    private int beforeRank = 0;

    // Use this for initialization
    void Start()
    {
        _pool = new RankingItemPool(_item.GetComponent<RankingItem>(), transform);
        this.OnDestroyAsObservable().Subscribe(_ => _pool.Dispose());

        _indicator.Show.Restart();
        if (PlayerPrefs.HasKey(PlayerInfo.UUID_NAME))
        {
            Ranking.GetLastRow(PlayerInfo.Uuid, (_) =>
            {
                UpdateRankText(_);
                Ranking.Get(_nowCells, (_nowCells + PAGING_VALUE > _allRank) ? _allRank % PAGING_VALUE - 1 : PAGING_VALUE, (result) => ReloadData(result), (error) => Error());
            });
        }
        else
        {
            Ranking.GetUuid((_) =>
            {
                PlayerInfo.Uuid = _.uuid;
                Ranking.GetLastRow(_.uuid, (rank) =>
                {
                    UpdateRankText(rank);
                    Ranking.Get(_nowCells, (_nowCells + PAGING_VALUE > _allRank) ? _allRank % PAGING_VALUE - 1 : PAGING_VALUE, (result) => ReloadData(result), (error) => Error());
                });
            }, (_) => Error());
        }
    }

    private void ReloadData(Response res)
    {
        _indicator.Dismiss.Restart();
        beforeScore = -1;
        beforeRank = 0;
        var myUuid = PlayerInfo.Uuid;
        _listItem.ForEach((_) =>
        {
            _pool.Return(_);
        });
        _listItem.Clear();
        res.result.ToList().ForEach((data, i) =>
        {
            var item = _pool.Rent();
            var rank = item.GetComponent<RankingItem>();
            if (beforeScore == int.Parse(data.score))
            {
                i = beforeRank;
            }
            else
            {
                beforeRank += 1;
            }
            beforeScore = int.Parse(data.score);
            rank.SetItem(beforeRank.ToString(), data.name, data.score, myUuid == data.id);
            if (myUuid == data.id) _myRank = beforeRank;
            _listItem.Add(rank);
        });
        _nowCells = PAGING_VALUE;
    }

    public void PagingGet()
    {
        if (_nowCells >= _allRank - 1) return;
        _indicator.Show.Restart();
        Ranking.Get(_nowCells, (_nowCells + PAGING_VALUE > _allRank) ? _allRank % PAGING_VALUE - 1 : PAGING_VALUE, (_) =>
        {
            _indicator.Dismiss.Restart();
            Paging(_);
        }, (_) => Error());
    }

    public void Paging(Response res)
    {
        var myUuid = PlayerInfo.Uuid;
        res.result.ToList().ForEach((data, i) =>
        {
            var item = _pool.Rent();
            var rank = item.GetComponent<RankingItem>();
            if (beforeScore == int.Parse(data.score))
            {
                i = beforeRank;
            }
            else
            {
                beforeRank += 1;
            }
            beforeScore = int.Parse(data.score);
            rank.SetItem(beforeRank.ToString(), data.name, data.score, myUuid == data.id);
            if (myUuid == data.id) _myRank = beforeRank;
            _listItem.Add(rank);
        });
        _nowCells += (_nowCells + PAGING_VALUE > _allRank) ? _allRank % PAGING_VALUE - 1 : PAGING_VALUE;
    }

    private void UpdateRankText(LastRowResponse res)
    {
        _myRank = int.Parse(res.myRank);
        _allRank = int.Parse(res.lastRow);
        _rankText.text = _myRank + " / " + (_allRank - 1);
    }

    private void Error()
    {
        _headerText.text = "Network Error";
        _headerText.color = Color.red;
        _indicator.Dismiss.Restart();
    }
}
