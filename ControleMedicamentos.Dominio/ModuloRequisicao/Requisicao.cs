using ControleMedicamentos.Dominio.Compartilhado;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using System;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {
        public Requisicao()
        {
        }

        public Requisicao(Medicamento medicamento, Paciente paciente, 
            int qtdMedicamento, DateTime data, Funcionario funcionario)
        {
            Medicamento = medicamento;
            Paciente = paciente;
            Funcionario = funcionario;
            QtdMedicamento = qtdMedicamento;
            Data = data;
        }

        public int QtdMedicamento { get; set; }
        public DateTime Data { get; set; }
        public Medicamento Medicamento { get; set; }
        public int IdMedicamento { get; set; }
        public Paciente Paciente { get; set; }
        public int IdPaciente { get; set; }
        public Funcionario Funcionario { get; set; }
        public int IdFuncionario { get; set; }
        
        public override bool Equals(object obj)
        {
            Requisicao requisicao = obj as Requisicao;

            if (requisicao == null)
                return false;

            return
                requisicao.Id.Equals(Id) &&
                requisicao.IdMedicamento.Equals(IdMedicamento) &&
                requisicao.IdPaciente.Equals(IdPaciente) &&
                requisicao.QtdMedicamento.Equals(QtdMedicamento) &&
                requisicao.Data.Equals(Data) &&
                requisicao.IdFuncionario.Equals(IdFuncionario);
        }
    }
}
