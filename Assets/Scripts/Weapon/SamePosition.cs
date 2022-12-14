using UnityEngine;

public class SamePosition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float speed;

    void Start()
    {
        speed = 120;
        transform.position = player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
        transform.position = player.transform.position;
    }
}
