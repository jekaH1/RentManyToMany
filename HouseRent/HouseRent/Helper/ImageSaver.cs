namespace HouseRent.Helper
{
    public static class ImageSaver
    {

        public static string SaveFile(this IFormFile file,string rootpaht,string foldername)
        {
            string filename = file.FileName;
            filename = filename.Length > 64 ? filename.Substring(filename.Length - 64, 64) : filename;
            filename = Guid.NewGuid().ToString() + filename;

            string path = Path.Combine(rootpaht, foldername, filename);
            using(FileStream fileStream=new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return filename;
        }
    }
}
