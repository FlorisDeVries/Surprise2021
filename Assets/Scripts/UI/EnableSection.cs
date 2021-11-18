using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSection : MonoBehaviour
{
    [SerializeField] private bool _defaultEnabled = false;
    [SerializeField] CanvasGroup _group;

    private void OnEnable()
    {
        if (!_defaultEnabled)
        {
            _group.alpha = 0;
        }
    }

    public void Enable()
    {
        // TODO: Animate enable
        _group.alpha = 1;
    }
}
