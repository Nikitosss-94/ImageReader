using ImageReader.Command;
using ImageReader.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageReader.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        /// <summary>
        /// Загрузчик 1 изображения
        /// </summary>
        public Loader Loader1 { get; set; }

        /// <summary>
        /// Загрузчик 2 изображения
        /// </summary>
        public Loader Loader2 { get; set; }

        /// <summary>
        /// Загрузчик 3 изображения
        /// </summary>
        public Loader Loader3 { get; set; }

        #region Binding

        /// <summary>
        /// Положение BrogressBar
        /// </summary>
        private double _progressLoad;
        public double ProgressLoad
        {
            get { return _progressLoad; }
            set
            {
                _progressLoad = value;
                OnPropertyChanged("ProgressLoad");
            }
        }

        /// <summary>
        /// 1 Изображение
        /// </summary>
        private object _image1;
        public object Image1
        {
            get { return _image1; }
            set
            {
                _image1 = value;
                OnPropertyChanged("Image1");
            }
        }

        /// <summary>
        /// 2 Изображение
        /// </summary>
        private object _image2;
        public object Image2
        {
            get { return _image2; }
            set
            {
                _image2 = value;
                OnPropertyChanged("Image2");
            }
        }

        /// <summary>
        /// 3 Изображение
        /// </summary>
        private object _image3;
        public object Image3
        {
            get { return _image3; }
            set
            {
                _image3 = value;
                OnPropertyChanged("Image3");
            }
        }

        /// <summary>
        /// URL 1 изображения
        /// </summary>
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

        /// <summary>
        /// URL 2 изображения
        /// </summary>
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

        /// <summary>
        /// URL 3 изображения
        /// </summary>
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

        /// <summary>
        /// Загрузка 1 изображения
        /// </summary>
        AppCommand _start1;
        public AppCommand StartLoadImage1
        {
            get
            {
                return _start1 ??
                    (_start1 = new AppCommand(() =>
                    {
                        if (!(Loader1 = new Loader()).Start(URL1, Progress, BtmImage, 1))
                        {
                            Loader1 = null;
                            MessageBox.Show("Неверный URL");
                        }
                    }
                    , () => (!string.IsNullOrWhiteSpace(URL1) & Loader1 == null)));
            }
        }

        /// <summary>
        /// Загрузка 2 изображения
        /// </summary>
        AppCommand _start2;
        public AppCommand StartLoadImage2
        {
            get
            {
                return _start2 ??
                    (_start2 = new AppCommand(() =>
                    {
                        if (!(Loader2 = new Loader()).Start(URL2, Progress, BtmImage, 2))
                        {
                            Loader2 = null;
                            MessageBox.Show("Неверный URL");
                        }
                    }
                    , () => (!string.IsNullOrWhiteSpace(URL2) & Loader2 == null)));
            }
        }

        /// <summary>
        /// Загрузка 3 изображения
        /// </summary>
        AppCommand _start3;
        public AppCommand StartLoadImage3
        {
            get
            {
                return _start3 ??
                    (_start3 = new AppCommand(() =>
                    {
                        if (!(Loader3 = new Loader()).Start(URL3, Progress, BtmImage, 3))
                        {
                            Loader3 = null;
                            MessageBox.Show("Неверный URL");
                        }
                    }
                    , () => (!string.IsNullOrWhiteSpace(URL3) & Loader3 == null)));
            }
        }

        /// <summary>
        /// Загрузка всех изображений
        /// </summary>
        AppCommand _startAll;
        public AppCommand StartAll
        {
            get
            {
                return _startAll ??
                    (_startAll = new AppCommand(() =>
                    {
                        if (!string.IsNullOrWhiteSpace(URL1) & Loader1 == null) (Loader1 = new Loader()).Start(URL1, Progress, BtmImage, 1);
                        if (!string.IsNullOrWhiteSpace(URL2) & Loader2 == null) (Loader2 = new Loader()).Start(URL2, Progress, BtmImage, 2);
                        if (!string.IsNullOrWhiteSpace(URL3) & Loader3 == null) (Loader3 = new Loader()).Start(URL3, Progress, BtmImage, 3);
                    }
                    , () => (!string.IsNullOrWhiteSpace(URL1) & Loader1 == null) | 
                    (!string.IsNullOrWhiteSpace(URL2) & Loader2 == null) | 
                    (!string.IsNullOrWhiteSpace(URL3) & Loader3 == null)));
            }
        }

        /// <summary>
        /// Остановка загрузки 1 изображения
        /// </summary>
        AppCommand _stop1;
        public AppCommand StopLoadImage1
        {
            get
            {
                return _stop1 ??
                    (_stop1 = new AppCommand(() =>
                    {
                        Loader1.Stop();
                        Loader1 = null;
                        Progress();
                    },
                    () => Loader1 != null
                    ));
            }
        }

        /// <summary>
        /// Остановка загрузки 2 изображения
        /// </summary>
        AppCommand _stop2;
        public AppCommand StopLoadImage2
        {
            get
            {
                return _stop2 ??
                    (_stop2 = new AppCommand(() =>
                    {
                        Loader2.Stop();
                        Loader2 = null;
                        Progress();
                    },
                    () => Loader2 != null
                    ));
            }
        }

        /// <summary>
        /// Остановка загрузки 3 изображения
        /// </summary>
        AppCommand _stop3;
        public AppCommand StopLoadImage3
        {
            get
            {
                return _stop3 ??
                    (_stop3 = new AppCommand(() =>
                    {
                        Loader3.Stop();
                        Loader3 = null;
                        Progress();
                    },
                    () => Loader3 != null
                    ));
            }
        }

        #endregion
        
        /// <summary>
        /// Получение загруженного изображения
        /// </summary>
        /// <param name="image">Изображение</param>
        /// <param name="i">Номер изображения</param>
        private void BtmImage(BitmapImage image, int i)
        {
            switch (i)
            {
                case 1:
                    Image1 = image;
                    Loader1 = null;
                    break;
                case 2:
                    Image2 = image;
                    Loader2 = null;
                    break;
                case 3:
                    Image3 = image;
                    Loader3 = null;
                    break;
            }
            if (image == null) MessageBox.Show("Указанный URL " + i + " не является картинкой");
            Progress();
        }

        /// <summary>
        /// Обновление ProgressBar
        /// </summary>
        private void Progress()
        {
            var m = 0;
            double p = 0;
            m += Loader1 != null ? (int)Loader1.MaxSize : 0;
            m += Loader2 != null ? (int)Loader2.MaxSize : 0;
            m += Loader3 != null ? (int)Loader3.MaxSize : 0;
            p += Loader1 != null ? Loader1.CurSize : 0;
            p += Loader2 != null ? Loader2.CurSize : 0;
            p += Loader3 != null ? Loader3.CurSize : 0;
            ProgressLoad = (p * 100 / m);       
        }
    }
}
