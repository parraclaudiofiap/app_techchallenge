using Domain.Base;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Cliente : IAggregateRoot
{
    public CPF CPF { get; private set; }
    public string Nome { get; private set; }
    public string Email{ get; private set; }
    
    public Cliente(CPF cpf, string nome, string email)
    {
        CPF = cpf;
        Nome = nome;
        Email = email;
        
        ValidateEntity();
    }

    private void ValidateEntity()
    {
        CPFAssertionConcern.AssertCPFFormat(CPF, "O numero de CPF informado não é valido");
        EmailAssertionConcern.AssertEmailFormat(Email, "O Email informado não é valido");
        AssertionConcern.AssertArgumentNotEmpty(Nome, "O Nome não pode estar em branco");
    }
}