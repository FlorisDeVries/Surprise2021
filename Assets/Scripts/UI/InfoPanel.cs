using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMPro.TMP_Text _textMeshPro;
    [SerializeField] private string _infoText;

    private void OnEnable()
    {
        _canvas.gameObject.SetActive(false);
        _textMeshPro.text = _infoText;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _canvas.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _canvas.gameObject.SetActive(false);
    }
}
