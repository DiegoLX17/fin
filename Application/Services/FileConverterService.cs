using System.Drawing;

namespace Application;
public class FileConverterService
{
    public static string PlaceHolder = "data:image;base64,iVBORw0KGgoAAAANSUhEUgAAAGQAAABkCAYAAABw4pVUAAA...";

    public static string ConvertToBase64(Stream file, int w = 256)
    {
        if (file.Length > 0)
        {
            var ms = new MemoryStream();
            file.CopyTo(ms);
            ms = ResizeImage(ms, w);
            var fileBytes = ms.ToArray();
            return "data:image;base64," + Convert.ToBase64String(fileBytes);
        }
        else
        {
            throw new FileLoadException();
        }
    }
    public static MemoryStream ResizeImage(MemoryStream ms, int w)
    {
        var img = Image.FromStream(ms);
        int h = Convert.ToInt32(w * img.Height / img.Width);
        var imgN = img.GetThumbnailImage(w, h, null, IntPtr.Zero);
        var res = new MemoryStream();
        imgN.Save(res, img.RawFormat);
        return res;
    }
}