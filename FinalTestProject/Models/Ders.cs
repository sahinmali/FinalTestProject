using FinalTestProject.Data;
using FinalTestProject.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace FinalTestProject.Models
{
    public class Ders
	{
		[Key]
		public string? DersKodu { get; set; }
        public string? DersAdi { get; set; }
        public int SaatCount { get; set; }
        public int Yariyil { get; set; }
        public float AKTS { get; set; }
        public float Kredi { get; set; }

        public DersNotu DersinNotu = new();
        public List<string>? OgrenciList { get; set; }
    }

}
