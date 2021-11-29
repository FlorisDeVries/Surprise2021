using System;
using UnityEngine;

namespace Pickups
{
    public class PickupRotate : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private void Update()
        {
            transform.Rotate(Vector3.up, _speed * Time.deltaTime);
        }
    }
}
