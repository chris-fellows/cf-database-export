using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFDatabaseExport
{
    internal class UIUtilities
    {
        public static string SelectFile(string title, string selectedFile, string filter, bool checkFileExists, bool checkPathExists)
        {
            OpenFileDialog dialog = new OpenFileDialog() { Title = title, FileName = selectedFile, CheckFileExists = checkFileExists, CheckPathExists = checkPathExists };
            if (!String.IsNullOrEmpty(filter))
            {
                dialog.Filter = filter;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return selectedFile;
        }

        public static Control GetControl<T>(Control parent)
        {
            Type type = typeof(T);

            foreach (Control childControl in parent.Controls)
            {
                //string test = childControl.GetType().ToString();
                if (childControl.GetType() == type)
                {
                    return childControl;
                }
            }
            return null;
        }

        public static ComboBox GetComboBox(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                //string test = childControl.GetType().ToString();
                if (childControl is ComboBox)
                {
                    return (ComboBox)childControl;
                }
            }
            return null;
        }

        public static ListView GetListView(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is ListView)
                {
                    return (ListView)childControl;
                }
            }
            return null;
        }

        public static TextBox GetTextBox(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is TextBox)
                {
                    return (TextBox)childControl;
                }
            }
            return null;
        }
    }
}
