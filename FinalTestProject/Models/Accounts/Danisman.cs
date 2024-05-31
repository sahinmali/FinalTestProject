using Microsoft.EntityFrameworkCore;

namespace FinalTestProject.Models.Accounts
{
	public class Danisman : Hesap
	{
		public List<Ogrenci> TanimliOgrenciler = new List<Ogrenci>();

		public List<Ogrenci> OgrGor()
		{
			return this.TanimliOgrenciler;
		}

		public void SecimGuncelle(Ogrenci ogrenci)
		{
			if (ogrenci != null && ogrenci.dersSecim != null)
			{
				ogrenci.dersSecim.onayla();
			}
		}
	}
}
