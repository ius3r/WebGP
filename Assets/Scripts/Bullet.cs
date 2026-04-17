using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            // Raise the event for hitting an enemy (2f)
            EventChannelManager.Instance.floatEvent.RaiseEvent(2f);
            //Destroy(gameObject);
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }
    private void OnEnable()
    {
        timer = 0;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.5f)
        {
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }
}
