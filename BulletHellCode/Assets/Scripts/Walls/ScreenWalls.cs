using UnityEngine;

namespace Walls
{
    public class ScreenWalls : MonoBehaviour
    {
        public float wallThickness = 1f;
        public Sprite wallSprite;

        public Transform leftWall, rightWall, topWall, bottomWall; // expose for spawner access

        void Start()
        {
            CreateWalls();
        }

        void CreateWalls()
        {
            Camera cam = Camera.main;
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;
            Vector2 center = cam.transform.position;

            float left = (float)(center.x - camWidth / 1.9 + wallThickness / 2);
            float right = (float)(center.x + camWidth / 1.9 - wallThickness / 2);
            float top = (float)(center.y + camHeight / 1.9 - wallThickness / 2);
            float bottom = (float)(center.y - camHeight / 1.9 + wallThickness / 2);

            leftWall = CreateWall("Left Wall", new Vector2(left, center.y), new Vector2(wallThickness, camHeight)).transform;
            rightWall = CreateWall("Right Wall", new Vector2(right, center.y), new Vector2(wallThickness, camHeight)).transform;
            topWall = CreateWall("Top Wall", new Vector2(center.x, top), new Vector2(camWidth, wallThickness)).transform;
            bottomWall = CreateWall("Bottom Wall", new Vector2(center.x, bottom), new Vector2(camWidth, wallThickness)).transform;
        }

        GameObject CreateWall(string name, Vector2 position, Vector2 size)
        {
            GameObject wall = new GameObject(name);
            wall.transform.position = position;

            var sr = wall.AddComponent<SpriteRenderer>();
            sr.sprite = wallSprite;
            sr.color = Color.white;
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size = size;
            sr.sortingOrder = 10;

            var collider = wall.AddComponent<BoxCollider2D>();
            collider.size = size;
            collider.isTrigger = false;

            wall.tag = "Wall";
            var rb = wall.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;

            wall.transform.parent = this.transform;
            return wall;
        }
    }
}
