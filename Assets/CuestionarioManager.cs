using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;

public class CuestionarioManager : MonoBehaviour
{
    [SerializeField] GameObject[] _preguntas;
    [SerializeField] GameObject _enhorabuena;
    [SerializeField] GameObject _cuestionario;
    [SerializeField] AudioTrigger _sonidoCorrecto;
    [SerializeField] AudioTrigger _sonidoError;
    [SerializeField] TextMeshProUGUI _BotonText;
    
    private GameObject _respuesta;

    private int _numPregunta = 0;
    private bool _respuestaCorrecta = false;
    private bool _acabat = false;

    public void Start()
    {
        if (_preguntas.Length > 0)
        {
            _preguntas[_numPregunta].SetActive(true);
        }
        else
        {
            Debug.LogError("Necesitas a�adir preguntas al GameObject <i>Contenido</i> del Cuestionario");
        }
    }

    public void f_cambiarRespuesta(bool respuestaUsuario)
    {
        _respuestaCorrecta = respuestaUsuario ? !_respuestaCorrecta : respuestaUsuario;
    }

    public void f_RespuestaSelecionada(GameObject respuesta)
    {
        _respuesta = respuesta;
    }

    public void f_compruebaRespuesta()
    {
        if(_acabat)
        {
            _cuestionario.transform.localScale = Vector3.zero;
        }
        else
        {
            if (_respuestaCorrecta)
            {
                _respuestaCorrecta = false;
                _sonidoCorrecto.PlayAudio();
                _preguntas[_numPregunta++].SetActive(false);
                if (_preguntas.Length > _numPregunta)
                {
                    _preguntas[_numPregunta].SetActive(true);
                }
                else
                {
                    _enhorabuena.SetActive(true);
                    _BotonText.text = "Tancar q�estionari";
                    _acabat = true;
                }

            }
            else
            {
                _sonidoError.PlayAudio();
                _respuesta.GetComponent<ToggleDeselect>().interactable = false;
                _respuesta.GetComponent<ToggleDeselect>().isOn = false;
                GameObject error = f_GetChildWithName(_respuesta, "Error");
                error.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }
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
