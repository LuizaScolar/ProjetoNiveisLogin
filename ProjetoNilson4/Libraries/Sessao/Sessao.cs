using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace ProjetoNilson4.Libraries.Sessao
{
    public class Sessao
    {
        private readonly IHttpContextAccessor _context;
        public Sessao(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private ISession Session => _context?.HttpContext?.Session ?? throw new InvalidOperationException("HTTP context or session is not available.");

        public void Cadastrar(string Key, string Valor)
        {
            Session.SetString(Key, Valor);
        }

        public string? Consultar(string Key)
        {
            return Session.GetString(Key);
        }
        public bool Existe(string Key)
        {
            if (Session.GetString(Key) == null)
            {
                return false;
            }
            return true;
        }
        public void Remover(string Key)
        {
            Session.Remove(Key);
        }
        public void RemoverTodos()
        {
            Session.Clear();
        }
        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                Session.Remove(Key);
            }
            Session.SetString(Key, Valor);
        }
    }
}
