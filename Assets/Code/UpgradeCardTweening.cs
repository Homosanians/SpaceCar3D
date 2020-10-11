using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpgradeCardTweening : MonoBehaviour
{
    [SerializeField]
    private Vector3 _targetLocation = Vector3.zero;

    [Range(0.1f, 10f), SerializeField]
    private float _moveDuration = 1f;

    [SerializeField]
    private Ease _moveEase = Ease.InOutBounce;

    [SerializeField]
    private DoTweenType _doTweenType = DoTweenType.MoveOneWay;

    [SerializeField]
    private enum DoTweenType
    {
        MoveOneWay,
        MoveTwoWay

    }

    public void Clicked()
    {
        if(_doTweenType == DoTweenType.MoveOneWay)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;

            transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
        }
        else if (_doTweenType == DoTweenType.MoveTwoWay)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            StartCoroutine(MoveBothWays()); 
        }
    }

    private IEnumerator MoveBothWays()
    {
        Vector3 originalLocation = transform.position;
        transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
        yield return new WaitForSeconds(_moveDuration);
        transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase);
    }
}
