using ImageReader.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageReader.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        #region Binding

        private int _progressLoad;
        public int ProgressLoad
        {
            get { return _progressLoad; }
            set
            {
                _progressLoad = value;
                OnPropertyChanged("ProgressLoad");
            }
        }

        private int _maxSize;
        public int MaxSize
        {
            get { return _maxSize; }
            set
            {
                _maxSize = value;
                OnPropertyChanged("MaxSize");
            }
        }

        private string _image1;
        public string Image1
        {
            get { return _image1; }
            set
            {
                _image1 = value;
                OnPropertyChanged("Image1");
            }
        }

        private string _image2;
        public string Image2
        {
            get { return _image2; }
            set
            {
                _image2 = value;
                OnPropertyChanged("Image2");
            }
        }

        private string _image3;
        public string Image3
        {
            get { return _image3; }
            set
            {
                _image3 = value;
                OnPropertyChanged("Image3");
            }
        }

        private string _url1;
        public string URL1
        {
            get { return _url1; }
            set
            {
                _url1 = value;
                OnPropertyChanged("URL1");
            }
        }

        private string _url2;
        public string URL2
        {
            get { return _url2; }
            set
            {
                _url2 = value;
                OnPropertyChanged("URL2");
            }
        }

        private string _url3;
        public string URL3
        {
            get { return _url3; }
            set
            {
                _url3 = value;
                OnPropertyChanged("URL3");
            }
        }


        #endregion

        #region Command

        AppCommand _start1;
        public AppCommand StartLoadImage1
        {
            get
            {
                return _start1 ??
                    (_start1 = new AppCommand(() =>
                    {
                        Image1 = URL1;
                    }));
            }
        }

        AppCommand _start2;
        public AppCommand StartLoadImage2
        {
            get
            {
                return _start2 ??
                    (_start2 = new AppCommand(() =>
                    {
                        Image2 = URL2;
                    }));
            }
        }

        AppCommand _start3;
        public AppCommand StartLoadImage3
        {
            get
            {
                return _start3 ??
                    (_start3 = new AppCommand(() =>
                    {
                        Image3 = URL3;
                    }));
            }
        }

        AppCommand _stop1;
        public AppCommand StopLoadImage1
        {
            get
            {
                return _stop1 ??
                    (_stop1 = new AppCommand(() =>
                    {
                    }));
            }
        }

        AppCommand _stop2;
        public AppCommand StopLoadImage2
        {
            get
            {
                return _stop2 ??
                    (_stop2 = new AppCommand(() =>
                    {
                    }));
            }
        }

        AppCommand _stop3;
        public AppCommand StopLoadImage3
        {
            get
            {
                return _stop3 ??
                    (_stop3 = new AppCommand(() =>
                    {
                    }));
            }
        }

        #endregion
        
    }
}
