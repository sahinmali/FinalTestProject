
namespace FinalTestProject.Data
{
    public class Danisman : Hesap
    {
        public List<string>? TanimliOgrenciler { get; set; }

        public List<string> OgrGor()
        {
            return TanimliOgrenciler;
        }

        // public void SecimGuncelle(string ogrenciKimlikNo)
        // {

        //     if (ogrenci != null && ogrenci.dersSecim != null)
        //     {
        //         ogrenci.dersSecim.onayla();
        //     }
        // }

    }
}
