using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class PictureBox : Control
    {
        private Image? _image;
        private string? _imageLocation;
        private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.Normal;

        public PictureBox()
        {
        }

        protected override void CreateHandle()
        {
            if (!IsHandleCreated)
            {
                QtHandle = NativeMethods.QPictureBox_Create(IntPtr.Zero);
                SetCommonProperties();
                UpdateImage();
                UpdateSizeMode();
            }
        }

        public Image? Image
        {
            get => _image;
            set
            {
                _image = value;
                if (IsHandleCreated)
                {
                    UpdateImage();
                }
                if (_sizeMode == PictureBoxSizeMode.AutoSize && _image != null)
                {
                    Size = _image.Size;
                }
            }
        }

        public string? ImageLocation
        {
            get => _imageLocation;
            set
            {
                _imageLocation = value;
                _image = null; // Reset image initially

                // Try to load into _image
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        if (File.Exists(value))
                        {
                            _image = Image.FromFile(value);
                        }
                    }
                    catch { }
                }

                if (IsHandleCreated)
                {
                    UpdateImage();
                }

                if (_sizeMode == PictureBoxSizeMode.AutoSize && _image != null)
                {
                    Size = _image.Size;
                }
            }
        }

        public PictureBoxSizeMode SizeMode
        {
            get => _sizeMode;
            set
            {
                if (_sizeMode != value)
                {
                    _sizeMode = value;
                    if (IsHandleCreated)
                    {
                        UpdateSizeMode();
                    }
                    if (_sizeMode == PictureBoxSizeMode.AutoSize && _image != null)
                    {
                        Size = _image.Size;
                    }
                }
            }
        }

        private void UpdateImage()
        {
            if (_image != null)
            {
                IntPtr pixmap = _image.GetQPixmap();
                NativeMethods.QPictureBox_SetImage(QtHandle, pixmap);
            }
            //else if (!string.IsNullOrEmpty(_imageLocation))
            //{
            //     NativeMethods.QPictureBox_SetImageLocation(Handle, _imageLocation);
            //}
            else
            {
                NativeMethods.QPictureBox_SetImage(QtHandle, IntPtr.Zero);
            }
        }

        private void UpdateSizeMode()
        {
            NativeMethods.QPictureBox_SetSizeMode(QtHandle, (int)_sizeMode);
        }
    }
}
