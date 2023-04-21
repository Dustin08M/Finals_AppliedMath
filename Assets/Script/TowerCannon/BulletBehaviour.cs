using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Transform target;

    public float despawnTime = 3;
    public float speed = 70f;

    public void Start()
    {

    }
    public void chase(Transform _target)
    {
        target = _target;
    }

    

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if(direction.sqrMagnitude <= distance)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distance, Space.World);
    }

    void HitTarget()
    {
        Destroy(gameObject);
        target.gameObject.SetActive(false);
    }
}
