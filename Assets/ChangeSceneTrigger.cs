using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int sceneIndex;
    [SerializeField] private SceneTransitionManager sceneTransManager;

    private void OnTriggerEnter(Collider other)
    {
        sceneTransManager.GoToSceneAsync(sceneIndex);
    }
}
