using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOnCollide : MonoBehaviour
{
    GameObject Canvs;
    public float timer = 5;
    public TextAsset text_files;
    public GameObject text_obj;
    public Text the_text;
    public Color text_color;
    bool is_active;
    Vector3 text_pos;
    // Start is called before the first frame update
    void Start()
    { 
        Canvs = GameObject.Find("Canvas");
    //    text_pos = Camera.main.WorldToScreenPoint(Vector3.zero);
    //    the_text.transform.position = text_pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!is_active)
             {
                text_obj.SetActive(true);
                text_obj.transform.SetParent(Canvs.transform, false);                  
                the_text.text = text_files.text;
                the_text.color = new Color(text_color.r, text_color.g, text_color.b);
                is_active = true;
                Invoke("Timer", timer);  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CancelInvoke("Timer");
            Timer();
        }
    }

    void Timer()
    {
        text_obj.transform.SetParent(transform, false);
        text_obj.SetActive(false);
        is_active = false;
    }

    
}
