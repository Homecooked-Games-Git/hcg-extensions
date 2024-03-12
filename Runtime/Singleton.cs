using UnityEngine;

namespace HCG.Extensions
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [SerializeField]
        #if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 25)] 
        #endif
        private bool dontDestroyOnLoad;
        
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Initialize();
        }
        protected virtual void Initialize() { }
    }
}
