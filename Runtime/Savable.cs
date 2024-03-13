using System;

namespace HCG.Extensions
{
    public class Savable
    {
        private string _prefix;
        private Action _saveAction;
        private Action _saveToCacheAction;
        private Action _loadAction;
        private bool _isDirty;
        public bool IsDirty => _isDirty;
        
        public static T Load<T>(string prefix, bool forceNew = false) where T : Savable, new()
        {
            var value = new T();
            var loadSettings = SaveSettings.GetSettings(true);
            var loaded = ES3.KeyExists(prefix, loadSettings);
            var isNonFirstLoad = loaded && value.PreLoadCheck() && !forceNew;
            if (isNonFirstLoad)
            {
                value = ES3.Load<T>(prefix, loadSettings);
            }
            value._prefix = prefix;
            value._saveAction = () =>
            {
                value.OnPreSave();
                ES3.Save(value._prefix, value, settings: SaveSettings.GetSettings(false));
                value.OnSave();
            };
            value._saveToCacheAction = () =>
            {
                value.OnPreSave();
                ES3.Save(value._prefix, value, settings: SaveSettings.GetSettings(true));
                value.OnSave();
            };
            value._loadAction = () =>
            {
                if (isNonFirstLoad)
                {
                    value.OnNonFirstLoad();
                }
                else
                {
                    value.OnFirstLoad();
                }
                value.OnEveryLoad();
            };
            return value;
        }


        public void SetDirty()
        {
            _isDirty = true;
            //Debug.LogError(GetType().Name + " has Set Dirty");
        }

        protected virtual void OnPreSave()
        {
        }

        public void Save(bool cached = false)
        {
            _isDirty = false;
            if (cached)
            {
                _saveToCacheAction.Invoke();
            }
            else
            {
                _saveAction.Invoke();
            }
            // Debug.LogError(GetType().Name + " has Saved");
        }

        public void CallLoadAction()
        {
            _loadAction?.Invoke();
        }

        protected virtual bool PreLoadCheck()
        {
            return true;
        }

        protected virtual void OnFirstLoad()
        {
        }

        protected virtual void OnNonFirstLoad()
        {
        }

        protected virtual void OnEveryLoad()
        {
            LoadRemoteData();
        }

        protected virtual void LoadRemoteData()
        {
        }

        protected virtual void OnSave()
        {
        }
    }
}