using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPApp
{
    public class UWPDialog : PCLProyect.IDialog
    {
        public async void Show(string message)
        {
            var Dialog = new Windows.UI.Popups.MessageDialog(message);
            await Dialog.ShowAsync();
        }
    }
}
