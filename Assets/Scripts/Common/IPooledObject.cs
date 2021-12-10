using UnityEngine;

namespace Assets.Scripts.Common
{
    public interface IPooledObject
    {
        public GameObject GameObject { get; set; }

        public void Disable();
    }
}
