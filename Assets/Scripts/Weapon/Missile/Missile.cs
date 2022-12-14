using DG.Tweening;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float damage;
    public static float _damage;
    [SerializeField] private GameObject child;
    public float cdTime;

    private Vector2 _direction;
    private Animator animator;
    private Rigidbody2D rb;
    private float speed;

    private void Awake()
    {
        _damage = damage;
        speed = 100;
        animator = child.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        var enemy = LocateEnemy();
        if (enemy == null)
        {
            Destroy(gameObject);
            return ;
        }
        else
        {
             _direction = MoveDirection(enemy.transform);
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward,_direction);
    }

    private GameObject LocateEnemy()
    {
        var results = new Collider2D[10];
        Vector3 now = transform.position;
        if (now.x == 0.0) now.x = 0.001f;
        if (now.y == 0.0) now.y = 0.001f;
        Physics2D.OverlapCircleNonAlloc(now, 20 , results);
        foreach(var result in results)
        {
            if (result != null && result.CompareTag("Enemy"))
            {
                return result.gameObject;
            }
        }
        return null;
    }

    private Vector2 MoveDirection(Transform target)
    {
        var direction = new Vector2(1, 0);
        if(target != null)
        {
            Vector3 now = transform.position;
            if (now.x == 0.0) now.x = 0.01f;
            if (now.y == 0.0) now.y = 0.01f;
            direction = target.position - now;
            direction.Normalize();
        }
        if (direction.x== 0.0) direction.x= 0.001f;
        if (direction.y == 0.0) direction.y = 0.001f;

        return direction;   
    }

    private void FixedUpdate()
    {
        Vector3 now = transform.position;
        var targetPosition =(Vector2)now + _direction;
        var nows = targetPosition;
        if (targetPosition.x == 0.0) targetPosition.x = 0.1f;
        if (targetPosition.y == 0.0) targetPosition.y = 0.1f;
        if (speed == 0) speed = 1;
        rb.DOMove(targetPosition,speed).SetSpeedBased();
        Destroy(gameObject,4f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetFloat("running", 10f);
            child.GetComponent<PolygonCollider2D>().enabled = false;
            speed = 0;
            Destroy(gameObject, 0.2f);
        }
    }   
}
