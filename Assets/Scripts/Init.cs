using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    Scene curr_scene;

    void Start()
    {
        curr_scene = SceneManager.GetActiveScene();
        Invoke("In", 1);
    }

    private void In()
    {
        FindObjectOfType<AudioManager>().Play("Music");
    }

    public void RestartScene()
    {
        LoadScene(curr_scene.name);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
