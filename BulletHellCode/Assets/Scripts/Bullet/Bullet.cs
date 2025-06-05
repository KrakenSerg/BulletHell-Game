using UnityEngine;

namespace  Bullet
{
    public class Bullet : MonoBehaviour
    {
        public Vector2 velocity;
        public float speed;
        public float rotation;
        void Start()
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            Destroy(gameObject, 6f); // fallback safety
        }
        void Update()
        {
            transform.Translate(velocity * (speed * Time.deltaTime));
        }
    }
}

