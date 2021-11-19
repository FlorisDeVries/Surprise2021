using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacter : MonoBehaviour
{
    [SerializeField] private Transform _boundary1;
    [SerializeField] private Transform _boundary2;
    private Transform _currentTarget;

    [SerializeField] private float _speed = 2;

    [SerializeField] private SkeletonAnimation _characterCell = null;

    private void OnEnable()
    {
        _characterCell.AnimationState.SetAnimation(0, "walk", true);
        _currentTarget = _boundary1;
    }

    private void Update()
    {
        // TODO: Flip to orient right

        // TODO: Make random wander

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentTarget.position) < .1f)
        {
            _currentTarget = _currentTarget == _boundary1 ? _boundary2 : _boundary1;
        }
    }
}
