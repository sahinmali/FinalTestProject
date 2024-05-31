using FinalTestProject.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace FinalTestProject.Models
{
	public class Ders
	{
		[Key]
		public string DersKodu = "";
		public OgretimElemani atananOgretimElemani = new();
		public int SaatCount;
		public int Yariyil;
		public float AKTS;
		public float Kredi;
		public string DersAdi = "";
		public DersNotu dersinNotu = new();
		public List<Ogrenci> OgrenciList = [];

	}

}
