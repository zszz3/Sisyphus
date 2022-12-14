using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Vector3 scale;
    public float damage;
    public float cdTime;

    private float tuning;
    private float speed;
    public int flagx = 1;
    public int flagy = 1;
    private Transform playerTransform;
    private Transform LaserTransform;
    private Vector2 startSpeed;
    private Rigidbody2D rb2d;

    void Start()
    {
        tuning = 0f;
        speed = 30;

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        LaserTransform = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(flagx,flagy,0) * speed;
        if (flagx == 1 && flagy == 1)
            LaserTransform.Rotate(0, 0, 45f);
        else if (flagx == 1 && flagy == -1) LaserTransform.Rotate(0, 0, -45f);
        else if (flagx == -1 && flagy == 1) LaserTransform.Rotate(0, 0, 135f);
        else LaserTransform.Rotate(0, 0, -135f);
        startSpeed = rb2d.velocity;
    }

    private void FixedUpdate()
    {
        float x = Mathf.Lerp(transform.position.x, playerTransform.position.x, tuning);
        float y = Mathf.Lerp(transform.position.y, playerTransform.position.y, tuning);
        transform.position = new Vector3(x, y, 0.0f);
        rb2d.velocity = rb2d.velocity;
        if (Mathf.Abs(transform.position.x - playerTransform.position.x) >20f)
        {
            Destroy(gameObject);
        }
    }
}
