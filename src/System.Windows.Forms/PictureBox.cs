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
                Handle = NativeMethods.QPictureBox_Create(IntPtr.Zero);
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
                using (var ms = new MemoryStream())
                {
                    // Save as PNG to preserve quality and transparency
                    _image.Save(ms, ImageFormat.Png);
                    var data = ms.ToArray();
                    NativeMethods.QPictureBox_SetImage(Handle, data, data.Length);
                }
            }
            //else if (!string.IsNullOrEmpty(_imageLocation))
            //{
            //     NativeMethods.QPictureBox_SetImageLocation(Handle, _imageLocation);
            //}
            else
            {
                NativeMethods.QPictureBox_SetImage(Handle, null, 0);
            }
        }

        private void UpdateSizeMode()
        {
            NativeMethods.QPictureBox_SetSizeMode(Handle, (int)_sizeMode);
        }
    }
}
