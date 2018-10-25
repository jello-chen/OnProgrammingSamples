using System.ComponentModel;

namespace CircleSample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int number;

        public int Number
        {
            get { return number; }
            set
            {
                if(number != value)
                {
                    number = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Number)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
