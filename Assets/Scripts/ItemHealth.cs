using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : Items
{
    public float health_value;

    protected override void ItemAction()
    {
        mc_scr.ChageHealth(health_value);
        Taken();
    }
}
