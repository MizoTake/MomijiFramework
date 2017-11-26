using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageBlock : MonoBehaviour
{
    [SerializeField]
    private Material[] _mat;

    private BlockType _type;
    private BoxCollider _col;
    private MeshRenderer _mesh;
    public bool Enable { get; private set; } = true;
    public bool Obstacle { get; private set; } = false;
    public int BreakCount { get; private set; } = 0;
    public bool Route { get; set; } = false;

    public void Setup()
    {
        _col = GetComponent<BoxCollider>();
        _mesh = GetComponent<MeshRenderer>();

        _type = (Random.Range(0, 100) % 2 == 0) ? BlockType.Obstacle : BlockType.Normal;
        _type = (Route) ? BlockType.Normal : _type;
        switch (_type)
        {
            case BlockType.Obstacle:
                Obstacle = true;
                _mesh.material = _mat[1];
                break;
            // case BlockType.CanBreak:
            //     BreakCount = Random.Range(0, 10);
            //     break;
            default:
                // Normal
                break;
        }
    }

    public void Disable()
    {
        _col.enabled = false;
        _mesh.enabled = false;
        Enable = false;
    }
}

enum BlockType
{
    Normal,
    Obstacle,
    CanBreak
}
