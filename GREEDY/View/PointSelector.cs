using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using GREEDY.DataManagers;
using GREEDY.OCRs;
using GREEDY.Services;

namespace GREEDY.View
{
    public partial class PointSelector : Form
    {
        private Bitmap _image;
        public PointSelector(Bitmap image)
        {
            _image = image;
            InitializeComponent();
        }

        private void pointSelector_Load(object sender, EventArgs e)
        {
            pictureBox.Image = _image;
        }

        readonly PropertyInfo _imageRectangleProperty = typeof(PictureBox).GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);

        private Point TranslateZoomMousePosition(Point coordinates)
        {
            // test to make sure our image is not null
            if (pictureBox.Image == null) return coordinates;
            // Make sure our control width and height are not 0 and our 
            // image width and height are not 0
            if (pictureBox.Width == 0 || pictureBox.Height == 0 || pictureBox.Image.Width == 0 || pictureBox.Image.Height == 0) return coordinates;
            // This is the one that gets a little tricky. Essentially, need to check 
            // the aspect ratio of the image to the aspect ratio of the control
            // to determine how it is being rendered
            float imageAspect = (float)pictureBox.Image.Width / pictureBox.Image.Height;
            float controlAspect = (float)pictureBox.Width / pictureBox.Height;
            float newX = coordinates.X;
            float newY = coordinates.Y;
            if (imageAspect > controlAspect)
            {
                // This means that we are limited by width, 
                // meaning the image fills up the entire control from left to right
                float ratioWidth = (float)pictureBox.Image.Width / pictureBox.Width;
                newX *= ratioWidth;
                float scale = (float)pictureBox.Width / pictureBox.Image.Width;
                float displayHeight = scale * pictureBox.Image.Height;
                float diffHeight = pictureBox.Height - displayHeight;
                diffHeight /= 2;
                newY -= diffHeight;
                newY /= scale;
            }
            else
            {
                // This means that we are limited by height, 
                // meaning the image fills up the entire control from top to bottom
                float ratioHeight = (float)pictureBox.Image.Height / pictureBox.Height;
                newY *= ratioHeight;
                float scale = (float)pictureBox.Height / pictureBox.Image.Height;
                float displayWidth = scale * pictureBox.Image.Width;
                float diffWidth = pictureBox.Width - displayWidth;
                diffWidth /= 2;
                newX -= diffWidth;
                newX /= scale;
            }
            return new Point((int)newX, (int)newY);
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            
            
            if (pictureBox.Image != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                var original = (Bitmap)pictureBox.Image;

                Point? point = null;
                switch (pictureBox.SizeMode)
                {
                    case PictureBoxSizeMode.Normal:
                    case PictureBoxSizeMode.AutoSize:
                    {
                        
                        point = new Point(me.X,me.Y);
                            break;
                        }
                    case PictureBoxSizeMode.CenterImage:
                    case PictureBoxSizeMode.StretchImage:
                    case PictureBoxSizeMode.Zoom:
                        {
                            Rectangle rectangle = (Rectangle)_imageRectangleProperty.GetValue(pictureBox, null);
                            if (rectangle.Contains(me.Location))
                            {
                                using (Bitmap copy = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height))
                                {
                                    using (Graphics g = Graphics.FromImage(copy))
                                    {
                                        //sita cj galima bus istrinti
                                        g.DrawImage(pictureBox.Image, rectangle);
                                        
                                        point = TranslateZoomMousePosition(new Point(me.X, me.Y));
                                    }
                                }
                            }
                            break;
                        }
                        
                }

                

                if (!point.HasValue)
                {
                   
                }
                else
                {
                    if (point1x.Text.Equals(""))
                    {
                        point1x.Text = point.Value.X.ToString();
                        point1y.Text = point.Value.Y.ToString();
                    }
                    else if (point2x.Text.Equals(""))
                    {
                        point2x.Text = point.Value.X.ToString();
                        point2y.Text = point.Value.Y.ToString();
                    }
                    else if (point4x.Text.Equals(""))
                    {
                        point4x.Text = point.Value.X.ToString();
                        point4y.Text = point.Value.Y.ToString();
                    }
                    else if (point3x.Text.Equals(""))
                    {
                        point3x.Text = point.Value.X.ToString();
                        point3y.Text = point.Value.Y.ToString();
                    }
                }
            }
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            if (!point1x.Text.Equals("") &&
                !point2x.Text.Equals("") &&
                !point3x.Text.Equals("") &&
                !point4x.Text.Equals(""))
            {
                ImageFormatService service = new ImageFormatService();
                Bitmap image = service.Resize(new Bitmap(pictureBox.Image),
                    new double[]
                    {
                        Double.Parse(point1x.Text), Double.Parse(point1y.Text),
                        Double.Parse(point2x.Text), Double.Parse(point2y.Text),
                        Double.Parse(point3x.Text), Double.Parse(point3y.Text),
                        Double.Parse(point4x.Text), Double.Parse(point4y.Text)
                    });

                //temp. testing how every filter works
                IReceiptService receiptService = new ReceiptService(new EmguOcr(), new DataConverter(), new DataManager());

                ImageViewTest original = 
                    new ImageViewTest(image, "Original", receiptService);
                ImageViewTest blur = 
                    new ImageViewTest(service.Blur(image, 5, 5), "Blured", receiptService);
                ImageViewTest dilate = 
                    new ImageViewTest(service.Dilate(service.Blur(image, 1, 1), 1), "Dilated", receiptService);
                ImageViewTest erode = 
                    new ImageViewTest(service.Erode(image, 1), "Eroded", receiptService);
                ImageViewTest blackAndWhite = 
                    new ImageViewTest(service.BlackAndWhite(image),"Black and white", receiptService);
                original.Show();
                blur.Show();
                dilate.Show();
                erode.Show();
                blackAndWhite.Show();
            }
            else
            {
                MessageBox.Show("Enter all points");
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            point1x.Text = "";
            point1y.Text = "";
            point2x.Text = "";
            point2y.Text = "";
            point3x.Text = "";
            point3y.Text = "";
            point4x.Text = "";
            point4y.Text = "";
        }
    }
}
