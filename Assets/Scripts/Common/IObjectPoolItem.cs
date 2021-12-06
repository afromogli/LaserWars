using UnityEngine;

namespace Assets.Scripts.Common
{
    public interface IObjectPoolItem
    {
        public GameObject GameObject { get; set; }

        public void Disable();
    }
}
