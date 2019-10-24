using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class McController : MonoBehaviour
{
    public float max_health;
    float current_health;
    int[] ammo;
    AudioSource aus;
    public AudioClip[] clips;
    Animator anim_arms;
    Animator anim_legs;
    public GameObject arms;
    public GameObject legs;
    Rigidbody2D rb;
    public float max_speed;
    Vector2 speed;
    Vector2 speed_coef;
    float rotation_z_rad;
    float rotation_z_deg;
    Vector2 mouse_pos;
    Vector2 look_at;
    public float angle_shift;
    Transform trans;
    GameObject wm;
    WeaponManager wm_scr;
    public GameObject weapon_pos;
    AudioSource aus_weapon_pos;
    Transform trans_weap_pos;
    GameObject weapon_obj;
    int current_weapon = 0;
    bool reload = true;
    bool change_reload = true;
    Vector2 prj_speed;
    GameObject prj_spawn_pos;
    Weapon weap_src;
    public GameObject camera_pos;
    Transform trans_cam_pos;
    public float max_dist = 2;
    public float offset_speed = 3;
    public bool[] weapon_unlocked;
    GuiController gui_scr;
    bool recover = true;
    public float recover_time = 1.5f;
    public GameObject[] blink_on_dmg;
    public Material[] mat_dmg;
    public float blink_time = 0.05f;

    void Start()
    {
        gui_scr = GameObject.Find("Canvas/Gui").GetComponent<GuiController>();
        current_health = max_health;
        aus = GetComponent<AudioSource>();
        aus_weapon_pos = weapon_pos.GetComponent<AudioSource>();
        anim_arms = arms.GetComponent<Animator>();
        anim_legs = legs.GetComponent<Animator>();
        wm = GameObject.Find("WeaponManager");
        wm_scr = wm.GetComponent<WeaponManager>();
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        trans_weap_pos = weapon_pos.GetComponent<Transform>();
        trans_cam_pos = camera_pos.GetComponent<Transform>();
        trans_cam_pos.SetParent(null);
        ammo = new int[wm_scr.weapons_prop.Length];
        ammo[0] = 100;
        weapon_unlocked = new bool[wm_scr.weapons_prop.Length];
        weapon_unlocked[current_weapon] = true;
        ChangeWeapon(current_weapon);
    }

    void Update()
    {  
          
        //moving
        speed_coef = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        speed.x = Input.GetAxis("Horizontal") * max_speed * Mathf.Abs(speed_coef.x);
        speed.y = Input.GetAxis("Vertical") * max_speed * Mathf.Abs(speed_coef.y);
        //aiming
        mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CameraPos();
        look_at = new Vector2(mouse_pos.x - trans.position.x, mouse_pos.y - trans.position.y).normalized;
        //firing
        if (Input.GetButton("Fire1") && reload)
        {
            anim_arms.SetBool("move", true);
            anim_arms.SetBool("stay", false);
            reload = false;
            Attack();
            Invoke("Reload", wm_scr.weapons_prop[current_weapon].reload_time);
        }

        if (Input.GetButton("Fire2") && change_reload)
        {
            ChangeWeapCheck();
        }
    }

    void FixedUpdate()
    {
        Move();
        Rotation();
    }

    void CameraPos()
    {
        Vector3 world_center_screen = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width / 2, Screen.height / 2, 0)) ;
        Vector3 mouse_pos_center = new Vector3 (mouse_pos.x - world_center_screen.x, mouse_pos.y - world_center_screen.y, 0);
        trans_cam_pos.position = Vector3.MoveTowards(trans_cam_pos.position, mouse_pos_center, Time.deltaTime * offset_speed);
        trans_cam_pos.position = new Vector3(Mathf.Clamp(trans_cam_pos.position.x, -max_dist, max_dist), Mathf.Clamp(trans_cam_pos.position.y, -max_dist, max_dist), trans_cam_pos.position.z);
    }

    void Move()
    {
        rb.velocity = speed;
        if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {

            if (!aus.isPlaying)
            {
                aus.Play();
                aus.clip = clips[0];
            }
            anim_legs.SetBool("move", true);
            anim_legs.SetBool("stay", false);
        }
        else
        {
            anim_legs.SetBool("move", false);
            anim_legs.SetBool("stay", true);
        }
    }

    void Rotation()
    {
        rotation_z_rad = Mathf.Atan2(look_at.y, look_at.x);
        rotation_z_deg = rotation_z_rad * Mathf.Rad2Deg + angle_shift;
        trans.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z_deg);
    }

    void Attack()
    {
        if (ammo[current_weapon] > 0 || current_weapon == 0)
        {
            if (current_weapon != 0)
                AmmoChange(current_weapon,-1);
            weap_src.Attack(current_weapon, look_at, trans.rotation);
        }
        else
        {
            ChangeWeapCheck();
        }
    }

    void Reload()
    {
        reload = true;
        anim_arms.SetBool("move", false);
        anim_arms.SetBool("stay", true);
    }

    void ChangeWeapCheck()
    {
        change_reload = false;
        Invoke("ChangeReload", 0.2f);
        current_weapon++;
        if (current_weapon >= wm_scr.weapons_prop.Length)
            current_weapon = 0;
        else
        {
            for (int i = current_weapon; i < wm_scr.weapons_prop.Length; i++)
            {
                if (weapon_unlocked[i] && ammo[i] > 0)
                {
                    current_weapon = i;
                    break;
                }
                else
                    current_weapon = 0;
            }
        }
        ChangeWeapon(current_weapon);
    }

    public void ChangeWeapon(int weap_id)
    {
        if (weapon_obj)
            Destroy(weapon_obj);
        current_weapon = weap_id;
        weapon_obj = (GameObject)Instantiate(wm_scr.weapons_prop[weap_id].prefab_obj);
        weapon_obj.transform.SetParent(trans_weap_pos);
        weapon_obj.transform.localPosition = Vector2.zero;
        weapon_obj.transform.rotation = trans.localRotation;
        weap_src = weapon_obj.GetComponent<Weapon>();
        aus_weapon_pos.clip = clips[1];
        aus_weapon_pos.Play();
        gui_scr.GuiWeaponChange(weap_id);
        gui_scr.GuiAmmoChange(weap_id, ammo[weap_id]);
    }

    void ChangeReload()
    {
        change_reload = true;
    }

    public void TakeDmg(float dmg)
    {
        if (recover)
        {
            recover = false;
            aus.clip = clips[3];
            aus.Play();
            Invoke("Recover", recover_time);
            BlinkOnDmg();
            ChageHealth(-1 * dmg);
        }
    }

    void Recover()
    {
        recover = true;
    }

    public void ChageHealth(float change_val)
    {
        current_health = current_health + change_val;
        if (current_health > max_health)
            current_health = max_health;
        else if (current_health <= 0)
            GameOver();
        gui_scr.GuiHealthChange(max_health, current_health);
    }

    public void UnlockWeap(int weap_num)
    {
        weapon_unlocked[weap_num] = true;
    }

    public void AmmoChange(int weap_id, int ammo_val)
    {
        ammo[weap_id] = ammo[weap_id] + ammo_val;
        if (ammo[weap_id] > wm_scr.weapons_prop[weap_id].max_ammo)
            ammo[weap_id] = wm_scr.weapons_prop[weap_id].max_ammo;
        else if (ammo[weap_id] <= 0)
            ammo[weap_id] = 0;
        gui_scr.GuiAmmoChange(weap_id, ammo[weap_id]);
    }

    public void BlinkOnDmg()
    {
        for (int j = 0; j < blink_on_dmg.Length; j++)
        {
            SpriteRenderer sp = blink_on_dmg[j].GetComponent<SpriteRenderer>();
            sp.material = mat_dmg[0];
            StartCoroutine(BlinkOff(sp, blink_time, j));
        }
    }

    IEnumerator BlinkOff(SpriteRenderer sp_r, float delay, int m)
    {
        yield return new WaitForSeconds(delay);
        sp_r.material = mat_dmg[1];
    }

    void GameOver()
    {
        Init i_scr = GameObject.Find("SceneManager").GetComponent<Init>();
        i_scr.RestartScene();
    }

}
