using UnityEngine;

namespace Bullet
{
    public static class BulletSpawnerUtility
    {
        public static float[] GenerateRotations(int count, float min, float max, bool isRandom)
        {
            float[] rotations = new float[count];

            if (isRandom)
            {
                for (int i = 0; i < count; i++)
                    rotations[i] = Random.Range(min, max);
            }
            else
            {
                float step = (max - min) / (count - 1);
                for (int i = 0; i < count; i++)
                    rotations[i] = min + step * i;
            }

            return rotations;
        }

        public static GameObject[] SpawnBullets(GameObject prefab, Vector2 position, float[] rotations, float speed, Vector2 velocity)
        {
            GameObject[] bullets = new GameObject[rotations.Length];

            for (int i = 0; i < rotations.Length; i++)
            {
                var bulletObj = Object.Instantiate(prefab, position, Quaternion.Euler(0, 0, rotations[i]));
                var bullet = bulletObj.GetComponent<Bullet>();
                bullet.rotation = rotations[i];
                bullet.speed = speed;
                bullet.velocity = velocity;

                bullets[i] = bulletObj;
            }

            return bullets;
        }
    }
}