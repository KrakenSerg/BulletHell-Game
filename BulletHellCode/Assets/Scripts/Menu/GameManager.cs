using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Menu
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int maxHP = 3;
        private int currentHP;
        private float gameStartTime;
        private List<float> hitTimes = new List<float>();
        private bool gameOver = false;

        public EndGamePanelUI endGameUI; // reference to the microservice

        void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            currentHP = maxHP;
            gameStartTime = Time.time;
        }

        public void RegisterHit()
        {
            if (gameOver) return;

            float timeSinceStart = Time.time - gameStartTime;
            hitTimes.Add(timeSinceStart);
            currentHP--;

            if (currentHP <= 0)
            {
                EndGame();
            }
        }

        public HUDUI hudUI;
        void EndGame()
        {
            Debug.Log("EndGame() called");
            gameOver = true;
            
            if (hudUI != null)
                hudUI.StopTimer();
            
            float totalTime = Time.time - gameStartTime;
            if (endGameUI != null)
            {
                endGameUI.ShowPanel(totalTime, hitTimes);
            }
            else
            {
                Debug.LogError("EndGamePanelUI is not assigned!");
            }
        }
    }
}