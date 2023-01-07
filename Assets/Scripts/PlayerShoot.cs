using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private float _shotsPerSecond = 1f;
    [SerializeField]
    private Transform _shotPos;
    private float _shotTimer;

    private bool _shouldShoot = false;

    public void FixedUpdate()
    {
        _shotTimer += Time.fixedDeltaTime;
        if (_shouldShoot && (1f / _shotsPerSecond) < _shotTimer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1 << 31))
            {
                var shootPosition = hit.point;
                shootPosition.y = transform.position.y;
                var shootDirection = (shootPosition - transform.position).normalized;
                var shootRotation = Quaternion.LookRotation(shootDirection);
                Instantiate(_projectile, _shotPos.position, shootRotation);
            }
            _shotTimer = 0;
        }

    }
    public void OnFire(InputValue value) => _shouldShoot = value.isPressed;
}

