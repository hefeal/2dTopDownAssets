using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy_pref;
    protected GameObject enemy;
    public GameObject[] waypoint;
    public int enemy_count = 1;
    protected int j;
    public float time_spawn;
    public Vector2 time_spawn_range;
    protected List<GameObject> enemies;

    protected void Start()
    {
        enemies = new List<GameObject>();
        for (j = 0; j < enemy_count; j++)
        {
            enemy = (GameObject)Instantiate(enemy_pref);

            for (int i = 0; i < waypoint.Length; i++)
            {
                if (waypoint[i].activeInHierarchy)
                { 
                    EnemyWaypoint e_scr = enemy.GetComponent<EnemyWaypoint>();
                    e_scr.waypoints[i] = waypoint[i];
                }
            }
            enemy.SetActive(false);
            enemies.Add(enemy);
        }
        Invoke("Spawn", time_spawn);
    }


    protected void Spawn()
    {
        for (j = 0; j < enemies.Count; j++)
        {
            if (!enemies[j].activeInHierarchy)
            {
                enemies[j].transform.position = transform.position;
                enemies[j].transform.localScale = transform.localScale;
                EnemyWaypoint e_scr2 = enemies[j].GetComponent<EnemyWaypoint>();
                e_scr2.health = e_scr2.health_max;
                e_scr2.SetWP(0);
                enemies[j].SetActive(true);
                break;

            }
        }
        Invoke("Spawn", time_spawn);
    }
}
