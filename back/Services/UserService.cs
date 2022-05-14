using System;
using System.Collections.Generic;
using grud_backend.Helpers;
using grud_backend.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace grud_backend.Services
{
    public interface IUserService
    {
        User Authenticate(AuthenticateModel authModel);
        IEnumerable<User> GetAll();
        User Get(string id);
        List<User> Get();
        List<Role> GetRoles();
        User GetByEmail(string email);
        User GetByUsername(string username);
        User Create(User usuario);
        void Update(string id, User usuarioIn);
        void Delete(string id);
        void CreateRelacion(ProvCliente pc);
        List<ProvCliente> GetAfiliacionC(string id);
        List<ProvCliente> GetAfiliacionP(string id);
        IEnumerable<ProvCliente> GetAllAfiliaciones();
    }

    public class UserService : IUserService
    {
        private readonly string _secret;
        private readonly IMongoCollection<User> _mongoUsers;
        private readonly IMongoCollection<Role> _mongoRoles;
        private readonly IMongoCollection<ProvCliente> _mongoAfiliacion;

        private IMongoQueryable<User> _users;
        private IMongoQueryable<ProvCliente> _afiliaciones;
        private static Global global = new Global();

        
        public UserService(IAppSettings appSettings)
        {
            var client = new MongoClient(appSettings.ConnectionString);
            var database = client.GetDatabase(appSettings.DatabaseName);
            _mongoAfiliacion = database.GetCollection<ProvCliente>(appSettings.AfiliacionCollectionName);
            _mongoUsers = database.GetCollection<User>(appSettings.UsersCollectionName);
            _mongoRoles = database.GetCollection<Role>(appSettings.RolesCollectionName);
            _users = database.GetCollection<User>(appSettings.UsersCollectionName).AsQueryable();
            _afiliaciones = database.GetCollection<ProvCliente>(appSettings.AfiliacionCollectionName).AsQueryable();
            _secret = appSettings.Secret;
        }

        public User Authenticate(AuthenticateModel authModel)
        {
            var email = authModel.Email;
            var username = authModel.Username;
            var password = authModel.Password;

            var user = Get().SingleOrDefault(x =>
            {
                if (email != null)
                {
                    return x.Email.ToString().ToLower().Contains(email.ToLower()) && global.Decrypt(x.Password) == password;
                }

                if (username != null)
                {
                    return String.Equals(x.Username.ToString(), username, StringComparison.CurrentCultureIgnoreCase) && global.Decrypt(x.Password) == password;
                }
                
                return false;
            });
            if (user == null)
                return null;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.WithoutPassword();
        }

        public List<Role> GetRoles()
        {
            return _mongoRoles.Find(x => true).ToList();
        }

        public User GetByEmail(string email)
        {
            return _mongoUsers.Find(user => user.Email.ToLower().Contains(email.ToLower())).FirstOrDefault();
        }
        
        public User GetByUsername(string username)
        {
            return _mongoUsers.Find(user => user.Username.ToLower().Contains(username.ToLower())).FirstOrDefault();
        }
        
        public User Get(string id)
        {
            return  _mongoUsers.Find(user => user.Id == id).FirstOrDefault();
        }

        public List<User> Get() => _mongoUsers.Find(user => true).ToList();

        public User Create(User usuario)
        {
            usuario.Id = Guid.NewGuid().ToString("D");
            usuario.Password = global.Encrypt(usuario.Password);
            _mongoUsers.InsertOne(usuario);
            return usuario;
        }

        public void Update(string id, User usuarioIn)
        {
            _mongoUsers.ReplaceOne(usuario => usuario.Id == id, usuarioIn);
        }

        public void Delete(string id)
        {
            _mongoUsers.DeleteOne((usuario => usuario.Id == id));
        }

        public void CreateRelacion(ProvCliente pc)
        {
            pc.Id = Guid.NewGuid().ToString("D");
            _mongoAfiliacion.InsertOne(pc);
        }

        public List<ProvCliente> GetAfiliacionC(string id)
        {
            return _mongoAfiliacion.Find(af => af.IdCliente== id).ToList();
        }

        public List<ProvCliente> GetAfiliacionP(string id)
        {
            return _mongoAfiliacion.Find(af => af.IdCliente== id).ToList();
        }

        public IEnumerable<ProvCliente> GetAllAfiliaciones()
        {
            return _afiliaciones;
        }
    }
}