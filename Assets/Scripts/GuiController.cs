using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    public GameObject health_bar;
    RectTransform rt_h;
    Vector2 rt_h_size = new Vector2(1.6f, 20);
    public GameObject fill_h;
    Slider health_slide;
    public GameObject weap_img_obj;
    Image weap_img;
    public Text weap_name;
    public Text ammo_txt;
    WeaponManager wm_scr;
    Animator anim_fill_h;
    public Text score_txt;
    void Awake()
    {
        weap_img = weap_img_obj.GetComponent<Image>();
        wm_scr = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        health_slide = health_bar.GetComponent<Slider>();
        rt_h = health_bar.GetComponent<RectTransform>();
        GuiAmmoChange(0, 100);
        GuiWeaponChange(0);
        anim_fill_h = fill_h.GetComponent<Animator>();
    }

    public void GuiHealthChange(float max_h, float curr_h)
    {
        rt_h.sizeDelta = new Vector2(max_h * rt_h_size.x, rt_h_size.y);
        health_slide.maxValue = max_h;
        if (health_slide.value > curr_h)
            anim_fill_h.SetBool("red", true);
        else
            anim_fill_h.SetBool("green", true);
        Invoke("HealthColorReturn",Time.deltaTime);
        health_slide.value = curr_h;
    }

    void HealthColorReturn()
    {
        anim_fill_h.SetBool("green", false);
        anim_fill_h.SetBool("red", false);
    }

    public void GuiAmmoChange(int curr_weap, int ammo_count)
    {
        if (curr_weap == 0)
        {
            ammo_txt.text = "\u221E";
            ammo_txt.fontSize = 48;
        }
        else
        {
            ammo_txt.text = ammo_count.ToString();
            ammo_txt.fontSize = 28;
        }
    }

    public void GuiWeaponChange(int curr_weap)
    {
        GameObject obj_img = wm_scr.weapons_prop[curr_weap].prefab_img;
        SpriteRenderer sr = obj_img.GetComponent<SpriteRenderer>();
        weap_img.sprite = sr.sprite;
        weap_name.text = wm_scr.weapons_prop[curr_weap].w_name;
    }

    public void GuiScoreSet(int value)
    {
        score_txt.text = value.ToString();
    }

}
