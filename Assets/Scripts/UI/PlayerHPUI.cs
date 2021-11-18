using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] private PlayerStats _stats;

    [SerializeField] private List<GameObject> _heartSprites;

    private void OnEnable()
    {
        _stats.PlayerHitEvent += OnPlayerHit;

        _heartSprites = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _heartSprites.Add(transform.GetChild(i).gameObject);
        }
        OnPlayerHit(0);
    }

    private void OnDisable()
    {
        _stats.PlayerHitEvent -= OnPlayerHit;
    }

    private void OnPlayerHit(float _)
    {
        for (int i = 0; i < _heartSprites.Count; i++)
        {
            _heartSprites[i].SetActive(i < _stats.CurrentHealth);
        }
    }
}