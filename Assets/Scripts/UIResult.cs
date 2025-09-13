using UnityEngine;
using UnityEngine.UI;

public class UIResult : MonoBehaviour
{
    [Header("Value Texts")]
    public Text scoreValueText;
    public Text bestScoreValueText;
    public Text coinValueText;

    void Start()
    {
        int lastScore = ScoreHolder.lastScore;
        int best = PlayerPrefs.GetInt("BestScore", 0);
        int coins = PlayerPrefs.GetInt("Coins", 0);

        if (scoreValueText) scoreValueText.text = lastScore.ToString();
        if (bestScoreValueText) bestScoreValueText.text = best.ToString();
        if (coinValueText) coinValueText.text = coins.ToString("00"); // hi?n th? 01, 02,...
    }
}
