
using System.Drawing;
using System.Drawing.Imaging;

internal class Program
{
    private static string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
    private static Dictionary<string, string> dictImagenes = new Dictionary<string, string>();

    private static void Main(string[] args)
    {
        double megaPixels = 2d;
        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory);

        foreach (var f in files) {
            FileInfo fi = new System.IO.FileInfo(f);

            if (imageExtensions.Contains(fi.Extension)) {
                resizeImage(fi, megaPixels);
            }            
        }

        foreach (var key in dictImagenes.Keys) {
            DeleteFile(key);
            File.Move(dictImagenes[key], key);            
        }

    }

    private static void resizeImage(FileInfo fi, double maxPixels) 
    {
        Bitmap bmp = new Bitmap(fi.FullName);
        double actualMegaPixels = (bmp.Height * bmp.Width) / Math.Pow(10, 6);

        // Resize if exceeds the maximum
        if (maxPixels < actualMegaPixels) {

            // Calculate the resize  factor
            double factor = Math.Sqrt(actualMegaPixels) / Math.Sqrt(maxPixels);
            Size newSize = new Size((int)(bmp.Width / factor), (int)(bmp.Height / factor));

            // Generate new rescaled bitmap
            Bitmap rescaled = new Bitmap(newSize.Width, newSize.Height, bmp.PixelFormat);
            using (Graphics g = Graphics.FromImage(rescaled))
            {
                string newPath = fi.FullName.Replace(fi.Name, "_" + fi.Name);
                g.DrawImage(bmp, 0, 0, newSize.Width, newSize.Height);
                rescaled.Save(newPath, bmp.RawFormat);
                
                // Save diccionary with new and old files path
                dictImagenes.Add(fi.FullName, newPath);
                g.Dispose();                
            }

            rescaled.Dispose();
            bmp.Dispose();
        }
    }

    public static void DeleteFile(String fileToDelete)
    {
        var fi = new System.IO.FileInfo(fileToDelete);
        if (fi.Exists)
        {
            fi.Delete();
            fi.Refresh();
            while (fi.Exists)
            {
                System.Threading.Thread.Sleep(100);
                fi.Refresh();
            }
        }
    }
}