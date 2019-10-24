using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponProperties[] weapons_prop;
    public List<GameObject>[] projectiles;
    GameObject projectile;
    public static WeaponManager wm_instance = null;

    private void Awake()
    {

        if (!wm_instance)
        {
            wm_instance = this;

        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

        void Start()
    {
        projectiles = new List<GameObject>[weapons_prop.Length];
        for (int i = 0; i < weapons_prop.Length; i++)
        {
            projectiles[i] = new List<GameObject>();
            for (int j=0; j < weapons_prop[i].count_of_projectiles; j++)
            {
                projectile = (GameObject)Instantiate(weapons_prop[i].prefab_projectile);
                projectile.transform.SetParent(transform);
                Projectile p_scr = projectile.GetComponent<Projectile>();
                p_scr.damage = weapons_prop[i].damage;
                projectiles[i].Add(projectile);
                projectile.SetActive(false);
            }
        }
    }
}
