using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public int score;
    public float health_max;
    public float health;
    public DropProperties[] drop;
    protected GameObject drop_item;
    protected Quaternion drop_rotate;
    protected Vector3 drop_pos_offset;
    protected bool is_dead;
    virtual protected void OnEnable()
    {
        health = health_max;
        is_dead = false;
    }

    virtual public void HealthChange(float dmg)
    {
        health = health - dmg;
        if (health <= 0 && !is_dead)
        {
            is_dead = true;
            DoDeath();
        }
            
    }

    virtual public void DoDeath()
    {

        for (int i = 0; i < drop.Length; i++)
        {
            drop_pos_offset = new Vector2(Random.Range(-1 * drop[i].drop_range.x, drop[i].drop_range.x), Random.Range (- 1 * drop[i].drop_range.y, drop[i].drop_range.y));
            if (drop[i].use_parent_rotate)
                drop_rotate = transform.rotation;
            else
                drop_rotate = Quaternion.identity;
            if (Random.Range(0, drop[i].max_prob) <= drop[i].probability)
                drop_item = (GameObject)Instantiate(drop[i].drop_pref, transform.position + drop_pos_offset, drop_rotate);
        }
        if (score > 0)
        {
            ScoreManager sm_scr = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
            sm_scr.ChangeScore(score);
        }

        gameObject.SetActive(false);
    }
}
