namespace MyBiz.Helpers
{
    public static class FileManager
    {
        public static string Save(string root, string file, IFormFile formFile)
        {
            string fileName = Guid.NewGuid().ToString() + formFile.FileName;

            if (fileName.Length > 100)
            {
                fileName = Guid.NewGuid().ToString() + formFile.FileName.Substring(formFile.FileName.Length - 64, 64);
            }

            string path = Path.Combine(root, file, fileName);

            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            return fileName;
        }

        public static bool Delete(string root, string file, string imageUrl)
        {
            string path = Path.Combine(root, file, imageUrl);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
