using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace KeyAuth_Wallpaper_Engine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string url = "https://cdn.concreteplayground.com/content/uploads/2015/12/Wildlife-Australia-quokkas-1920x1080.jpeg";
            //Wallpaper.Set(url, Wallpaper.Style.Fill);
            //Wallpaper.Set("", Wallpaper.Style.Fill);
        }

        private void quokka1_Click(object sender, EventArgs e)
        {
            Wallpaper.Set("https://wallpaperaccess.com/full/4819398.jpg", Wallpaper.Style.Fill);
        }

        private void quokka2_Click(object sender, EventArgs e)
        {
            Wallpaper.Set("https://cutewallpaper.org/21/quokka-wallpapers/Quokka-Animal-Facts-with-Wallpapers.jpg", Wallpaper.Style.Fill);
        }

        private void quokka3_Click(object sender, EventArgs e)
        {
            Wallpaper.Set("https://images.wallpaperscraft.com/image/single/quokka_cute_food_126646_2560x1440.jpg", Wallpaper.Style.Fill);
        }

        private void quokka4_Click(object sender, EventArgs e)
        {
            Wallpaper.Set("https://animals.sandiegozoo.org/sites/default/files/2020-04/hero-quokka.jpg", Wallpaper.Style.Fill);
        }

        private void quokka5_Click(object sender, EventArgs e)
        {
            Wallpaper.Set("https://cdn.i-scmp.com/sites/default/files/styles/1920x1080/public/d8/images/methode/2019/03/29/9d3e16cc-4bbf-11e9-8e02-95b31fc3f54a_image_hires_175715.jpg", Wallpaper.Style.Fill);
        }

        private void quokka6_Click(object sender, EventArgs e)
        {
            Wallpaper.Set("https://cdn.concreteplayground.com/content/uploads/2015/12/Wildlife-Australia-quokkas-1920x1080.jpeg", Wallpaper.Style.Fill);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }

    public sealed class Wallpaper
    {
        //Credits to http://eddiejackson.net/wp/?p=21967
        Wallpaper() { }

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Fill,
            Fit,
            Span,
            Stretch,
            Tile,
            Center
        }

        public static void Set(string url, Style style)
        {
            System.IO.Stream streamedimage = new System.Net.WebClient().OpenRead(url);

            System.Drawing.Image img = System.Drawing.Image.FromStream(streamedimage);
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (style == Style.Fill)
            {
                key.SetValue(@"WallpaperStyle", 10.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }
            if (style == Style.Fit)
            {
                key.SetValue(@"WallpaperStyle", 6.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }
            if (style == Style.Span)
            {
                key.SetValue(@"WallpaperStyle", 22.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }
            if (style == Style.Stretch)
            {
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }
            if (style == Style.Tile)
            {
                key.SetValue(@"WallpaperStyle", 0.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
            }
            if (style == Style.Center)
            {
                key.SetValue(@"WallpaperStyle", 0.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
            }

            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                tempPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
