using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private float projectileForce = 0f;
    private InputAction fire;

    private void Awake()
    {
        fire = InputSystem.actions.FindAction("Player/Attack");
    }
    private void OnEnable()
    {
        fire.started += Shoot;
    }
    private void OnDisable()
    {
        fire.started -= Shoot;
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        GameObject projectile = GameObject.Instantiate(bullet, projectileSpawn.position, projectileSpawn.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileForce, ForceMode.Impulse);
        Destroy(projectile, 1.5f);
    }
}