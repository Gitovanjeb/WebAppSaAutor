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
    public IHttpActionResult Get()
    {
        // Retrieve the user information from the JWT token's claims
        string username = Request.Headers.GetValues("Username").FirstOrDefault();

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
    public IHttpActionResult Put(Korisnik updatedUser)
    {
        // Retrieve the user information from the JWT token's claims
        ClaimsIdentity identity = User.Identity as ClaimsIdentity;
        if (identity == null)
        {
            return BadRequest("Invalid token.");
        }

        string username = identity.FindFirst(ClaimTypes.Name)?.Value;

        // Retrieve the user profile from your existing database using Baza.korisnici.Values.ToList()
        List<Korisnik> korisnici = Baza.korisnici.Values.ToList();
        Korisnik existingUser = korisnici.FirstOrDefault(u => u.Ime == username);
        if (existingUser == null)
        {
            return NotFound();
        }

        // Update the user information
        existingUser.Ime = updatedUser.Ime ?? existingUser.Ime;
        existingUser.Prezime = updatedUser.Prezime ?? existingUser.Prezime;
        existingUser.Lozinka = updatedUser.Lozinka ?? existingUser.Lozinka;
        existingUser.Email = updatedUser.Email ?? existingUser.Email;
        if (updatedUser.DatumRodjenja != DateTime.MinValue)
        {
            existingUser.DatumRodjenja = updatedUser.DatumRodjenja;
        }
        return Ok(existingUser);
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