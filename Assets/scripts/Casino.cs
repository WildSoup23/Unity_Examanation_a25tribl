using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Casino : MonoBehaviour
{
    public TextMeshProUGUI press_E;
    public GameObject bubble;
    [SerializeField] private TextMeshProUGUI textBubble;
    [SerializeField] private List<string> textList = new List<string>();
    [SerializeField] public int textStage = 0;
    public bool tutroialOver = false;
    [SerializeField] private GameObject beter;
    private int betAmount;
    [SerializeField] private TextMeshProUGUI bet_txt;
    private playe_Script player;
    [SerializeField] private int winChanse;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playe_Script>();
    }
    // Update is called once per frame
    void Update()
    {
        textBubble.text = textList[textStage];
        bet_txt.text = $"{betAmount}";

        if(textStage == 2)
        {
            beter.SetActive(true);
        }
        else
        {
            beter.SetActive(false);
        }
    }
    public void ChangeTextStage(int amount)
    {
        textStage += amount;
        if(textStage > textList.Count - 1)
        {
            textStage = textList.Count-1;
        }
        if( textStage == 2)
        {
            tutroialOver = true;
        }
        else if(textStage >=4)
        {
            textStage = 2;
            betAmount = 0;
        }
    }
    public void ChangeBetAmount(int amount)
    {
        betAmount += amount;
        if (betAmount > player.score)
        {
            betAmount = player.score;
        }
        else if(betAmount < 0)
        {
            betAmount = 0;
        }
    }

    public void StartBet()
    {
        textStage = 3;
        Invoke("Bet", 2f);
    }

    private void Bet()
    {
        if(betAmount == 0)
        {
            textStage = 6;
            return;
        }

        int bettingAmount = betAmount;
        int rn = Random.Range(0, 100);

        if (rn <= winChanse)
        {
            textStage = 4;
            player.score += bettingAmount;
        }
        else
        {
            textStage = 5;
            player.score -= bettingAmount;
        }
    }
}
