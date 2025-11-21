using UnityEngine;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pop_up;
    public bool isWin;
    
    // Update is called once per frame
    void Update()
    {
        if(!pop_up == null) pop_up.alpha -= Time.deltaTime;
    }

    public void ShowText()
    {
        if(!pop_up == null) pop_up.alpha = 1f;
    }
}
