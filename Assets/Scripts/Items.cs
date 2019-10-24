using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    protected Animator anim;
    public float fade_time = 1;
    protected AudioSource aus;
    protected bool taken;
    protected McController mc_scr;

    void Start()
    {
        aus = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    public void FloatObj()
    {
        anim.SetBool("float", true);
    }

    public void Taken()
    {
        taken = true;
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }
        if (!aus.isPlaying)
            aus.Play();
        anim.SetBool("taken", true);
        Invoke("Del", fade_time);
    }

    void Del()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!taken)
            {
                mc_scr = collision.GetComponent<McController>();
                ItemAction();
                Taken();
            }
        }
    }

    protected virtual void ItemAction()
    {

    }
}
