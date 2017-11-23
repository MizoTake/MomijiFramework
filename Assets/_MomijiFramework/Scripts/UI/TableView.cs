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
    private Text _header;
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

    public int MyRank { set { _myRank = value; } }
    public IndicatorView Indicator => _indicator;
    public Text RankText => _rankText;
    public int AllRank => _allRank;
    public int NowCells => _nowCells;
    public int MAX_Cell => PAGING_VALUE;

    // Use this for initialization
    void Start()
    {
        _pool = new RankingItemPool(_item.GetComponent<RankingItem>(), transform);
        this.OnDestroyAsObservable().Subscribe(_ => _pool.Dispose());

        if (PlayerPrefs.HasKey(UserInfo.UUID_NAME))
        {
            _indicator.Show.Restart();
            Ranking.GetLastRow(UserInfo.Uuid, (_) =>
            {
                _myRank = int.Parse(_.myRank);
                _allRank = int.Parse(_.lastRow);
                _rankText.text = _myRank + " / " + (_allRank - 1);
                Ranking.Get(_nowCells, (_nowCells + PAGING_VALUE > _allRank) ? _allRank % PAGING_VALUE - 1 : PAGING_VALUE, (result) =>
                {
                    ReloadData(result);
                    _indicator.Dismiss.Restart();
                }, (error) =>
                {
                    _header.text = "Network Error";
                    _header.color = Color.red;
                    _indicator.Dismiss.Restart();
                });
            });
        }
        else
        {
            _indicator.Show.Restart();
            Ranking.GetUuid((_) =>
            {
                UserInfo.Uuid = _.uuid;
                Ranking.GetLastRow(_.uuid, (rank) =>
                {
                    _myRank = int.Parse(rank.myRank);
                    _allRank = int.Parse(rank.lastRow);
                    _rankText.text = _myRank + " / " + (_allRank - 1);
                    Ranking.Get(_nowCells, (_nowCells + PAGING_VALUE > _allRank) ? _allRank % PAGING_VALUE - 1 : PAGING_VALUE, (result) =>
                    {
                        ReloadData(result);
                        _indicator.Dismiss.Restart();
                    }, (error) =>
                    {
                        _header.text = "Network Error";
                        _header.color = Color.red;
                        _indicator.Dismiss.Restart();
                    });
                });
            }, (_) =>
            {
                _header.text = "Network Error";
                _header.color = Color.red;
                _indicator.Dismiss.Restart();
            });
        }
    }

    public void ReloadData(Response res)
    {
        beforeScore = -1;
        beforeRank = 0;
        var myUuid = UserInfo.Uuid;
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
        }, (_) =>
        {
            _header.text = "Network Error";
            _header.color = Color.red;
        });
    }

    private void Paging(Response res)
    {
        var myUuid = UserInfo.Uuid;
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
}
