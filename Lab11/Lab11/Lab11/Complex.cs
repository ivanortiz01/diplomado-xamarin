
using Android.App;
using Android.OS;

namespace Lab11
{
    public class Complex : Fragment
    {
        public int Real { get; set; }
        public int Imaginary { get; set; }

        public override string ToString()
        {
            return $"{Real} + {Imaginary}i";
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }

    }
}