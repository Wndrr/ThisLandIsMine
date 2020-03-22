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
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.forward * 1000); //Moving projectile
            _rigidbody.AddTorque(transform.forward * 3);
        }

        // Update is called once per frame
        void Update()
        {
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