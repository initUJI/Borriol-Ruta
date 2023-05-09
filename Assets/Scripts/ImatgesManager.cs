using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;
using UnityEngine.UI;

public class ImatgesManager : MonoBehaviour
{
    [SerializeField] GameObject[] _imatges;
    [SerializeField] GameObject _canvasImatges;
    [SerializeField] GameObject _botonIzquierda;
    [SerializeField] GameObject _botonDerecha;
    [SerializeField] TextMeshProUGUI _DescripcionTexto;

    private int _numImatge = 0;
    [SerializeField] string[] _descripciones =
    {
        "Descripcion de la Imagen 1",
        "Descripcion de la Imagen 2",
    };

    public void Start()
    {
        if (_imatges.Length > 0)
        {
            _imatges[_numImatge].SetActive(true);
            _DescripcionTexto.text = _descripciones[_numImatge];

            _botonIzquierda.GetComponent<Button>().interactable = false;
            if(_imatges.Length == 1)
            {
                _botonDerecha.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            Debug.LogError("Necesitas añadir Imagenes al GameObject <i>Contenido</i> del Canvas de Cartells");
        }
    }

    public void f_cambiarImatge(string direccion)
    {
        switch (direccion)
        {
            case "derecha":
                if (_numImatge < _imatges.Length)
                {
                    _imatges[_numImatge].SetActive(false);
                    _numImatge++;
                    _imatges[_numImatge].SetActive(true);
                    _DescripcionTexto.text = _descripciones[_numImatge];

                    _botonIzquierda.GetComponent<Button>().interactable = true; 
                }

                if (_numImatge == _imatges.Length - 1)
                {
                    _botonDerecha.GetComponent<Button>().interactable = false;
                }
                break;
            case "izquierda":
                if (_numImatge > 0) 
                {
                    _imatges[_numImatge].SetActive(false);
                    _numImatge--;
                    _imatges[_numImatge].SetActive(true);
                    _DescripcionTexto.text = _descripciones[_numImatge];

                    _botonDerecha.GetComponent<Button>().interactable = true; 
                }
                if (_numImatge - 1 < 0)
                {
                    _botonIzquierda.GetComponent<Button>().interactable = false;
                }
                break;
            default: break;
        }
    }

    public void f_CerrarCanvas()
    {
        _canvasImatges.SetActive(false);
    }

    private GameObject f_GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.FindChildRecursive(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
