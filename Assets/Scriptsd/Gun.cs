using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    [SerializeField] private InputAction shootAction;

    private void OnEnable()
    {
        shootAction.Enable();
    }

    private void OnDisable()
    {
        shootAction.Disable(); // this is not ai

    }

    private void Update()
    {
        if (shootAction.WasPressedThisFrame())
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
        }
    }
}