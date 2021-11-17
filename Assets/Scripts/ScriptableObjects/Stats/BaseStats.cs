using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : ScriptableObject
{
    [Header("Combat Stats")]
    [SerializeField] protected float maxHealth;
    public float MaxHealth => maxHealth;

    [Header("Movement settings")]
    [SerializeField] protected float movementSpeed = 5f;
    public float MovementSpeed => movementSpeed;

}
