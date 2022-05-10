    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        private bool _isPause;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _isPause = !_isPause;
                Time.timeScale = _isPause ? 0 : 1;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }