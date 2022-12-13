using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace ProjektWatki
{
    public abstract class Vehicle
    {
        #region Fields
        protected int speed;
        protected Rectangle vehicleShape;
        protected int priority;
        #endregion
        #region Coonstructor&Destructor
        public Vehicle(int speed, int priority)
        {
            this.speed = speed;
            this.priority = priority;
            this.vehicleShape = new Rectangle();
        }
        ~Vehicle() 
        { 
        }
        #endregion
        #region Getters&Setters
        public Rectangle VehicleShape
        {
            get
            {
                return vehicleShape;
            }
            set
            {
                vehicleShape = value;
            }
        }
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        #endregion
        #region Methods
        public virtual void CreateShape() // do dziedziczenia
        {

        }
        #endregion
    }
}
