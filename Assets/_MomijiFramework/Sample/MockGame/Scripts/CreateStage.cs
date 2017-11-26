using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CreateStage : Singleton<CreateStage>
{

    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private StageBlock _blockPrefab;

    private StageBlock[,] _stageBlock;

    public static Subject<Unit> CreateStageStart = new Subject<Unit>();
    public static ReactiveProperty<bool> FinishCreateStage = new ReactiveProperty<bool>(false);
    public static int Width => Instance._width;
    public static int Height => Instance._height;
    public static StageBlock[,] StageBlocks => Instance._stageBlock;
    public static List<Vector2> Path = new List<Vector2>();

    // Use this for initialization
    void Start()
    {
        _stageBlock = new StageBlock[_width, _height];
        RoutePath();

        CreateStageStart
            .Take(1)
            .Subscribe(_ =>
            {
                StartCoroutine(InstanceBlocks());
            })
            .AddTo(this);

        // TODO: Debug処理
        // CreateStageStart.OnNext(Unit.Default);
    }

    private void RoutePath()
    {
        Path.Add(new Vector2(_width / 2, _height - 1));
        int cnt = 0;
        while (true)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    if (Path[cnt].x - 1 < 0) continue;
                    Path.Add(new Vector2(Path[cnt].x - 1, Path[cnt].y));
                    break;
                case 1:
                    Path.Add(new Vector2(Path[cnt].x, Path[cnt].y - 1));
                    break;
                case 2:
                    if (Path[cnt].x - 1 > _width) continue;
                    Path.Add(new Vector2(Path[cnt].x + 1, Path[cnt].y));
                    break;
            }
            if (Path[cnt].y == 0) break;
            cnt += 1;
        }
    }

    public static void RemoveRigidBody()
    {
        for (var y = 0; y < Instance._height; y++)
        {
            for (var x = 0; x < Instance._width; x++)
            {
                Destroy(Instance._stageBlock[x, y].GetComponent<Rigidbody>());
            }
        }
    }

    IEnumerator InstanceBlocks()
    {
        var cnt = 0;
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var check = (Path.IndexOf(new Vector2(x, y)) == -1) ? false : true;
                var block = _stageBlock[x, y] = Instantiate(_blockPrefab, new Vector3(x, (y + 1) + 40.0f, 0), Quaternion.identity).GetComponent<StageBlock>();
                block.transform.SetParent(transform);
                block.Route = check;
                block.Setup();
                cnt += 1;
            }
            if (y % 2 == 0) yield return new WaitForSeconds(0.1f);
        }
        FinishCreateStage.Value = true;
    }
}
