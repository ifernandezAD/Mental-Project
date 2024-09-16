using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimatedIcon : MonoBehaviour
{
    void Start()
    {
        transform.DORotate(Vector3.forward * 360f, 1f, RotateMode.FastBeyond360).
            SetEase(Ease.OutBounce).
            SetLoops(-1);
    }
}
