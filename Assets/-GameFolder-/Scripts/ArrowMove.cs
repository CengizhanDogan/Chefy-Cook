using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowMove : MonoBehaviour
{
    void Start()
    {
        var gfxY = transform.position.y + 1f;
        transform.DOLocalMoveY(gfxY, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
