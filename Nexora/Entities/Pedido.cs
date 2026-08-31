using Nexora.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexora.Entities
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public int Numero { get; private set; }

        [Required]
        public StatusPedido Status { get; private set; }

        [Required]
        public OrigemPedido Origem { get; private set; }

        public int? ClienteId { get; private set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; private set; }

        public int? CriadoPorUsuarioId { get; private set; }

        [ForeignKey(nameof(CriadoPorUsuarioId))]
        public Usuario? CriadoPorUsuario { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public DateTime? DataAtualizacao { get; private set; }

        public List<ItemPedido> Itens { get; set; } = new();

        public Pagamento? Pagamento { get; set; }

        public decimal Total => Itens.Sum(i => i.Subtotal);

        protected Pedido() { } 
        public Pedido(OrigemPedido origem, Cliente? cliente, int? criadoPorUsuarioId)
        {
            Origem = origem;
            Cliente = cliente;
            ClienteId = cliente?.Id;
            CriadoPorUsuarioId = criadoPorUsuarioId;

            Status = StatusPedido.Criado;
            DataCriacao = DateTime.UtcNow;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AdicionarItem(Produto produto, int quantidade, string? observacao)
        {
            GarantirEdicaoPermitida();

            if (!produto.Disponivel)
                throw new InvalidOperationException("Produto indisponível não pode ser adicionado ao pedido.");

            var item = new ItemPedido(produto, quantidade, observacao);
            Itens.Add(item);
            AtualizarDataModificacao();
        }
        public void RemoverItem(int itemPedidoId)
        {
            GarantirEdicaoPermitida();

            var item = Itens.FirstOrDefault(i => i.Id == itemPedidoId);
            if (item is null)
                throw new InvalidOperationException("Item não encontrado neste pedido.");

            Itens.Remove(item);
            AtualizarDataModificacao();
        }

        public void AlterarQuantidadeItem(int itemPedidoId, int novaQuantidade)
        {
            GarantirEdicaoPermitida();

            var item = Itens.FirstOrDefault(i => i.Id == itemPedidoId);
            if (item is null)
                throw new InvalidOperationException("Item não encontrado neste pedido.");

            item.AlterarQuantidade(novaQuantidade);
            AtualizarDataModificacao();
        }

        // ---------- Transições de estado ----------

        public void Cancelar()
        {
            if (Status != StatusPedido.Criado)
                throw new InvalidOperationException("Pedido só pode ser cancelado enquanto estiver em 'Criado'.");

            Status = StatusPedido.Cancelado;
            AtualizarDataModificacao();
        }

        public void IniciarPreparacao()
        {
            if (Status != StatusPedido.Criado)
                throw new InvalidOperationException("Pedido só pode iniciar preparação a partir de 'Criado'.");

            Status = StatusPedido.EmPreparacao;
            AtualizarDataModificacao();
        }

        public void MarcarComoPronto()
        {
            if (Status != StatusPedido.EmPreparacao)
                throw new InvalidOperationException("Pedido só pode ficar 'Pronto' a partir de 'EmPreparacao'.");

            Status = StatusPedido.Pronto;
            AtualizarDataModificacao();
        }

        public void MarcarComoFinalizado()
        {
            if (Status != StatusPedido.Pronto)
                throw new InvalidOperationException("Pedido só pode ser 'Finalizado' a partir de 'Pronto'.");

            Status = StatusPedido.Finalizado;
            AtualizarDataModificacao();
        }

        // ---------- Auxiliares privados ----------

        private void GarantirEdicaoPermitida()
        {
            if (Status != StatusPedido.Criado)
                throw new InvalidOperationException("Pedido só pode ser editado enquanto estiver em 'Criado'.");
        }

        private void AtualizarDataModificacao()
        {
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
