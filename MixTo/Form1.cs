using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixTo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        int hMax;

        private void DoTheThing2()
        {
            openFileDialog1.ShowDialog();
            Bitmap target = (Bitmap)Image.FromFile(openFileDialog1.FileName);
            openFileDialog1.ShowDialog();
            Bitmap source1 = ResizeImage(Image.FromFile(openFileDialog1.FileName), target.Width, target.Height);
            openFileDialog1.ShowDialog();
            Bitmap source2 = ResizeImage(Image.FromFile(openFileDialog1.FileName), target.Width, target.Height);
            hMax = target.Height;

            time = DateTime.Now;


            Bitmap final = new Bitmap(target.Width,target.Height);
            int w = target.Width;
            int h = target.Height;

            Rectangle cropRect = new Rectangle(0, 0, w / 4, h);

            Bitmap bit11 = new Bitmap(w / 4, h) ;
            using (Graphics g = Graphics.FromImage(bit11))
            {
                g.DrawImage(source1, new Rectangle(0, 0, bit11.Width, bit11.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            Bitmap bit12 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(bit12))
            {
                g.DrawImage(source2, new Rectangle(0, 0, bit12.Width, bit12.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }


            cropRect = new Rectangle(w / 4, 0, w / 4, h);
            Bitmap bit21 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(bit21))
            {
                g.DrawImage(source1, new Rectangle(0, 0, bit21.Width, bit21.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            Bitmap bit22 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(bit22))
            {
                g.DrawImage(source2, new Rectangle(0, 0, bit22.Width, bit22.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }


            cropRect = new Rectangle(w / 2, 0, w / 4, h);
            Bitmap bit31 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(bit31))
            {
                g.DrawImage(source1, new Rectangle(0, 0, bit31.Width, bit31.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            Bitmap bit32 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(bit32))
            {
                g.DrawImage(source2, new Rectangle(0, 0, bit32.Width, bit32.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }


            cropRect = new Rectangle(w * 3 / 4, 0, w / 4, h);
            Bitmap bit41 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(bit41))
            {
                g.DrawImage(source1, new Rectangle(0, 0, bit41.Width, bit41.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            Bitmap bit42 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(bit42))
            {
                g.DrawImage(source2, new Rectangle(0, 0, bit42.Width, bit42.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }



           



            Bitmap tmp1 = null;
            Bitmap tmp2 = null;
            Bitmap tmp3 = null;
            Bitmap tmp4 = null;
            cropRect = new Rectangle(0, 0, w / 4, h);
            Bitmap target1 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(target1))
            {
                g.DrawImage(target, new Rectangle(0, 0, target1.Width, target1.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            cropRect = new Rectangle(w / 4, 0, w / 4, h);
            Bitmap target2 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(target2))
            {
                g.DrawImage(target, new Rectangle(0, 0, target2.Width, target2.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            cropRect = new Rectangle(w / 2, 0, w / 4, h);
            Bitmap target3 =  new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(target3))
            {
                g.DrawImage(target, new Rectangle(0, 0, target3.Width, target3.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            cropRect = new Rectangle(w * 3 / 4, 0, w / 4, h);
            Bitmap target4 = new Bitmap(w / 4, h);
            using (Graphics g = Graphics.FromImage(target4))
            {
                g.DrawImage(target, new Rectangle(0, 0, target4.Width, target4.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }



            Thread t1 = new Thread(() =>
            {
                 tmp1 = calc(0,target, bit11, bit12);
            });
            t1.Start();
            Thread.Sleep(100);
            Thread t2 = new Thread(() =>
            {
                 tmp2 = calc(1,target2, bit21, bit22);
            });
            t2.Start();


            Thread.Sleep(100);
            Thread t3 = new Thread(() =>
            {
                tmp3 = calc(2, target3, bit31, bit32);
            });
            t3.Start();

            //Thread.Sleep(100);
            //Thread t4 = new Thread(() =>
            //{
                tmp4 = calcWithProgress(3, target4, bit41, bit42);
            //});
            //t4.Start();


            t1.Join();
            t2.Join();
            t3.Join();
      //      t4.Join();

            cropRect = new Rectangle(0, 0, w / 4, h);
            using (Graphics g = Graphics.FromImage(final))
            {
                g.DrawImage(tmp1, new Rectangle(0, 0, bit11.Width+1, bit11.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            cropRect = new Rectangle(0, 0, bit11.Width, bit11.Height);
            using (Graphics g = Graphics.FromImage(final))
            {
                g.DrawImage(tmp2, new Rectangle(w / 4, 0, w / 4 + 1, h),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            cropRect = new Rectangle(0, 0, bit11.Width, bit11.Height);
            using (Graphics g = Graphics.FromImage(final))
            {
                g.DrawImage(tmp3, new Rectangle(w /2, 0, w / 4 + 1, h),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            cropRect = new Rectangle(0, 0, bit11.Width, bit11.Height);
            using (Graphics g = Graphics.FromImage(final))
            {
                g.DrawImage(tmp4, new Rectangle(w * 3 / 4, 0, w / 4 + 1, h),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            //int ww, hw;
            //for (ww = 0; ww < target.Width; ww++)
            //    for (hw = 0; hw < target.Height; hw++)
            //    {
            //        //if(ww<w/4)
            //        //    final.SetPixel(ww, hw, tmp1.GetPixel(ww, hw));
            //        if (ww > w / 4 && ww < w / 2)
            //            final.SetPixel(ww, hw, tmp2.GetPixel(ww - (w / 4)-1, hw));
            //        if (ww > w / 2 && ww < w *3 / 4)
            //            final.SetPixel(ww, hw, tmp3.GetPixel(ww - 1 - (w / 4)*2, hw));
            //        if (ww > w * 3 / 4 && ww < w )
            //            final.SetPixel(ww, hw, tmp4.GetPixel(ww - 1 - (w / 4)*3, hw));
            //    }
                    

            pictureBox1.Image = final;
            final.Save("Mix" + new Random().Next().ToString() + ".png");
            return;
            
        //    final.Save("Mix" + new Random().Next().ToString() + ".png");
        }

        private void DoTheThing()
        {
            openFileDialog1.ShowDialog();
            Bitmap target = (Bitmap)Image.FromFile(openFileDialog1.FileName);
            openFileDialog1.ShowDialog();
            Bitmap bit1 = ResizeImage(Image.FromFile(openFileDialog1.FileName), target.Width, target.Height);
            openFileDialog1.ShowDialog();
            Bitmap bit2 = ResizeImage(Image.FromFile(openFileDialog1.FileName), target.Width, target.Height);
            hMax = target.Height;
            
            Bitmap final = new Bitmap(target.Width,target.Height);
            time = DateTime.Now;
            double progressMulti = 1000.0 / target.Width;
            //Thread t1 = new Thread(() =>
            //{

            //   Bitmap tmp = calc(0, target.Width / 4,target,bit1,bit2);
            //    int w, h;
            //    for (w = 0; w < target.Width / 4; w++)
            //        for (h = 0; h < hMax; h++)
            //            final.SetPixel(w, h, tmp.GetPixel(w, h));
            //});
            //t1.Start();
            //Thread t2 = new Thread(() =>
            //{
            //    Bitmap tmp = calc(target.Width / 4, target.Width / 2, target, bit1, bit2);
            //    int w, h;
            //    for (w = target.Width / 4; w < target.Width / 2; w++)
            //        for (h = 0; h < hMax; h++)
            //            final.SetPixel(w, h, tmp.GetPixel(w, h));

            //});
            //t2.Start();
            //Thread t3 = new Thread(() =>
            //{
            //    Bitmap tmp = calc(target.Width / 2, target.Width * 3 / 4, target, bit1, bit2);
            //    int w, h;
            //    for (w = target.Width / 2; w < target.Width * 3 / 4; w++)
            //        for (h = 0; h < hMax; h++)
            //            final.SetPixel(w, h, tmp.GetPixel(w, h));
            //});
            //t3.Start();
            //Thread t4 = new Thread(() =>
            //{
            //    Bitmap tmp = calc(target.Width * 3 / 4, target.Width, target, bit1, bit2);
            //    int w, h;
            //    for (w = target.Width * 3 / 4; w < target.Width ; w++)
            //        for (h = 0; h < hMax; h++)
            //            final.SetPixel(w, h, tmp.GetPixel(w, h));
            //});
            //t4.Start();
            //t1.Join();
            //t2.Join();
            //t3.Join();
            //t4.Join();


            int w, h;


            for (w = 0; w < target.Width; w++)
            {
                progressBar1.Value =(int) Math.Floor( w * progressMulti);
                for (h = 0; h < hMax; h++)
                {
                    int r, g, b;
                    if (Math.Abs(target.GetPixel(w, h).R - bit1.GetPixel(w, h).R) > Math.Abs(target.GetPixel(w, h).R - bit2.GetPixel(w, h).R))
                        r = bit2.GetPixel(w, h).R;
                    else
                        r = bit1.GetPixel(w, h).R;

                    if (Math.Abs(target.GetPixel(w, h).G - bit1.GetPixel(w, h).G) > Math.Abs(target.GetPixel(w, h).G - bit2.GetPixel(w, h).G))
                        g = bit2.GetPixel(w, h).G;
                    else
                        g = bit1.GetPixel(w, h).G;

                    if (Math.Abs(target.GetPixel(w, h).B - bit1.GetPixel(w, h).B) > Math.Abs(target.GetPixel(w, h).B - bit2.GetPixel(w, h).B))
                        b = bit2.GetPixel(w, h).B;
                    else
                        b = bit1.GetPixel(w, h).B;

                    final.SetPixel(w, h, Color.FromArgb(r, g, b));

                }
                if (checkBox1.Checked && w % 10 == 0)
                {
                    pictureBox1.Image = final;
                    this.Update();
                }
            }
            pictureBox1.Image = final;
            final.Save("Mix" + new Random().Next().ToString() + ".png");
        }

        DateTime time;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
         //   time = DateTime.Now;

            if (checkBox2.Checked)
                DoTheThing();
            else
                DoTheThing2();
            
           TimeSpan timeSpan =  DateTime.Now - time;
        //    MessageBox.Show(timeSpan.TotalSeconds.ToString());
            Console.WriteLine("runtime: " + timeSpan.TotalSeconds.ToString());
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        public Bitmap calc(int num, Bitmap target, Bitmap bit1, Bitmap bit2)
        {
            Bitmap retVal = new Bitmap(bit1.Width, hMax);
            int w, h;
    
            
            for (w = 0; w< bit1.Width; w++)
                for (h = 0; h<hMax; h++)
                {
                    int r, g, b;
                    if (Math.Abs(target.GetPixel(w, h).R - bit1.GetPixel(w, h).R) > Math.Abs(target.GetPixel(w , h).R - bit2.GetPixel(w, h).R))
                        r = bit2.GetPixel(w, h).R;
                    else
                        r = bit1.GetPixel(w, h).R;

                    if (Math.Abs(target.GetPixel(w , h).G - bit1.GetPixel(w, h).G) > Math.Abs(target.GetPixel(w , h).G - bit2.GetPixel(w, h).G))
                        g = bit2.GetPixel(w, h).G;
                    else
                        g = bit1.GetPixel(w, h).G;

                    if (Math.Abs(target.GetPixel(w , h).B - bit1.GetPixel(w, h).B) > Math.Abs(target.GetPixel(w , h).B - bit2.GetPixel(w, h).B))
                        b = bit2.GetPixel(w, h).B;
                    else
                        b = bit1.GetPixel(w, h).B;

                    retVal.SetPixel(w, h, Color.FromArgb(r, g, b));

                }
            return retVal;
        }
        public Bitmap calcWithProgress(int num, Bitmap target, Bitmap bit1, Bitmap bit2)
        {
            Bitmap retVal = new Bitmap(bit1.Width, hMax);
            int w, h;

            double progressMulti = 1000.0 / bit1.Width;

            for (w = 0; w < bit1.Width; w++)
            {
                progressBar1.Value = (int) Math.Floor( w * progressMulti);
                for (h = 0; h < hMax; h++)
                {
                    int r, g, b;
                    if (Math.Abs(target.GetPixel(w , h).R - bit1.GetPixel(w, h).R) > Math.Abs(target.GetPixel(w , h).R - bit2.GetPixel(w, h).R))    // + (bit1.Width * num)
                        r = bit2.GetPixel(w, h).R;
                    else
                        r = bit1.GetPixel(w, h).R;

                    if (Math.Abs(target.GetPixel(w , h).G - bit1.GetPixel(w, h).G) > Math.Abs(target.GetPixel(w , h).G - bit2.GetPixel(w, h).G))
                        g = bit2.GetPixel(w, h).G;
                    else
                        g = bit1.GetPixel(w, h).G;

                    if (Math.Abs(target.GetPixel(w , h).B - bit1.GetPixel(w, h).B) > Math.Abs(target.GetPixel(w , h).B - bit2.GetPixel(w, h).B))
                        b = bit2.GetPixel(w, h).B;
                    else
                        b = bit1.GetPixel(w, h).B;

                    retVal.SetPixel(w, h, Color.FromArgb(r, g, b));

                }
            }            return retVal;
        }
       
    }
}
