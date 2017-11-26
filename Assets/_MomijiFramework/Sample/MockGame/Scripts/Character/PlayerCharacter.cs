using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class PlayerCharacter : Singleton<PlayerCharacter>
{

    private int _positionX;
    private int _positionY;
    private Rigidbody _rigid;

    public int BlockPosX => Instance._positionX;
    public int BlockPosY => Instance._positionY;

    // Use this for initialization
    void Start()
    {
        _positionX = CreateStage.Width / 2;
        _positionY = CreateStage.Height;
        _rigid = GetComponent<Rigidbody>();
    }

    public static void RemoveRigidBody()
    {
        Destroy(Instance.gameObject.GetComponent<Rigidbody>());
    }

    public void Click()
    {
        Next(_positionX, _positionY - 1);
    }

    public void LeftFlick()
    {
        Next(_positionX - 1, _positionY);
    }

    public void RightFlick()
    {
        Next(_positionX + 1, _positionY);
    }

    public void Next(int nextX, int nextY)
    {
        if (_rigid) return;
        if (nextX < 0 || nextY < 0 || nextX > CreateStage.Width - 1 || nextY > CreateStage.Height - 1)
        {
            return;
        }
        var next = CreateStage.StageBlocks[nextX, nextY];
        if (next.Obstacle) return;
        if (next.Enable) next.Disable();
        transform.DOMove(next.transform.position, 0.3f).Play();
        _positionX = nextX;
        _positionY = nextY;
    }

}
