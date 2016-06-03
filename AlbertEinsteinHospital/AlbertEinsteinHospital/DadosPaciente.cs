using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbertEinsteinHospital
{
    public class DadosPaciente
    {  
        public static void registarPaciente(string nome, DateTime dataNascim, int numSns, string genero, string morada, int telefone, string email)
        {
            AEH_BDEntities db = new AEH_BDEntities();

            Paciente p = new Paciente();
            p.Nome = nome;
            p.DataNascimento = dataNascim;
            p.NumSns = numSns;
            p.Genero = genero;
            p.Morada = morada;
            p.Telefone = telefone;
            p.Email = email;

            db.PessoaSet.Add(p);
            db.SaveChanges();
            
            Paciente paciente = db.PessoaSet.OfType<Paciente>().Where(x => x.Id == 1).OrderBy(Paciente=>p.Nome).FirstOrDefault();
            db.Dispose();
        }

        public static List<Paciente> getPacientes()
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            List<Paciente> listaPacientes = bd.PessoaSet.OfType<Paciente>().OrderBy(Paciente => Paciente.Nome).ToList();

            return listaPacientes;
        }

        public static void atualizarPaciente(string nome, DateTime dataNascim, int numSns, string genero, string morada, int telefone, string email)
        {
            AEH_BDEntities bd = new AEH_BDEntities();

            Paciente p = bd.PessoaSet.OfType<Paciente>().Where(i => i.NumSns == numSns).OrderBy(Paciente => Paciente.Nome).FirstOrDefault();

            p.Nome = nome;
            p.DataNascimento = dataNascim;
            p.NumSns = numSns;
            p.Genero = genero;
            p.Morada = morada;
            p.Telefone = telefone;
            p.Email = email;

            bd.SaveChanges();
        }

        
    }
}
