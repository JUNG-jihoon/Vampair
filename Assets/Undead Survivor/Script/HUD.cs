using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Infotype {Exp, Level,Kill, Time, Health }
    public Infotype type;

    Text myText;
    Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case Infotype.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp;
                break;

            case Infotype.Level:
                myText.text = string.Format("LV.{0:F0}", GameManager.instance.level) ;
                break;
            case Infotype.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case Infotype.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:F0}:{1:F0}", min, sec);
                break;
            case Infotype.Health:
                float curhealth = GameManager.instance.health;
                float maxhealth = GameManager.instance.maxHealth;
                mySlider.value = curhealth / maxhealth;
                break;
        }
    }
}
