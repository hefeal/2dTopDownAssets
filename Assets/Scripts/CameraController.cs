using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject MC;
    public GameObject cam_pos_offset;
    float player_x;
    float player_y;
    Vector3 new_pos;
    Transform trans_MC;
    Transform trans;
    Transform trans_offset;

    private void Start()
    {
        trans = GetComponent<Transform>();
        trans_MC = MC.GetComponent<Transform>();
        trans_offset = cam_pos_offset.GetComponent<Transform>();
    }

    void Update()
    {
        if (MC != null)
        {
            player_x = trans_MC.position.x;
            player_y = trans_MC.position.y;
            new_pos = new Vector3(player_x + trans_offset.position.x, player_y + trans_offset.position.y, trans.position.z);
            trans.position = new_pos;
        }

    }

}
