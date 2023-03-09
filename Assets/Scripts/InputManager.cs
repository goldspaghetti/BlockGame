using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public AIPlayer aIPlayer;
    public Button aiPlayer;
    void Awake(){
        aiPlayer.onClick.AddListener(getAiOutput);
    }
    void getAiOutput(){
        aIPlayer.calculateMove();
    }
}
