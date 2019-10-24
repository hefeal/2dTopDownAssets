using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{

    protected override void OnTriggerEnter2D(Collider2D collision)
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

        if (collision.tag == "Player")
        {
            project_obj.SetActive(false);
            Stop();
            McController mc_scr = collision.GetComponent<McController>();
            mc_scr.TakeDmg(damage);
            if (expl_obj)
            {
                expl_obj.SetActive(true);
                Invoke("DisableObj", disable_time);
            }
        }
    }
}
