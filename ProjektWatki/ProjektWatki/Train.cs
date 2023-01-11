using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjektWatki
{
    public class Train: Vehicle
    {
        #region Fields
        #endregion
        #region Constructor&Destructor
        public Train(double speed, double positionX, double positionY, string startingPosition) : base(speed, positionX, positionY, startingPosition)
        {    
            CreateShape();
        }
        ~Train() 
        { 
        }
        #endregion
        #region Getters&Setters
        #endregion
        #region Methods
        public override void CreateShape()
        {
            base.CreateShape();
            vehicleShape.Height = 30;
            vehicleShape.Width = 150;
            ImageBrush imgBrush = new ImageBrush();
            RandomColor(imgBrush);
            vehicleShape.Fill = imgBrush;
        }
        private void RandomColor(ImageBrush randomImage)
        {
            Random random = new Random();
            int choice = random.Next(1, 5);
            switch (choice)
            {
                case 1:
                    randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/train1.png", UriKind.Relative));
                    break;
                case 2:
                    randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/train2.png", UriKind.Relative));
                    break;
                case 3:
                    randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/train3.png", UriKind.Relative));
                    break;
                case 4:
                    randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/train4.png", UriKind.Relative));
                    break;
            }
        }
        #endregion
    }
}
