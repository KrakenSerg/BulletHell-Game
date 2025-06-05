using UnityEngine;

namespace Bullet
{
    public class BulletSpawner : MonoBehaviour
    {
        [Header("Setup")]
        public GameObject bulletPrefab;
        public int numberOfBullets = 5;
        public bool isRandom = false;
        public float minRotation = 0f;
        public float maxRotation = 360f;

        [Header("Bullet Settings")]
        public float bulletSpeed = 5f;
        public Vector2 bulletVelocity = Vector2.zero;

        [Header("Cooldown")]
        public float cooldown = 1f;
        private float timer;

        void Start() => timer = cooldown;

        void Update()
        {
            timer -= Time.deltaTime;
            if (timer > 0) return;

            float[] rotations = BulletSpawnerUtility.GenerateRotations(numberOfBullets, minRotation, maxRotation, isRandom);
            BulletSpawnerUtility.SpawnBullets(bulletPrefab, transform.position, rotations, bulletSpeed, bulletVelocity);
            timer = cooldown;
        }
    }
}