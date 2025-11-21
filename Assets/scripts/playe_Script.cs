using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class playe_Script : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    private bool canJump = false;

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

    [SerializeField] GameObject casino;
    private bool CanOpenCasino = false;
    private bool CasinoIsOpen = false;
    [SerializeField] private TextMeshProUGUI esc_txt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = anim.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CasinoIsOpen)
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
        else
        {
            anim.SetFloat("speed", 0);
            walk_sfx.volume = 0f;
        }


            score_txt.text = $"{score}";
            
            
        /*
        if (CanOpenCasino && !CasinoIsOpen)
        {
            casino.GetComponent<Casino>().press_E.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                CasinoIsOpen = true;
                esc_txt.gameObject.SetActive(true);
                casino.GetComponent<Casino>().bubble.SetActive(true);
            }
        }
        else
        {
            casino.GetComponent<Casino>().press_E.gameObject.SetActive(false);
        }

        if (CasinoIsOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CasinoIsOpen = false;
                esc_txt.gameObject.SetActive(false);
                casino.GetComponent<Casino>().bubble.SetActive(false);
                if (casino.GetComponent<Casino>().tutroialOver)
                {
                    casino.GetComponent<Casino>().textStage = 2;
                }
                else
                {
                    casino.GetComponent<Casino>().textStage = 0;
                }
                
            }
        }
        */
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
        /*
        else if (collision.gameObject.CompareTag("CheckPoint"))
        {
            spawnPoint.position = collision.transform.position;
            checkPoint_Sfx.Play();
            //collision.GetComponent<CheckPoint>().ShowText();
        }
        else if (collision.gameObject.CompareTag("Casino"))
        {
            CanOpenCasino = true;
        }
        */
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Casino"))
        {
            CanOpenCasino = false;
        }
    }

    public void Reset()
    {
        transform.position = spawnPoint.position;
        hurt_sfx.Play();
        trail.SetActive(false);
    }
    */
}
