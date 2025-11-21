using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private bool isRool;
    [SerializeField] private bool isFly;
    [SerializeField] private bool hasLifeTime;
    [SerializeField] private float lifeTine;
    [SerializeField] float speed;
    [SerializeField] float amplitude;
    private Rigidbody2D rb;

    private Transform startPos;
    private float life = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRool)
        {
            rb.AddForce(Vector2.right * speed);
        }
        else if (isFly)
        {
            life += Time.deltaTime;
            transform.position = new Vector2(startPos.position.x, startPos.position.y + ((Mathf.Sin(life * speed) * amplitude)* Time.deltaTime));
        }


        if (hasLifeTime)
        {
            lifeTine -= Time.deltaTime;

            if(lifeTine <= 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
