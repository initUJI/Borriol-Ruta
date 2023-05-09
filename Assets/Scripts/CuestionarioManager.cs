using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using TMPro;

public class CuestionarioManager : MonoBehaviour
{
    [SerializeField] GameObject[] _preguntas;
    [SerializeField] GameObject _inicio;
    [SerializeField] GameObject _enhorabuena;
    [SerializeField] GameObject _cuestionario;
    [SerializeField] AudioTrigger _sonidoCorrecto;
    [SerializeField] AudioTrigger _sonidoError;
    [SerializeField] TextMeshProUGUI _BotonText;
    
    private GameObject _respuesta;

    private int _numPregunta = 0;
    private bool _respuestaCorrecta = false;
    private bool _acabat = false;
    private bool _començat = false;

    public void Start()
    {
        if (_preguntas.Length > 0)
        {
            _BotonText.text = "Començar qüestionari";
        }
        else
        {
            Debug.LogError("Necesitas añadir preguntas al GameObject <i>Contenido</i> del Cuestionario");
        }
    }

    public void f_ComenzarCuestionario()
    {
        _inicio.SetActive(false);
        _preguntas[_numPregunta].SetActive(true);
        _BotonText.text = "Comprovar resposta";
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
        if (!_començat) 
        {
            _començat = true; 
            f_ComenzarCuestionario();
        }
        else if (_acabat)
        {
            _cuestionario.SetActive(false);
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
                    _BotonText.text = "Tancar qüestionari";
                    _acabat = true;
                }

            }
            else
            {
                if (!_respuesta.GetComponent<ToggleDeselect>().isOn) return;
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
