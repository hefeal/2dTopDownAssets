using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score;
    GuiController gui_scr;
    
    void Start()
    {
        gui_scr = GameObject.Find("Canvas/Gui").GetComponent<GuiController>();
    }

    public void ChangeScore(int value)
    {
        score = score + value;
        gui_scr.GuiScoreSet(score);
    }


}
