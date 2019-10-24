using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : Enemy
{
    public GameObject target;
    AudioSource aus;
    public float attack_dist;
    Transform trans;
    Transform trans_targ;
    float rotation_z_deg;
    Vector2 look_at;
    float dist;
    public float angle_shift;
    bool reload = true;
    public float reload_time;
    public float proj_speed;
    List<GameObject> e_projectiles;
    GameObject e_projectile;
    public int count_of_proj;
    public GameObject e_projectile_prefab;
    public GameObject proj_pos;
    Transform trans_proj_pos;

    void Start()
    {
        aus = GetComponent<AudioSource>();
        trans_proj_pos = proj_pos.GetComponent<Transform>();
        e_projectiles = new List<GameObject>();
            for (int j = 0; j < count_of_proj; j++)
            {
                e_projectile = (GameObject)Instantiate(e_projectile_prefab);
                e_projectiles.Add(e_projectile);
                e_projectile.SetActive(false);
            }
        if (!target)
            target = GameObject.Find("MC");
        trans = GetComponent<Transform>();
        trans_targ = target.GetComponent<Transform>();
    }

    void Update()
    {
        look_at = new Vector2(trans_targ.position.x - trans.position.x, trans_targ.position.y - trans.position.y);
        dist = look_at.magnitude;
        if (dist <= attack_dist)
        {
            Rotation();
            Fire();
        }
    }

    void Rotation()
    {
        rotation_z_deg = Mathf.Atan2(look_at.y, look_at.x) * Mathf.Rad2Deg + angle_shift;
        trans.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z_deg);
    }

    void Fire()
    {
        if (reload)
        {
            if (!aus.isPlaying)
                aus.Play();
            reload = false;
            ProjSpawn();
            Invoke("Reload", reload_time);
        }
    }

    void Reload()
    {
        reload = true;
    }

    void ProjSpawn()
    {
        for (int j = 0; j < count_of_proj; j++)
        {
            if (!e_projectiles[j].activeInHierarchy)
            {
                e_projectiles[j].transform.position = trans_proj_pos.position;
                e_projectiles[j].transform.rotation = trans.rotation;
                EnemyProjectile ep_scr = e_projectiles[j].GetComponent<EnemyProjectile>();
                ep_scr.speed = look_at.normalized * proj_speed; ;
                e_projectiles[j].SetActive(true);
                break;
            }
        }
    }
}
