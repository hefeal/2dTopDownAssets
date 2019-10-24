using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : Enemy
{
    public float speed;
    public GameObject[] waypoints;
    int curr_waypoint_num =0;
    Rigidbody2D rb;
    Transform trans;
    float rotation_z_deg;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        MoveToPoint(curr_waypoint_num);
    }

    void MoveToPoint(int w_num)
    {
        rb.velocity = new Vector2(waypoints[w_num].transform.position.x - trans.position.x,
                                  waypoints[w_num].transform.position.y - trans.position.y).normalized * speed;
        Rotation();
    }

    void Rotation()
    {
        rotation_z_deg = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        trans.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z_deg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (Collider2D c in waypoints[curr_waypoint_num].GetComponents<Collider2D>())

        {
            if (c == collision)
                curr_waypoint_num++;
            if (curr_waypoint_num >= waypoints.Length || !waypoints[curr_waypoint_num])
                curr_waypoint_num = 0;
            MoveToPoint(curr_waypoint_num);
        }
    }
    
    public void SetWP(int wp)
    {
        curr_waypoint_num = wp;
    }
}
