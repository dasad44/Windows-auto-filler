﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace Auto_filler
{
    class ScreenshotSave
    {
        //string path = @Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
        public void CaptureMyScreen()

        {
            if (MainWindow.ScreenShot)
            {
                try
                {
                    string path = MainWindow.path + "\\Capture.jpg";
                    double Xd = MainWindow.pos.X;
                    double Yd = MainWindow.pos.Y;
                    int X = (int)Xd;
                    int Y = (int)Yd;
                    int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                    captureBitmap.Save(@path, ImageFormat.Jpeg);
                    MessageBox.Show("Screen Captured " + X + " " + Y);
                    Process photoViewer = new Process();
                    photoViewer.StartInfo.FileName = @path;
                    photoViewer.StartInfo.Arguments = @"\Windows Photo Viewer\PhotoViewer.dll";
                    photoViewer.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}