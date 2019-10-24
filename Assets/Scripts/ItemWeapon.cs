using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapon : Items
{
    public int weap_id;

    protected override void ItemAction()
    {
        WeaponManager wm_scr = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        mc_scr.UnlockWeap(weap_id);
        mc_scr.AmmoChange(weap_id, wm_scr.weapons_prop[weap_id].default_ammo);
        mc_scr.ChangeWeapon(weap_id);
    }
}
