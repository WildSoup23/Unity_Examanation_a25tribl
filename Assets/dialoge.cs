using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class dialoge : MonoBehaviour
{
    public TextMeshProUGUI press_E;
    public GameObject canvas;
    public GameObject startBorder;
    [SerializeField] private TextMeshProUGUI textBubble;
    [SerializeField] private List<string> textList = new List<string>();
    [SerializeField] public int textStage = 0;
    public bool tutroialOver = false;
    private bool canOpen = false;
    private playe_Script player;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playe_Script>();
    }
    
    // Update is called once per frame
    void Update()
    {
        textBubble.text = textList[textStage];
        if (canOpen && !tutroialOver)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.enabled = false;
                canvas.SetActive(true);
                press_E.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                player.enabled = true;
                canvas.SetActive(false);
                if (!tutroialOver) press_E.gameObject.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeTextStage(1);
            }
        }
    }
    public void ChangeTextStage(int amount)
    {
        textStage += amount;
        if(textStage > textList.Count - 1)
        {
            textStage = textList.Count-1;
        }
        else if (textStage == 3)
        {
            tutroialOver = true;
            player.enabled = true;
            canvas.SetActive(false);
            player.timer = 120f;
            startBorder.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !tutroialOver)
        {
            canOpen = true;
            press_E.gameObject.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canOpen = false;
            canvas.SetActive(false);
            if (!tutroialOver) press_E.gameObject.SetActive(false);
        }
    }
}
