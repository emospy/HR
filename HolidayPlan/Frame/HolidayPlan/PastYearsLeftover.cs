using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HolidayPlan
{
    class PastYearsLeftover : INotifyPropertyChanged
    {
        private int personID;
        private int pastYearsTotal;
        private readonly int pastYearsTotalOriginal;
               
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
                return this.pastYearsTotal;
            }
            set
            {
                if (value > int.MinValue && value < int.MaxValue)
                {
                    this.pastYearsTotal = value;
                    this.NotifyPropertyChanged("Leftover");
                }
            }
        }
        public int LeftoverOriginal
        {
            get
            {
                return this.pastYearsTotalOriginal;
            }
        }
       
        public PastYearsLeftover(int personID, int pastYearsSum)
        {
            this.PersonID = personID;
            this.Leftover = pastYearsSum;
            if (pastYearsSum > 0 && pastYearsSum < int.MaxValue)
            {
                this.pastYearsTotalOriginal = pastYearsSum;
            }
            else
            {
                this.pastYearsTotalOriginal = 0;
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
