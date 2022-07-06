﻿    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        private bool _isPause;
        public FixBar FixBar;
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

            if (Input.GetKeyDown(KeyCode.V))
            {
                ScoreSystem.Score += 10;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
            }
        }
    }