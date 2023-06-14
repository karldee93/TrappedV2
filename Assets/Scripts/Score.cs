using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public int playerScore = 0;
    public int roundNum = 1;
    public int ammoCap;
    public int currentInClip;
    public int akmAmmoCap;
    public int akmCurrentInClip;
    // sets variables to hold text objects
    public Text points;
    public Text round;
    public Text inClip;
    public Text cap;
    public Text akmInClip;
    public Text akmCap;
    void Update()
    {   // updates UI display with current variable values
        points.GetComponent<Text>().text = "Score: " + playerScore.ToString();
        round.GetComponent<Text>().text = "Round: " + roundNum.ToString();
        inClip.GetComponent<Text>().text = "Shells in Barrel: " + currentInClip.ToString();
        cap.GetComponent<Text>().text = "Shells Bag: " + ammoCap.ToString();
        akmInClip.GetComponent<Text>().text = "AKM Ammo in Clip: " + akmCurrentInClip.ToString();
        akmCap.GetComponent<Text>().text = "AKM Ammo Bag: " + akmAmmoCap.ToString();
    }
}
