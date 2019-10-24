using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponProperties
{
    public int id;
    public string w_name;
    public GameObject prefab_obj;
    public GameObject prefab_item;
    public GameObject prefab_projectile;
    public int count_of_projectiles;
    public Vector2 projectile_speed;
    public float damage;
    public float reload_time;
    public int max_ammo;
    public int default_ammo;
    public GameObject prefab_img;
}
