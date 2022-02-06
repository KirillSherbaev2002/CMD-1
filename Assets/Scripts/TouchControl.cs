using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    private Vector2 _swipeStart;
    private Vector2 _swipeEnd;
    private const float SwipeToScreenWidth = 16;
    private const float SwipeWValue = 150;
    private const float SwipeToScreenHeight = 16;
    private const float SwipeYValue = 300;

    public event Action SwipeRight;
    public event Action SwipeLeft;
    public event Action SwipeUp;
    public event Action SwipeDown;

    [SerializeField] private bool isTouchedStarts;

    private void Awake()
    {
        isTouchedStarts = false;
    }

    private void FixedUpdate()
    {
        if(Input.touchCount >= 1 || Input.GetMouseButton(0))
        {
            if (!isTouchedStarts)
            {
                _swipeStart = Input.mousePosition;
            }
            isTouchedStarts = true;
        }
        else
        {
            if (isTouchedStarts)
            {
                _swipeEnd = Input.mousePosition;
                CheckMovementNeeded();
            }
            isTouchedStarts = false;
        }
    }

    private void CheckMovementNeeded()
    {
        if (Vector2.Distance(_swipeStart, _swipeEnd) >= Screen.width / SwipeToScreenWidth)
        // Sets minimum value to swipe    
        {
            if (_swipeStart.x + SwipeWValue < _swipeEnd.x)
            {
                SwipeRight?.Invoke();
            }
            else if (_swipeStart.x > _swipeEnd.x + SwipeWValue)
            {
                SwipeLeft?.Invoke();
            }
        }

        if(Vector2.Distance(_swipeStart, _swipeEnd) >= Screen.width / SwipeToScreenHeight)
        {
            if (_swipeStart.y + SwipeYValue < _swipeEnd.y)
            {
                SwipeUp();
            }
            else if (_swipeStart.y > _swipeEnd.y + SwipeYValue)
            {
                SwipeDown();
            }
        }
    }
}
