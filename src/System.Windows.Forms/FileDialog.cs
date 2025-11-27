using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
    public abstract class FileDialog : CommonDialog
    {
        public string FileName { get; set; } = string.Empty;
        public string InitialDirectory { get; set; } = string.Empty;
        public string Filter { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        [Obsolete(Control.NotImplementedWarning)] public int FilterIndex { get; set; }

        public override void Reset()
        {
            FileName = string.Empty;
            InitialDirectory = string.Empty;
            Filter = string.Empty;
            Title = string.Empty;
        }

        protected static string TranslateFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return string.Empty;

            // WinForms: "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            // Qt: "Text files (*.txt);;All files (*.*)"
            // Note: Qt expects the pattern to be in parentheses at the end of the description string.
            // "Images (*.png *.xpm *.jpg);;Text files (*.txt);;XML files (*.xml)"

            var parts = filter.Split('|');
            var qtFilters = new System.Collections.Generic.List<string>();

            for (int i = 0; i < parts.Length; i += 2)
            {
                if (i + 1 >= parts.Length) break;

                string desc = parts[i];
                string pattern = parts[i + 1];

                string qtPattern = pattern.Replace(";", " ");


                var index = desc.IndexOf("(*.");
                if (index == -1)
                {
                    qtFilters.Add($"{desc} ({qtPattern})");
                }
                else
                {
                    desc = desc.Substring(0, index).Trim();
                    qtFilters.Add($"{desc} ({qtPattern})");
                }
            }

            return string.Join(";;", qtFilters);
        }
    }
}
