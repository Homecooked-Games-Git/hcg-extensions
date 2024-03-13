namespace HCG.Extensions
{
    public static class SaveSettings
    {
        private const string SaveVersion = "v1";
        private const string SavePath = "default_save_" + SaveVersion + ".es3";
        
        public static ES3Settings GetSettings(bool cache){
            return
                new ES3Settings(){
                    path = SavePath,
                    location = cache ? ES3.Location.Cache : ES3.Location.File,
                    encryptionType = ES3.EncryptionType.None,
                    compressionType = ES3.CompressionType.None,
                    directory = ES3.Directory.PersistentDataPath,
                    format = ES3.Format.JSON,
                    prettyPrint = true,
                    bufferSize = 2048,
                    encoding = System.Text.Encoding.UTF8,
                    postprocessRawCachedData = false,
                    typeChecking = true,
                    safeReflection = true,
                    memberReferenceMode = ES3.ReferenceMode.ByRef,
                    referenceMode = ES3.ReferenceMode.ByRefAndValue,
                    serializationDepthLimit = 64,
                    assemblyNames = new []{ "Assembly-CSharp-firstpass", "Assembly-CSharp" }
                };
        }
    }
}
