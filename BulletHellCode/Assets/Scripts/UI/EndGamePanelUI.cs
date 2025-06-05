using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndGamePanelUI : MonoBehaviour
    {
        public GameObject panelRoot;
        public TextMeshProUGUI totalTimeText;
        public TextMeshProUGUI resultText;
        public TextMeshProUGUI hitTimesText;
        public Button restartButton;

        private void Awake()
        {
            panelRoot.SetActive(false);
            restartButton.onClick.AddListener(() => {
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene(
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                );
            });
        }

        public void ShowPanel(float totalTime, List<float> hitTimes)
        {
            panelRoot.SetActive(true);
            Time.timeScale = 0f;

            totalTimeText.text = $"Total Time: {totalTime:F2} seconds";

            // 💡 Add the evaluation logic
            string performance = "Try harder";
            if (totalTime >= 11f && totalTime <= 20f)
                performance = "Nice try";
            else if (totalTime >= 21f && totalTime <= 40f)
                performance = "Good job";
            else if (totalTime > 41f)
                performance = "You are pro";

            resultText.text = $"{performance}";

            hitTimesText.text = "Hit Times:\n";
            for (int i = 0; i < hitTimes.Count; i++)
            {
                hitTimesText.text += $"Hit {i + 1}: {hitTimes[i]:F2} s\n";
            }
        }
    }
}
