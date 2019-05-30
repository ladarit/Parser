using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace GovernmentParse.Helpers
{
    public static class FontProvider
    {
        public static PrivateFontCollection Pfc;

        static FontProvider()
        {
            Pfc = new PrivateFontCollection();
            InitCustomLabelFont();
        }

        private static void InitCustomLabelFont()
        {
            int fontLength = Properties.Resources.Roboto_Regular.Length;

            byte[] fontdata = Properties.Resources.Roboto_Regular;

            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            Marshal.Copy(fontdata, 0, data, fontLength);

            Pfc.AddMemoryFont(data, fontLength);
        }

        public static Font SetRobotoFont(float fontSize, FontStyle style)
        {
            return new Font(Pfc.Families[0], fontSize, style);
        }
    }
}
