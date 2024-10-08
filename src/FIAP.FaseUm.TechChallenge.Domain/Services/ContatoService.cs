﻿using FIAP.FaseUm.TechChallenge.Custom.Exceptions.Config;
using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;

namespace FIAP.FaseUm.TechChallenge.Domain.Services
{
    public class ContatoService(IContatoRepository contatoRepository) : IContatoService
    {
        public async Task AlterarContato(int id, Contato contato)
        {
            var contatoBase = await contatoRepository.GetById(id) ??
                throw new NotFoundException($"Contato de id {id} não encontrado");

            contatoBase.Alterar(contato.Nome!, contato.Telefone!, contato.Email!);

            contatoRepository.Update(contatoBase);
        }

        public void CadastrarContato(Contato contato) => contatoRepository.Add(contato);

        public async Task<IEnumerable<Contato>> ListarContatos(string ddd) => await contatoRepository.ListarContatos(ddd) ?? new List<Contato>();        

        public async Task RemoverContato(int id)
        {
            var contato = await contatoRepository.GetById(id) ??
                throw new NotFoundException($"Contato de id {id} não encontrado");

            contatoRepository.Delete(contato);
        }
    }
}
