namespace MSFackTarget
{
    public class FileWrapper
    {
        private readonly FileHelper m_FileHelper;

        public FileWrapper(FileHelper fileHelper)
        {
            m_FileHelper = fileHelper;
        }


        public bool IsFileExist(string fileName)
        {
            return !m_FileHelper.IsNull(fileName);
        }

        public bool IsFileEmpty(string fileName)
        {
            return m_FileHelper.IsEmpty(fileName);
        }

        public bool IsFileNullOrEmpty(string fileName)
        {
            return FileHelper.IsNullOrEmpty(fileName);
        }
    }
}