using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pausee : MonoBehaviour
{
    public Text txt_Score;
    

    private void OnEnable()
    {
        txt_Score.text = GameManager.Instance.Score.ToString();
    }


    //back to the game
    public void OnBtnReturn()
    {
        GameManager.Instance.gamestate = GameState.Running;
        SoundManageer.Instance.PlaySFX(0);
        Time.timeScale = 1;
        UIManager.Instance.Back();
    }

}
