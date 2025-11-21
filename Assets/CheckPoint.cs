using UnityEngine;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pop_up;
    
    // Update is called once per frame
    void Update()
    {
        pop_up.alpha -= Time.deltaTime;
    }

    public void ShowText()
    {
        pop_up.alpha = 1f;
    }
}
