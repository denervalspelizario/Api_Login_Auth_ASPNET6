﻿using Api_Login_Auth_ASPNET.Enum;

namespace Api_Login_Auth_ASPNET.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public CargoEnum Cargo { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public DateTime TokenDataCriacao { get; set; } = DateTime.Now;
    }
}
