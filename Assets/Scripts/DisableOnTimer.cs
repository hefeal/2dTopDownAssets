using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTimer : MonoBehaviour
{
    public float timer = 0.05f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("DisableTimer", timer);
    }

    void DisableTimer()
    {
        gameObject.SetActive(false);
    }
}
