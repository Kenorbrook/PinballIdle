using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallsManager : MonoBehaviour
{
    public Text Second;
    public Text Third;
    public Text Fought;
    public GameManager Gm;

    public static bool[] isOpenBall = new bool[] { true, false, false, false };

    public static int questCompleted = 0;

    public static int ballsLost = 0;

    public void ChangeSecond()
    {
        if(questCompleted>10)
            Second.text = $"Already done: \n 10/10";
        else
        Second.text = $"Already done: \n {questCompleted}/10";
    }

    public void ChangeThird()
    {
        if (ballsLost > 1000)
            Third.text = $"Balls lost: \n 1000/1000";
        else
        {
            Third.text = $"Balls lost: \n {ballsLost}/1000";
        }
    }

    public void ChangeFought()
    {
        
        if(Gm.POintSpent>1000000)
            Fought.text = $"Point spent:\n1.000.000/1.000.000";
        else
            Fought.text = $"Point spent:\n{Gm.POintSpent}/1.000.000";
    }
}
