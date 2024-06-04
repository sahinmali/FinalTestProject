
namespace FinalTestProject.Models
{
    public class DersSecimi
    {
        public List<string>? SecilenDersler { get; set; }
        public long OgrenciKimlikNo { get; set; }
        public string? DanismanKimlikNo { get; set; }

        public List<Ders> DersList = [];

    }
}
