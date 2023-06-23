using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApp.Models;

public class KupacProfilController : ApiController
{
    // GET api/profile
  //  [Authorize]
    public IHttpActionResult Get(string username)
    {
        // Retrieve the user information from the JWT token's claims
       // string username = Request.Headers.GetValues("Username").FirstOrDefault();

        // Retrieve the user profile from your existing database using Baza.korisnici.Values.ToList()
        List<Korisnik> korisnici = Baza.korisnici.Values.ToList();
        Korisnik korisnik = korisnici.FirstOrDefault(u => u.KorisnickoIme == username);
        if (korisnik == null)
        {
            return NotFound();
        }

        return Ok(korisnik);
    }

    // PUT api/profile
    //[Authorize]
    public IHttpActionResult Put(string username, Korisnik updateUser)
    {
        // Retrieve the user information from the JWT token's claims
        List<Korisnik> korisnici = Baza.korisnici.Values.ToList();
        Korisnik korisnik = korisnici.FirstOrDefault(u => u.KorisnickoIme == username);
        if (korisnik == null)
        {
            return NotFound();
        }






        korisnik.Ime = updateUser.Name ?? korisnik.Ime;
        korisnik.Prezime = Request.Form["updatedSurname"] ?? korisnik.Prezime;
        korisnik.Lozinka = Request.Form["updatedPassword"] ?? korisnik.Lozinka;
        korisnik.Email = Request.Form["updatedEmail"] ?? korisnik.Email;
        DateTime dateOfBirth;
        if (DateTime.TryParse(Request.Form["updatedDateOfBirth"], out dateOfBirth))
        {
            korisnik.DateOfBirth = dateOfBirth;
        }


    }

/*public class User
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}
*/