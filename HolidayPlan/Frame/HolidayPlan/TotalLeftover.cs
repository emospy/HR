using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HolidayPlan
{
    class TotalLeftover : INotifyPropertyChanged
    {
        private int personID;
        private int totalLeftover;
        private readonly int totalLeftoverOriginal;

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
                return this.totalLeftover;
            }
            set
            {
                if (value > int.MinValue && value < int.MaxValue)
                {
                    this.totalLeftover = value;
                    this.NotifyPropertyChanged("Leftover");
                }
            }
        }
        public int LeftoverOriginal
        {
            get
            {
                return this.totalLeftoverOriginal;
            }
        }

        public TotalLeftover(int personID, int totalLeftover)
        {
            this.PersonID = personID;
            this.Leftover = totalLeftover;
            if (totalLeftover > 0 && totalLeftover < int.MaxValue)
            {
                this.totalLeftoverOriginal = totalLeftover;
            }
            else
            {
                this.totalLeftoverOriginal = 0;
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
