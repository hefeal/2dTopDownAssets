using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destroyable
{
    public float touch_dmg;
    public GameObject[] blink_on_dmg;
    public Material[] mat_dmg;
    public float blink_time = 0.05f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            McController p_scr = collision.GetComponent<McController>();
            p_scr.TakeDmg(touch_dmg);
        }
    }

    public void BlinkOnDmg()
    {
        if (!is_dead)
        {
            for (int j = 0; j < blink_on_dmg.Length; j++)
            {
                SpriteRenderer sp = blink_on_dmg[j].GetComponent<SpriteRenderer>();
                sp.material = mat_dmg[0];
                StartCoroutine(BlinkOff(sp, blink_time, j));
            }
        }
        else
            for (int j = 0; j < blink_on_dmg.Length; j++)
            {
                SpriteRenderer sp = blink_on_dmg[j].GetComponent<SpriteRenderer>();
                sp.material = mat_dmg[1];
            }

    }

    IEnumerator BlinkOff(SpriteRenderer sp_r, float delay, int m)
    {
        yield return new WaitForSeconds(delay);
        sp_r.material = mat_dmg[1];
    }
}
