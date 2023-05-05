using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarEventoZona : MonoBehaviour
{

    [SerializeField] GameObject _canvas;
    [SerializeField] float _distancia = 0.2f;
    [SerializeField] GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canvas != null)
            {
                print("entro");
                _canvas.SetActive(true);
                _canvas.transform.localPosition = new Vector3(_player.transform.localPosition.x + _distancia, _player.transform.localPosition.y + 0.5f, _player.transform.localPosition.z);
                _canvas.transform.localRotation = _player.transform.localRotation;
            }
        }
    }
}