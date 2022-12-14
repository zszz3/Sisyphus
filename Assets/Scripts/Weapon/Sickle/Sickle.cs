using UnityEngine;

public class Sickle : MonoBehaviour
{
    [SerializeField] private Vector3 scale;
    public float damage;
    public float cdTime;

    private float rotateSpeed; 
    private float tuning;
    private float speed;
    private float totalTime = 0;
    private float continueTime = 20f;
    private Transform playerTransform;
    private Transform sickleTransfrom;
    private Vector2 startSpeed;
    private Rigidbody2D rb2d;

    void Start()
    {
        totalTime = 0;
        rotateSpeed = 30;
        tuning = 0.09f;
        speed = 30;

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sickleTransfrom = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        if (playerTransform.position.x > sickleTransfrom.position.x)
        {
                rb2d.velocity = -transform.right* speed;
        }
        else
        {
            rb2d.velocity = transform.right * speed;
        }
        startSpeed = rb2d.velocity;
    }

    private void FixedUpdate()
    {
        totalTime += Time.deltaTime;
        if(scale != sickleTransfrom.localScale)
        {
            sickleTransfrom.localScale = scale;
        }
        transform.Rotate(0, 0, rotateSpeed);
        float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);
        transform.position = new Vector3(transform.position.x, y, 0.0f);
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) < 0.5f || Mathf.Abs(transform.position.x - playerTransform.position.x) > 2000f
            || totalTime>continueTime)
        {
            Destroy(gameObject);
        }
    }
}
