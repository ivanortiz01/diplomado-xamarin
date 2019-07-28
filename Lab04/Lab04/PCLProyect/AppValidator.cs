using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLProyect
{
    public class AppValidator
    {
        IDialog Dialog;

        public string Email { get; set; }
        public string Password { get; set; }
        public string Device { get; set; }

        public AppValidator(IDialog platformDialog)
        {
            Dialog = platformDialog;
        }

        public async void Validate()
        {
            string Result;

            var ServiceClient = new SALLab04.ServiceClient();
            var SvcClient = await ServiceClient.ValidateAsync(Email, Password, Device);
            Result = $"{SvcClient.Status}\n{SvcClient.Fullname}\n{SvcClient.Token}";
            
            Dialog.Show(Result);
        }
    }
}

