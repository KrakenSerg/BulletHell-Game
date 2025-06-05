using UnityEngine;
using UI; 

namespace Player{
    public class Player : MonoBehaviour
    {
        public int startHp;
        int hp;
        public float bulletCooldown;
        float bulletTimer;
        
        public HUDUI hudUI; 
        
        void Start()
        {
            hp = startHp;
        }
        void Update()
        {
            bulletTimer -= Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Bullet" && bulletTimer <= 0)
            {
                hp -= 1;
                print(hp);
                bulletTimer = bulletCooldown;
                
                hudUI.SetHP(hp);
                Menu.GameManager.instance.RegisterHit();
            }
        }
    }
}
