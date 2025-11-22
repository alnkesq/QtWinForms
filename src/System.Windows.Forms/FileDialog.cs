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
            // If the description doesn't have it, we might need to append it?
            // Actually, Qt's setNameFilter simply takes strings. getOpenFileName takes "filter".
            // "Images (*.png *.xpm *.jpg);;Text files (*.txt);;XML files (*.xml)"
            
            var parts = filter.Split('|');
            var qtFilters = new System.Collections.Generic.List<string>();

            for (int i = 0; i < parts.Length; i += 2)
            {
                if (i + 1 >= parts.Length) break;

                string desc = parts[i];
                string pattern = parts[i + 1];

                // Convert "*.txt;*.doc" to "*.txt *.doc"
                string qtPattern = pattern.Replace(";", " ");

                // If description doesn't already contain the pattern, append it?
                // Qt documentation says: "The filter string must be in the format 'Description (*.ext1 *.ext2)'"
                // If the description is just "Text files", we need to make it "Text files (*.txt)"
                
                // Heuristic: check if desc contains "(".
                if (!desc.Contains("("))
                {
                    qtFilters.Add($"{desc} ({qtPattern})");
                }
                else
                {
                    // Assume the user provided a description that matches what they want, 
                    // but Qt *requires* the pattern in parens to actually filter.
                    // If the user provided "Text files (*.txt)", and pattern is "*.txt", we are good.
                    // If the user provided "Text files (stuff)", and pattern is "*.txt", we might have an issue if we don't ensure the pattern is there.
                    // For safety, let's just use the Qt format.
                    // Actually, if I pass "Text files (*.txt)" as the filter string to Qt, it uses "*.txt" as the filter.
                    // So I just need to ensure the string I pass to Qt is valid.
                    qtFilters.Add(desc);
                }
            }

            return string.Join(";;", qtFilters);
        }
    }
}
