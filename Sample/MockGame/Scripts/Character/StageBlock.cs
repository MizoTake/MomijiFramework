using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Momiji
{

    public class StageBlock : MonoBehaviour
    {
        [SerializeField]
        private Material[] _mat;
        [SerializeField]
        private TextMesh _breakCount;

        private BlockType _type;
        private BoxCollider _col;
        private MeshRenderer _mesh;
        public bool Enable { get; private set; } = true;
        public bool Obstacle { get; private set; } = false;
        public int BreakCount { get; private set; } = 0;

        public void Setup(bool route)
        {
            _col = GetComponent<BoxCollider>();
            _mesh = GetComponent<MeshRenderer>();

            var type = (BlockType)Random.Range(0, Enum<BlockType>.Count);
            _type = (route && type == BlockType.Obstacle) ? BlockType.Normal : type;
            if (_type == BlockType.CanBreak)
            {
                _type = (Random.Range(0, 2) % 2 == 0) ? BlockType.CanBreak : BlockType.Normal;
            }
            switch (_type)
            {
                case BlockType.Obstacle:
                    Obstacle = true;
                    _mesh.material = _mat[1];
                    _breakCount.gameObject.SetActive(false);
                    break;
                case BlockType.CanBreak:
                    BreakCount = Random.Range(1, 10);
                    _mesh.material = _mat[2];
                    _breakCount.text = "" + BreakCount;
                    break;
                default:
                    // Normal
                    _breakCount.gameObject.SetActive(false);
                    break;
            }
        }

        public void Disable()
        {
            _col.enabled = false;
            _mesh.enabled = false;
            _breakCount.gameObject.SetActive(false);
            Enable = false;
        }

        public void Damage()
        {
            BreakCount -= 1;
            _breakCount.text = "" + BreakCount;
        }
    }

    enum BlockType
    {
        Normal,
        Obstacle,
        CanBreak
    }
}