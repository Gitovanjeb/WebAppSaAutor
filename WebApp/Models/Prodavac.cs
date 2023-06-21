using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Prodavac : Korisnik
    {
        List<int> objavljeniProizvodi = new List<int>();

        public List<int> ObjavljeniProizvodi { get => objavljeniProizvodi; set => objavljeniProizvodi = value; }

        public override string ToString()
        {
            string objavljeniProizvodiString = string.Join(",", ObjavljeniProizvodi);

            return $"{KorisnickoIme},{Lozinka},{Ime},{Prezime},{Pol},{Email},{DatumRodjenja},|{objavljeniProizvodiString}";
        }

        public static Prodavac Parse(string prodavacString)
        {
            Prodavac prodavac = new Prodavac();
            string[] vrednosti = prodavacString.Split(',');

            if (vrednosti.Length != 8)
            {
                throw new ArgumentException("Invalid format for prodavacString. Expected 8 vrednosti separated by commas.");
            }

            prodavac.KorisnickoIme = vrednosti[0];
            prodavac.Lozinka = vrednosti[1];
            prodavac.Ime = vrednosti[2];
            prodavac.Prezime = vrednosti[3];
            prodavac.Pol = (Pol)Enum.Parse(typeof(Pol), vrednosti[4]);
            prodavac.Email = vrednosti[5];
            prodavac.DatumRodjenja = DateTime.Parse(vrednosti[6]);

            string objavljeniProizvodiString = vrednosti[7].TrimStart('|').TrimEnd('|');
            string[] objavljeniProizvodiArray = objavljeniProizvodiString.Split(',');
            prodavac.ObjavljeniProizvodi = new List<int>();
            foreach (string proizvod in objavljeniProizvodiArray)
            {
                prodavac.ObjavljeniProizvodi.Add(int.Parse(proizvod));
            }

            return prodavac;
        }
    }
}