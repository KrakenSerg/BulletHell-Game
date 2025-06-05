using System.Collections;
using UnityEngine;

namespace Bullet
{
    public class BulletSpawnerManager : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Sprite spawnerSprite;
        public float initialMinCooldown = 2f;
        public float initialMaxCooldown = 4f;
        public float fireRateIncrease = 0.05f;

        public Walls.ScreenWalls screenWalls;

        private Vector2 screenCenter;

        void Start()
        {
            screenCenter = Camera.main.transform.position;
            StartCoroutine(WaitAndInit());
        }

        IEnumerator WaitAndInit()
        {
            yield return new WaitForEndOfFrame(); // Wait for walls
            InitSpawners();
        }

        void InitSpawners()
        {
            if (!screenWalls.leftWall) return;

            SpawnOnWall(screenWalls.leftWall, Vector2.right);
            SpawnOnWall(screenWalls.rightWall, Vector2.left);
            SpawnOnWall(screenWalls.topWall, Vector2.down);
            SpawnOnWall(screenWalls.bottomWall, Vector2.up);
        }

        void SpawnOnWall(Transform wall, Vector2 direction)
        {
            var positions = GetSpawnerPositions(wall, direction);
            foreach (var pos in positions)
            {
                var spawner = new GameObject("BulletSpawner");
                spawner.transform.position = pos;

                if (spawnerSprite)
                {
                    var sr = spawner.AddComponent<SpriteRenderer>();
                    sr.sprite = spawnerSprite;
                    sr.color = Color.red;
                    sr.sortingOrder = 15;
                }

                StartCoroutine(ShootCoroutine(spawner.transform));
            }
        }

        Vector2[] GetSpawnerPositions(Transform wall, Vector2 inward)
        {
            var col = wall.GetComponent<BoxCollider2D>();
            Vector2 size = col.size;
            Vector2 pos = wall.position;

            bool vertical = Mathf.Abs(inward.x) > 0;
            float span = vertical ? size.y : size.x;
            float offset = span / 4;

            return new Vector2[]
            {
                pos + (vertical ? new Vector2(0, -offset) : new Vector2(-offset, 0)) + inward * 0.8f,
                pos + (vertical ? new Vector2(0, offset)  : new Vector2(offset, 0))  + inward * 0.8f
            };
        }

        IEnumerator ShootCoroutine(Transform spawner)
        {
            float min = initialMinCooldown;
            float max = initialMaxCooldown;

            while (true)
            {
                if (!bulletPrefab) yield break;

                Vector2 pos = spawner.position;
                Vector2 toCenter = (screenCenter - pos).normalized;
                float angle = Mathf.Atan2(toCenter.y, toCenter.x) * Mathf.Rad2Deg + Random.Range(-90f, 90f);
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                var bullet = Instantiate(bulletPrefab, pos, Quaternion.Euler(0, 0, angle));
                var b = bullet.GetComponent<Bullet>();
                b.velocity = dir;
                b.speed = Mathf.Max(5f, b.speed);

                float wait = Random.Range(min, max);
                yield return new WaitForSeconds(wait);

                min = Mathf.Max(0.2f, min - fireRateIncrease * wait);
                max = Mathf.Max(min + 0.1f, max - fireRateIncrease * wait);
            }
        }
    }
}