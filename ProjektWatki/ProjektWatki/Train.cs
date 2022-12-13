using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjektWatki
{
    public class Train: Vehicle
    {
        #region Fields
        #endregion
        #region Constructor&Destructor
        public Train(int speed, int priority) : base(speed, priority)
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
            SolidColorBrush brownBrush = new SolidColorBrush();
            brownBrush.Color = Colors.Brown;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            vehicleShape.StrokeThickness = 4;
            vehicleShape.Stroke = blackBrush;
            vehicleShape.Fill = brownBrush;
            vehicleShape.RadiusX = 10;
            vehicleShape.RadiusY = 10;
        }
        #endregion
    }
}
