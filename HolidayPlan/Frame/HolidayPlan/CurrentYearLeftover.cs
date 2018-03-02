using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HolidayPlan
{
    class CurrentYearLeftover : INotifyPropertyChanged
    {
        private int personID;
        private int currentYearLeftover;
        private readonly int currentYearLeftoverOriginal;
                
        public event PropertyChangedEventHandler PropertyChanged;
        public int PersonID
        {
            get
            {
                return this.personID;
            }
            set
            {
                if (value > 0 && value < int.MaxValue)
                {
                    this.personID = value;
                }
            }
        }
        public int Leftover
        {
            get
            {
                return this.currentYearLeftover;
            }
            set
            {
                if (value > int.MinValue && value < int.MaxValue)
                {
                    this.currentYearLeftover = value;
                    this.NotifyPropertyChanged("Leftover");
                }
            }
        }
        public int LeftoverOriginal
        {
            get
            {
                return this.currentYearLeftoverOriginal;
            }
        }

        public CurrentYearLeftover(int personID, int currentYearLeftover)
        {
            this.PersonID = personID;
            this.Leftover = currentYearLeftover;
            if (currentYearLeftover > 0 && currentYearLeftover < int.MaxValue)
            {
                this.currentYearLeftoverOriginal = currentYearLeftover;
            }
            else
            {
                this.currentYearLeftoverOriginal = 0;
            }
        }
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
