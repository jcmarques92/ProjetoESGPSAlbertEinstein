using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AlbertEinsteinHospital
{
    public class DadosUtilizador
    {
        public static void registarUtilizador(string nome, DateTime dataNascim, string genero, string morada, int telefone, bool ativo, string email, int sns, string nomeUtilizador, string password, string tipoUtilizador)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Utilizador u = new Utilizador();

            u.Nome = nome;
            u.DataNascimento = dataNascim;
            u.Genero = genero;
            u.Morada = morada;
            u.Telefone = telefone;
            u.Ativo = ativo;
            u.Email = email;
            u.NumSns = sns;
            u.NomeUtilizador = nomeUtilizador;
            u.Password = password;
            u.TipoUtilizador = tipoUtilizador;

            bd.PessoaSet.Add(u);
            bd.SaveChanges();

            Utilizador utilizador = bd.PessoaSet.OfType<Utilizador>().Where(i => i.Id == 1).FirstOrDefault();

        }

        public static void atualizarAtualizador(string nome, DateTime dataNascim, string genero, string morada, int telefone, bool ativo, string email, int sns, string nomeUtilizador, string password, string tipoUtilizador)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Utilizador u = bd.PessoaSet.OfType<Utilizador>().Where(i => i.NumSns == sns).FirstOrDefault();

            u.Nome = nome;
            u.DataNascimento = dataNascim;
            u.Genero = genero;
            u.Morada = morada;
            u.Telefone = telefone;
            u.Ativo = ativo;
            u.Email = email;
            u.NumSns = sns;
            u.NomeUtilizador = nomeUtilizador;
            u.Password = password;
            u.TipoUtilizador = tipoUtilizador;

            bd.SaveChanges();
        }

        public static List<Utilizador> getUtilizadores()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Utilizador> listaUtilizadores = bd.PessoaSet.OfType<Utilizador>().ToList();

            return listaUtilizadores;
        }

        public static string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();
        }
    }
}
