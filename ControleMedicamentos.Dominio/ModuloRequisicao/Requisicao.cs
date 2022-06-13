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
            QtdMedicamento = qtdMedicamento;
            Data = data;
            Funcionario = funcionario;
        }

        public int QtdMedicamento { get; set; }
        public DateTime Data { get; set; }
        public Medicamento Medicamento { get; set; }
        public Paciente Paciente { get; set; }
        public Funcionario Funcionario { get; set; }

        public override bool Equals(object obj)
        {
            Requisicao requisicao = obj as Requisicao;

            if (requisicao == null)
                return false;

            return
                requisicao.Id.Equals(Id) &&
                requisicao.Medicamento.Equals(Medicamento) &&
                requisicao.Paciente.Equals(Paciente) &&
                requisicao.QtdMedicamento.Equals(QtdMedicamento) &&
                requisicao.Data.Equals(Data) &&
                requisicao.Funcionario.Equals(Funcionario);
        }
    }
}
