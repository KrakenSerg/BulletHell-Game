using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDUI : MonoBehaviour
    {
        public TextMeshProUGUI runTimeText;
        public TextMeshProUGUI hpText;

        private float runTime;
        private bool isRunning = true;
        private int currentHp = 3;
        
        void Update()
        {
            if (!isRunning) return;

            runTime += Time.deltaTime;
            runTimeText.text = $"Time: {runTime:F2} s";
            hpText.text = $"HP: {currentHp}";
        }

        public void SetHP(int hp)
        {
            currentHp = hp;
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        public float GetFinalTime() => runTime;
    }
}
