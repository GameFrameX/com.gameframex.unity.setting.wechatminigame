namespace GameFrameX.Setting.Runtime
{
    /// <summary>
    /// 微信小游戏存档文件存储后端。基于微信 FileSystemManager 同步接口：用户目录 <c>wx.env.USER_DATA_PATH</c>，两阶段原子写经 WriteFileSync + CopyFileSync + UnlinkSync。
    /// </summary>
    internal sealed class WeChatMiniGameSaveSlotFileStorage : MiniGameSaveSlotFileStorageBackendBase
    {
        private WeChatWASM.WXFileSystemManager m_FileSystemManager;

        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SaveSlotFileStorageBackendFactory.RegisterChannelBackend(() => new WeChatMiniGameSaveSlotFileStorage());
        }

        private WeChatWASM.WXFileSystemManager GetFileSystemManager()
        {
            if (m_FileSystemManager == null)
            {
                m_FileSystemManager = WeChatWASM.WXBase.GetFileSystemManager();
            }

            return m_FileSystemManager;
        }

        protected override string GetPlatformUserDataPath()
        {
            return WeChatWASM.WXBase.env.USER_DATA_PATH;
        }

        protected override string CombinePath(string left, string right)
        {
            return left + "/" + right;
        }

        protected override void WriteFileBytes(string filePath, byte[] contents)
        {
            GetFileSystemManager().WriteFileSync(filePath, contents);
        }

        protected override byte[] ReadFileBytes(string filePath)
        {
            return GetFileSystemManager().ReadFileSync(filePath);
        }

        protected override void CopyFileBytes(string sourceFilePath, string destinationFilePath)
        {
            GetFileSystemManager().CopyFileSync(sourceFilePath, destinationFilePath);
        }

        protected override void DeleteFile(string filePath)
        {
            GetFileSystemManager().UnlinkSync(filePath);
        }

        protected override bool ExistsFile(string filePath)
        {
            return GetFileSystemManager().AccessSync(filePath).Equals("access:ok");
        }

        protected override void CreateDirectoryIfExistsMissing(string directoryPath)
        {
            if (GetFileSystemManager().AccessSync(directoryPath).Equals("access:ok"))
            {
                return;
            }

            GetFileSystemManager().MkdirSync(directoryPath, true);
        }

        protected override string[] ListDirectoryEntryNames(string directoryPath)
        {
            return GetFileSystemManager().ReaddirSync(directoryPath);
        }
    }
}
