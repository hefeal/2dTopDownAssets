using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManual : MonoBehaviour
{
    SpriteRenderer sr;
    public Sprite[] sp;
    public float second_per_frame;
    public float delay;
    int i;
    public bool not_loop;
    Transform trans;

    void OnEnable()
    {
        i = 0;
        trans = GetComponent<Transform>();
        if (delay == 0)
            delay = second_per_frame;
        sr = GetComponent<SpriteRenderer>();
        Invoke("FrameChange", delay);
    }

    void FrameChange()
    {
        sr.sprite = sp[i];
        i++;
        if (i >= sp.Length && !not_loop)
            i = 0;
        if (i < sp.Length)
            Invoke("FrameChange", second_per_frame);
    }

    private void OnDisable()
    {
        i = 0;
    }
}
