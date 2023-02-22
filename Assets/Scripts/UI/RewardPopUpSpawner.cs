using UnityEngine;

namespace TowerDefense
{
    public class RewardPopUpSpawner : MonoBehaviour
    {
        [SerializeField] private RewardPopUp _popUpPrefab;

        public void SpawnPopUp(int rewardAmount, Vector3 position)
        {
            RewardPopUp popUp = Instantiate(_popUpPrefab, transform);
            popUp.Pop(rewardAmount, position);
        }
    }
}
