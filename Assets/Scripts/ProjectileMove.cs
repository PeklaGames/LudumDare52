using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    void FixedUpdate()
    {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer != 30)
        {
            var health = collider.gameObject.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.UpdateHealth(-GameStateManager.Instance.Damage.AttackDamage);
            }
            Destroy(gameObject);
        }
    }
}
