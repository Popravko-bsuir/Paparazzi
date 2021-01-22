using UnityEngine;

namespace Resources.Scripts.Player
{
    public class PlayerContolls : MonoBehaviour
    {
        [SerializeField] private MeshRenderer road;
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float horizontalSpeed;
        [SerializeField] private Rigidbody rb;
        private float _horizontalBounds;
        private Vector3 _previousPosition;
        void Start()
        {
            _horizontalBounds = road.bounds.size.x / 2;
        }

        void Update()
        {
            HorizontalMovement();
        }

        private void HorizontalMovement()
        {
            var horizontalDirection = Input.GetAxisRaw("Horizontal") * horizontalSpeed;
            rb.velocity = new Vector3(horizontalDirection, 0, forwardSpeed);
            var transformPosition = transform.position;
            if (transformPosition.x > _horizontalBounds || transformPosition.x < -_horizontalBounds)
            {
                transform.position = new Vector3(_previousPosition.x, transformPosition.y, transformPosition.z);
                return;
            }
            _previousPosition = transformPosition;
        }
    }
}
