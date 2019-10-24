using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropProperties 
{
    public GameObject drop_pref;
    public float probability;
    public float max_prob = 100;
    public bool use_parent_rotate;
    public Vector2 drop_range;
}
