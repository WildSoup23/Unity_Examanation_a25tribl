using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private int value;
    [SerializeField] private AudioSource sound;
    private bool hasPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayed)
        {
            if (!sound.isPlaying)
            {
                Destroy(this);
            }
        }
    }

    public int GainScore()
    {
        if (!hasPlayed)
        {
            sound.Play();
            hasPlayed = true;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            return value;
        }
        return 0;
    }
}
