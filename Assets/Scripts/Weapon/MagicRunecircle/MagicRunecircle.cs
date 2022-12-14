using UnityEngine;

public class MagicRunecircle : MonoBehaviour
{
    [SerializeField] private float continueTime;
    public float damage;
    public float cdTime;

    private float totalTime;

    void Start()
    {
        totalTime = 0;
    }

    private void FixedUpdate()
    {
        totalTime += Time.deltaTime;
        if (totalTime >= continueTime)
        {
            totalTime = 0;
            gameObject.SetActive(false);
        }
    }
}
