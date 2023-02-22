using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDefense.EnemyWaves
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField, Range(0f, 5f)] private float _waveBeginWaitTime = 3f;
        [SerializeField] private float _buildTimeSpan = 5f;
        [SerializeField] private GameEvent OnBuildTimeBegin;
        [SerializeField] private GameEvent OnBuildTimeEnd;
        [SerializeField] private GameEvent OnGameVictory;
        [SerializeField] private Vector3Value spawnTilePosition;
        [SerializeField] private Vector3Value homeTilePosition;
        [SerializeField] private Timer _buildTimer;
        [SerializeField] private Timer _spawnTimer;
        [SerializeField] private TimerUI _buildTimerUI;
        [SerializeField] private TMP_Text _waveNumberUI;
        [SerializeField] private List<Wave> _waves;

        private Wave _currentWave;
        private int _currentWaveIndex = 0;
        private bool _waveIsOngoing = false;
        private int _waveEnemyCount = 0;

        private void Start()
        {
            _buildTimer.OnTimerElapsed += OnBuildTimeEnd.Raise;
            _buildTimer.OnTimerElapsed += StartNextWave;
            _spawnTimer.OnTimerElapsed += Spawn;
        }

        private void Update()
        {
            if (_buildTimer.Running)
                _buildTimerUI.UpdateTime(_buildTimer.ElapsedTime);

            if (Input.GetKeyDown(KeyCode.Keypad0))
                SetUp();
        }

        public void SetUp()
        {
            if (_waves == null || _waves.Count == 0) {
                Debug.LogWarning("There is no waves setup for the wave manager to process. Please assign a wave first!");
                return;
            }

            Debug.Log("Wave Manager has started.");

            foreach (var wave in _waves)
                wave.InitializeWave();

            _currentWaveIndex = -1;
            _buildTimer.Begin(_buildTimeSpan);
            OnBuildTimeBegin.Raise();
        }

        /// <summary>
        /// Starts the next wave.
        /// </summary>
        private void StartNextWave()
        {
            if (_currentWaveIndex >= _waves.Count - 1) return;

            _currentWaveIndex++;
            _currentWave = _waves[_currentWaveIndex];
            _waveIsOngoing = true;
            _waveEnemyCount = _currentWave.TotalCountOfEnemies;
            _waveNumberUI.text = $"Wave {_currentWaveIndex + 1}";
            _spawnTimer.Begin(_waveBeginWaitTime);
        }

        /// <summary>
        /// Spawns next enemy in queue.
        /// </summary>
        private void Spawn()
        {
            if (_currentWave != null && !_currentWave.HasSpawnedAllEnemies) {
                _currentWave.Spawn(spawnTilePosition.runtimeValue, homeTilePosition.runtimeValue);
                if (!_currentWave.HasSpawnedAllEnemies) {
                    _spawnTimer.Begin(_currentWave.SpawnDelay);
                }
            }
        }

        public void LowerEnemyCount()
        {
            _waveEnemyCount--;

            if (_waveIsOngoing && _currentWave.HasSpawnedAllEnemies && _waveEnemyCount == 0) {

                if (_currentWaveIndex == _waves.Count - 1)
                    OnGameVictory.Raise();
                else {
                    OnBuildTimeBegin.Raise();
                    _buildTimer.Begin(_buildTimeSpan);
                }

                _waveIsOngoing = false;
            }
        }

        private void OnValidate() => _buildTimeSpan = Mathf.Clamp(_buildTimeSpan, 0f, _buildTimeSpan);
    }
}