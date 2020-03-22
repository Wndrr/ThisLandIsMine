using Controllers.Mobs;
using UnityEngine;

namespace Controllers.Items
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Throwable : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            var forwardVector = transform.forward;
            _rigidbody.AddForce(forwardVector * 1000); //Moving projectile
            _rigidbody.AddTorque(forwardVector * 3);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Mob"))
            {
                var mob = other.gameObject.GetComponent<Mob>();
                mob.Hit();
                Destroy(gameObject);
            }
        }
    }
}