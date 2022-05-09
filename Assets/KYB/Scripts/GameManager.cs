    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        private bool _isPause;
        private void Update()
        {
            // todo : 멈춤 기능 되는지 확인
            if (Input.GetKeyDown(KeyCode.P))
            {
                _isPause = !_isPause;
                Time.timeScale = _isPause ? 0 : 1;
            }

            // todo : 리셋 기능 되는지 확인
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
                //Invoke("LoadPlayScene", 0.1f);
            }

        }

        private void LoadPlayScene()
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }