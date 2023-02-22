using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class LifeCounter : MonoBehaviour
    {
        [SerializeField] private Image _lifeIconPrefab;

        private readonly Stack<Image> _lifeIcons = new();

        public void SetLifeCounter(int numberOfLives)
        {
            if (_lifeIcons.Count > 0) {
                while (_lifeIcons.Count > 0)
                    Destroy(_lifeIcons.Pop().gameObject);

                _lifeIcons.Clear();
            }

            for (int i = 0; i < numberOfLives; i++)
                _lifeIcons.Push(Instantiate(_lifeIconPrefab, transform));
        }

        public void RemoveLife()
        {
            if (_lifeIcons.Count > 0)
                Destroy(_lifeIcons.Pop().gameObject);
        }
    }
}
