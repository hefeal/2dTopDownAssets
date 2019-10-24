using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    public GameObject projectile_pos;
    public GameObject flash;
    public AudioSource aus;
    protected AudioClip clip;
    protected Vector2 prj_speed;
    protected GameObject wm;
    protected WeaponManager wm_scr;
    protected Transform trans_proj_pos;

    virtual protected void Start()
    {
        wm = GameObject.Find("WeaponManager");
        wm_scr = wm.GetComponent<WeaponManager>();
        aus = GetComponent<AudioSource>();
        trans_proj_pos = projectile_pos.GetComponent<Transform>();
    }

    virtual public void Attack(int current_weapon, Vector2 look_at, Quaternion rotat)
    {
        prj_speed = GetSpeed(current_weapon, look_at);
        ProjectileSpawn(current_weapon, rotat);
    }

    virtual protected void ProjectileSpawn(int current_weapon, Quaternion rotat)
    {    
        for (int j = 0; j < wm_scr.projectiles[current_weapon].Count; j++)
        {
            if (!wm_scr.projectiles[current_weapon][j].activeInHierarchy)
            {
                wm_scr.projectiles[current_weapon][j].transform.position = trans_proj_pos.position;
                wm_scr.projectiles[current_weapon][j].transform.rotation = rotat;
                Projectile pr_scr = wm_scr.projectiles[current_weapon][j].GetComponent<Projectile>();
                pr_scr.speed = prj_speed;
                wm_scr.projectiles[current_weapon][j].SetActive(true);
                break;
            }
        }
        flash.SetActive(true);
        aus.Play();
    }

    virtual protected Vector2 GetSpeed(int current_weapon, Vector2 look_at)
    {
        Vector2 speed = new Vector2(look_at.x * wm_scr.weapons_prop[current_weapon].projectile_speed.x, look_at.y * wm_scr.weapons_prop[current_weapon].projectile_speed.y);
        return speed;
    }
}
