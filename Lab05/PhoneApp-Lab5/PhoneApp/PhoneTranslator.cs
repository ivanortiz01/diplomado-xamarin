using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    public class PhoneTranslator
    {
        string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXZ";
        string Numbers = "2223334445556667778889999";

        public string ToNumber(string alfanumericPhoneNumber)
        {
            var NumericPhoneNumer = new StringBuilder();
            if(!string.IsNullOrWhiteSpace(alfanumericPhoneNumber))
            {
                alfanumericPhoneNumber = alfanumericPhoneNumber.ToUpper();
                foreach(var c in alfanumericPhoneNumber)
                {
                    if("0123456789".Contains(c))
                    {
                        NumericPhoneNumer.Append(c);
                    }
                    else
                    {
                        var Index = Letters.IndexOf(c);
                        if(Index>=0)
                        {
                            NumericPhoneNumer.Append(Numbers[Index]);
                        }
                    }
                }
            }
            return NumericPhoneNumer.ToString();
        }
    }
}