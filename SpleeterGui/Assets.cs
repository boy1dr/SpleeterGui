using System.Drawing;
using System.IO;
using System.Reflection;

namespace SpleeterGui
{
    /// <summary>
    /// Images and default configs embedded in the exe as plain files (see the EmbeddedResource items in SpleeterGui.csproj).
    /// </summary>
    internal static class Assets
    {
        public static Image LoadImage(string name)
        {
            using (Stream s = Open(name))
            {
                if (s == null) return null;
                // Copy so the bitmap doesn't keep a reference to the (disposed) stream.
                using (Image img = Image.FromStream(s))
                {
                    return new Bitmap(img);
                }
            }
        }

        public static Icon LoadIcon(string name)
        {
            using (Stream s = Open(name))
            {
                return s == null ? null : new Icon(s);
            }
        }

        public static string LoadText(string name)
        {
            using (Stream s = Open(name))
            {
                if (s == null) return null;
                using (StreamReader reader = new StreamReader(s))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static Stream Open(string name)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("SpleeterGui." + name);
        }
    }
}
