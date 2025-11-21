using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class playe_Script : MonoBehaviour
{

    public int health;
    public int max_health;
    private Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    private bool canJump = false;

    public Transform startPoint;
    public Transform spawnPoint;
    [SerializeField] private TextMeshProUGUI score_txt;
    [SerializeField] public int score;

    [SerializeField] private Animator anim;
    private SpriteRenderer spr;

    [SerializeField] AudioSource ding;
    [SerializeField] AudioSource jump_sfx;
    [SerializeField] AudioSource walk_sfx;
    [SerializeField] AudioSource hurt_sfx;
    [SerializeField] AudioSource checkPoint_Sfx;
    [SerializeField] GameObject trail;

    [SerializeField] private TextMeshProUGUI esc_txt;

    public float timer = 0f;
    [SerializeField] private TextMeshProUGUI timer_txt;
    
    private bool GameOver = false;
    [SerializeField] private GameObject gameOverScreen;
    public GameObject winScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = anim.gameObject.GetComponentInChildren<SpriteRenderer>();
        health = max_health;
        spawnPoint = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timer_txt.gameObject.SetActive(true);
            timer_txt.text = $"{timer}";
        }
        else
        {
            timer_txt.gameObject.SetActive(false);
        }

        if (timer < 0)
        {
            GameOver = true;
            gameOverScreen.SetActive(true);
        }
        if (!GameOver)
        {
            float input = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.Space) && canJump)
            {
                rb.AddForce(Vector2.up * jumpPower);
                jump_sfx.Play();
                canJump = false;
            }

            if (input > 0f) spr.flipX = false;
            else spr.flipX = true;

            rb.linearVelocityX = input * speed;

            anim.SetFloat("speed", (Mathf.Sqrt(Mathf.Pow(input, 2))));

            if (canJump && (Mathf.Sqrt(Mathf.Pow(input, 2))) > 0.01)
            {
                walk_sfx.volume = 1f;
            }
            else walk_sfx.volume = 0f;
        }
            score_txt.text = $"{score}";
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("1");
        if (collision.gameObject.layer == 6)
        {
            canJump = true;
            trail.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("2");
        if (collision.gameObject.layer == 6)
        {   
            canJump = false;
            trail.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score++;
            ding.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("CheckPoint"))
        {
            if (collision.GetComponent<CheckPoint>().isWin)
            {
                timer = 100000;
                winScreen.SetActive(true);
            }
            else
            {
                spawnPoint = collision.transform;
                checkPoint_Sfx.Play();
                collision.GetComponent<CheckPoint>().ShowText();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            ChangeHealth(1);
            Destroy(collision.gameObject);
        }
        
    }
    public void Reset()
    {
        transform.position = spawnPoint.position;
        ChangeHealth(1);
        trail.SetActive(false);
    }

    public void Restart()
    {
        if(health <= 0) health = max_health;
        transform.position = startPoint.position;
        trail.SetActive(false);
    }

    public void ChangeHealth(int amount)
    {
        Debug.Log("ouch");
        hurt_sfx.Play();
        health -= amount;
        if(health >=0) Restart();
    }
}
