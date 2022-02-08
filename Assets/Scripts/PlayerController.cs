using System;
using System.Collections;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementHorizontalValue;
    [SerializeField] private float stepOfMovementHorizontal;
    [SerializeField] private float speedOfUpdatesForMovement;

    private float _defaultX;
    private bool _isCorutineComplited = true;
    
    private Transform _playerTransform;
    private TouchControl _playerControl;
    private PlayerInteraction _playerInteraction;

    public Animator JumpMoveUp;
    public Animator JumpAnimation;
    public AudioSource movingHorizontal;

    [SerializeField] BoxCollider _bc;

    #region Check Varibles on correctness

    private void OnValidate()
    {
        /*if (movementHorizontalValue == 0) movementHorizontalValue = 1;
       //if (speedOfUpdatesForMovement == 0) speedOfUpdatesForMovement = 0.01f;
        //if (stepOfMovementHorizontal == 0 || stepOfMovementHorizontal > movementHorizontalValue) 
            stepOfMovementHorizontal = movementHorizontalValue;*/
    }

    #endregion

    private void Awake()
    {
        _playerControl = FindObjectOfType<TouchControl>();

        _playerControl.SwipeLeft += TrySwipeLeft;
        _playerControl.SwipeRight += TrySwipeRight;

        _playerControl.SwipeUp += TrySwipeUp;
        _playerControl.SwipeUp += JumpSide;
        _playerControl.SwipeDown += Roll;

        _playerTransform = transform;
        _defaultX = _playerTransform.localPosition.x;
    }

    private void TrySwipeLeft()
    {
        if (_playerTransform.localPosition.x < _defaultX || !_isCorutineComplited)return;
        Vector3 localPosition = transform.localPosition;
        JumpMoveUp.SetTrigger("LeftMove");
        movingHorizontal.Play();
        StartCoroutine(MoveHorizontal(false, localPosition.x - movementHorizontalValue));
    }
    
    private void TrySwipeRight()
    {
        if (_playerTransform.localPosition.x > _defaultX || !_isCorutineComplited) return;
        Vector3 localPosition = transform.localPosition;
        JumpMoveUp.SetTrigger("RightMove");
        movingHorizontal.Play();
        StartCoroutine(MoveHorizontal(true, localPosition.x + movementHorizontalValue));
    }

    private void TrySwipeUp()
    {
        //JumpMoveUp.SetTrigger("Jump");
        JumpAnimation.SetTrigger("Jump");
        StartCoroutine(DisableCollider());
    }

    IEnumerator DisableCollider()
    {
        GetComponentInChildren<AudioSource>().volume = 0;
        _bc.enabled = false;
        yield return new WaitForSeconds(0.8f);
        GetComponentInChildren<AudioSource>().volume = 0.06f;
        _bc.enabled = true;
        yield break;
    }

    private void JumpSide()
    {
        JumpAnimation.SetTrigger("JumpSide");
    }

    private void Roll()
    {
        JumpAnimation.SetTrigger("Roll");
        movingHorizontal.Play();
        StartCoroutine(DisableCollider());
    }

    private IEnumerator MoveHorizontal(bool isRight, float neededX)
    {
        _isCorutineComplited = false;
        if (isRight)
        {
            while (_playerTransform.localPosition.x < neededX)
            {
                _playerTransform.localPosition += new Vector3(stepOfMovementHorizontal,0,0);
                yield return new WaitForSeconds(speedOfUpdatesForMovement);
            } 
        }
        else
        {
            while (_playerTransform.localPosition.x > neededX)
            {
                _playerTransform.localPosition -= new Vector3(stepOfMovementHorizontal,0,0);
                yield return new WaitForSeconds(speedOfUpdatesForMovement);
            }
        }

        var localPosition = _playerTransform.localPosition;
        localPosition = new Vector3(neededX, localPosition.y, 
            localPosition.z);
        _playerTransform.localPosition = localPosition;
        
        _isCorutineComplited = true;
    }
}
