using UnityEngine;
using Random = System.Random;

namespace Controllers.Mobs
{
    [RequireComponent(typeof(CharacterController))]
    public class MobAi : MonoBehaviour
    {
        private CharacterController _controller;
        private Vector3 _targetPosition;
        private bool _isTargetPositionReached;

        public int speed = 6;
        public int targetPositionMaxDistance = 20;
        // Start is called before the first frame update
        private void Start()
        {
            var random = new Random();
            _controller = GetComponent<CharacterController>();
            InvokeRepeating(nameof(ChoseNewTargetPosition), 1f, random.Next(3, 15));
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_isTargetPositionReached)
                Move();
        }
        private void Move()
        {
            if (_targetPosition.x == transform.position.x && _targetPosition.z == transform.position.z)
            {
                _isTargetPositionReached = true;
            }

            var movementOffset = _targetPosition - transform.position;
        
            _controller.SimpleMove(movementOffset.normalized * speed);
        }

        private void ChoseNewTargetPosition()
        {
            var rand = new Random();

            var xOffset = rand.Next(-targetPositionMaxDistance, targetPositionMaxDistance);
            var zOffset = rand.Next(-targetPositionMaxDistance, targetPositionMaxDistance);

            var targetPosition = transform.position;
            targetPosition.x += xOffset;
            targetPosition.z += zOffset;
            targetPosition.y = 0;

            _targetPosition = targetPosition;
            _isTargetPositionReached = false;
        }
    }
}
