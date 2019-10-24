using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotGun : Weapon
{
    public int count_of_shot;
    float angle;
    float step_angle;
    Vector2 look_at_angle;
    public float wide_size;
    public float angle_offset;

    override public void Attack(int current_weapon, Vector2 look_at, Quaternion rotat)
    {
        step_angle = wide_size / count_of_shot;
        angle = angle_offset - wide_size/2; 

        for (int i = 0; i < count_of_shot; i++)
        {
            look_at_angle = RotateVector(look_at, angle);
            prj_speed = GetSpeed(current_weapon, look_at_angle);
            ProjectileSpawn(current_weapon, rotat);
            angle = angle + step_angle;
        }
    }

    Vector2 RotateVector(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}
