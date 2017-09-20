using System;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace ImageReader.Model
{
    /// <summary>
    /// Звгрузчик изображения
    /// </summary>
    class Loader
    {
        private WebClient client { get; set; }

        /// <summary>
        /// номер загружаемого изображения
        /// </summary>
        private int _it;
        public int It
        {
            get { return _it; }
            set { _it = value; }
        }

        /// <summary>
        /// размер загружаемого изображения
        /// </summary>
        private long _maxSize;
        public long MaxSize
        {
            get { return _maxSize; }
            set { _maxSize = value; }
        }

        /// <summary>
        /// Текущий размер загружаемого изображения
        /// </summary>
        private double _curSize;
        public double CurSize
        {
            get { return _curSize; }
            set { _curSize = value; }
        }
        
        public delegate void UpdateProgress();
        event UpdateProgress Update;
        public delegate void BtmImage(BitmapImage btm, int it);
        event BtmImage Image;

           /// <summary>
           /// старт загрузки картинки
           /// </summary>
           /// <param name="path">URL</param>
           /// <param name="update">процедура обратного вызова для обновления ProgressBar</param>
           /// <param name="image">процедура обратного вызова для получения картинки</param>
           /// <param name="i">номер загружаемой картинки</param>
           /// <returns></returns>
        public bool Start(string path, UpdateProgress update, BtmImage image, int i)
        {
            Update = update;
            Image = image;
            client = new WebClient();
            It = i;
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadDataCompleted += Client_DownloadDataCompleted;
            try
            {
                client.DownloadDataAsync(new Uri(path));
                return true;
            }
            catch (Exception ex)
            {
                Stop();
                return false;
            }
        }


        /// <summary>
        /// загрузка завершена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                byte[] data = e.Result;
                BitmapImage image = new BitmapImage();
                using (var mem = new MemoryStream(data))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    try
                    {
                        image.EndInit();
                    }
                    catch (Exception ex) { Image(null, It); return; }
                }
                image.Freeze();
                Image(image, It);
            }
            catch (Exception ex) { }
            finally
            {
                Stop();
            }
        }

        /// <summary>
        /// процент загрузки изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            MaxSize = e.TotalBytesToReceive;
            CurSize = e.BytesReceived;
            Update();
        }

        /// <summary>
        /// Остановка загрузки картинки
        /// </summary>
        public void Stop()
        {
            client.CancelAsync();
            client.Dispose();
        }
    }
}

