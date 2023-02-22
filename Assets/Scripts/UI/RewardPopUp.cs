using TMPro;
using UnityEngine;

namespace TowerDefense
{
    public class RewardPopUp : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private TMP_Text _popUpText;
        [SerializeField] private float _slideUpAmount;
        [SerializeField] private float _slideSpeed;

        private Vector3 _popUpCachedPosition;
        private bool _slide = false;

        private void Update()
        {
            if (_slide)
                SlideUpward();
        }

        public void Pop(int rewardAmount, Vector3 position)
        {
            _popUpText.text = $"+{rewardAmount}";
            _transform.position = _popUpCachedPosition = Camera.main.WorldToScreenPoint(position);
            _slide = true;
            gameObject.SetActive(true);
        }

        private void SlideUpward()
        {
            if (_transform.position.y < _popUpCachedPosition.y + _slideUpAmount)
                _transform.position = new Vector3(_transform.position.x, _transform.position.y + Time.deltaTime * _slideSpeed * _slideUpAmount);
            else
                Destroy(gameObject);
        }
    }
}
