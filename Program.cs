
using System.Drawing;

internal class Program
{
    private static string[] imageExtensions = new string[] { "jpg", "jpeg", "png", "gif", "bmp" };

    private static void Main(string[] args)
    {
        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);
        string[] imageFiles;

        foreach (var f in files) {
            FileInfo fi = new System.IO.FileInfo(f);

            if (imageExtensions.Contains(fi.Extension)) {
                resizeImage(f);
            }
        }
    }

    protected static void resizeImage(string path) {
        Bitmap bmp = new Bitmap(path);

        bmp.SetResolution(20, 20);

        //File.WriteAllBytes(path + ".temp", bmp);
    }
}