namespace GameFrameX.Setting.Runtime
{
    internal sealed class WeChatMiniGameSettingStorage : ISettingStorageBackend
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SettingStorageBackendFactory.RegisterMiniGameBackend(() => new WeChatMiniGameSettingStorage());
        }

        public bool Save()
        {
            return true;
        }

        public bool HasKey(string key)
        {
            return WeChatWASM.WXSDKManagerHandler.Instance.StorageHasKeySync(key);
        }

        public bool DeleteKey(string key)
        {
            if (!WeChatWASM.WXSDKManagerHandler.Instance.StorageHasKeySync(key))
            {
                return false;
            }

            WeChatWASM.WXSDKManagerHandler.Instance.StorageDeleteKeySync(key);
            return true;
        }

        public void DeleteAll()
        {
            WeChatWASM.WXSDKManagerHandler.Instance.StorageDeleteAllSync();
        }

        public int GetInt(string key, int defaultValue)
        {
            return WeChatWASM.WXSDKManagerHandler.Instance.StorageGetIntSync(key, defaultValue);
        }

        public void SetInt(string key, int value)
        {
            WeChatWASM.WXSDKManagerHandler.Instance.StorageSetIntSync(key, value);
        }

        public float GetFloat(string key, float defaultValue)
        {
            return WeChatWASM.WXSDKManagerHandler.Instance.StorageGetFloatSync(key, defaultValue);
        }

        public void SetFloat(string key, float value)
        {
            WeChatWASM.WXSDKManagerHandler.Instance.StorageSetFloatSync(key, value);
        }

        public string GetString(string key, string defaultValue)
        {
            return WeChatWASM.WXSDKManagerHandler.Instance.StorageGetStringSync(key, defaultValue);
        }

        public void SetString(string key, string value)
        {
            WeChatWASM.WXSDKManagerHandler.Instance.StorageSetStringSync(key, value);
        }
    }
}
