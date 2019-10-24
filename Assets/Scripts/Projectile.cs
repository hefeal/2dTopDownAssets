using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 speed;
    public float damage;
    public GameObject project_obj;
    public GameObject expl_obj;
    public float disable_time = 1f;
    public float live_time = 10f;
    protected Vector2 start_pos;
    protected Rigidbody2D rb;
    AudioSource aus;

    protected virtual void OnEnable()
    {
        aus = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        start_pos = transform.position;
        rb.velocity = speed;
        Invoke("DisableObj", live_time);
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            project_obj.SetActive(false);
            Stop();
            if (expl_obj)
            {
                expl_obj.SetActive(true);
                Invoke("DisableObj", disable_time);
            }
        }

        if (collision.tag == "Enemy")
        {
            Enemy e_scr = collision.GetComponent<Enemy>();
            e_scr.HealthChange(damage);
            e_scr.BlinkOnDmg();
            project_obj.SetActive(false);
            Stop();
            if (expl_obj)
            {
                expl_obj.SetActive(true);
                Invoke("DisableObj", disable_time);
            }
        }

        if (collision.tag == "Crashable")
        {
            Enemy e_scr = collision.GetComponent<Enemy>();
            e_scr.HealthChange(damage);
            project_obj.SetActive(false);
            Stop();
            if (expl_obj)
            {
                expl_obj.SetActive(true);
                Invoke("DisableObj", disable_time);
            }
        }
    }

    protected void DisableObj()
    {
        CancelInvoke("DisableObj");
        project_obj.SetActive(true);
        expl_obj.SetActive(false);
        gameObject.SetActive(false);
    }

    protected void Stop()
    {
        if (!aus.isPlaying)
            aus.Play();
        rb.velocity = Vector2.zero;
        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }
    }
}
