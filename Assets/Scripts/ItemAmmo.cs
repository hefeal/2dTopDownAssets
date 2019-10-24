using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAmmo :Items
{
    public int weap_id;
    public int ammo_count;

    protected override void ItemAction()
    {
        mc_scr.AmmoChange(weap_id, ammo_count);
    }
}
