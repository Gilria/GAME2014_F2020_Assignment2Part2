using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{
    public Text txt_Score;
    
    public void OnBtnPause()
    {
        UIManager.Instance.OpenPanel(1);
        SoundManageer.Instance.PlaySFX(0);
        GameManager.Instance.gamestate = GameState.Pause;
        Time.timeScale = 0;
    }


    private void OnEnable()
    {
        txt_Score.text = "0";
    }


    public void UpdateScore(int value)
    {
        txt_Score.text = value.ToString();
    }
}
