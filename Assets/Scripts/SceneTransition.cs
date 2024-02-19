using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [SerializeField] private int toSceneIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit door");
        if (other.tag == "Player")
        {
            Debug.Log("Player Hit door");
            SceneManager.LoadScene(toSceneIndex);
            //SceneManager.MoveGameObjectToScene(other.gameObject, SceneManager.GetSceneByBuildIndex(1));
        }
    }
}
