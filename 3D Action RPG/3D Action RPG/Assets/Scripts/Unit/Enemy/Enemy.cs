using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private Vector3 startPosition;

    private float offsetRange = 10f;
    private bool _isOutOfSafeArea = false;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Set X and Y to move
        if (target != null)
        {

        }
        else
            StayInSafeArea();
    }

    public void SetTarget(Transform t) => target = t;

    private void MoveTo()
    {
        // move to target if move out of spawn range back to original poistion
    }
    
    private void StayInSafeArea()
    {
        if (!_isOutOfSafeArea)
            return;
        // _isOutOfSafeArea = true; // after back to default path
    }

    private bool IsOutOfAreaOffset()
    {
        var sumPosition = transform.localPosition - startPosition;
        var currentPosition = Mathf.Abs(sumPosition.x) + Mathf.Abs(sumPosition.z);

        return currentPosition > offsetRange;
    }
}
